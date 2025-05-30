using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFix.Data;
using AutoFix.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AutoFix.Services
{    public interface IAuthService
    {
        Task<ApplicationUser> RegisterUserAsync(string fullName, string userName, string email, string password, string role);
        Task<ApplicationUser?> LoginUserAsync(string emailOrUsername, string password);
        Task<bool> LogoutUserAsync();
        Task<bool> IsUsernameUniqueAsync(string username);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> UpdateMechanicProfileAsync(Mechanic mechanic);
        Task<Mechanic?> GetMechanicByIdAsync(string id);
        Task<Client?> GetClientByIdAsync(string id);
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }

    public class AuthService : IAuthService
    {
        private readonly MongoDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;        public AuthService(MongoDbContext context, IHttpContextAccessor httpContextAccessor, 
                          ILogger<AuthService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }public async Task<ApplicationUser> RegisterUserAsync(string fullName, string userName, string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("All parameters are required and cannot be null or empty");
            }

            ApplicationUser newUser;
            
            try
            {
                if (role.ToLower() == "client")
                {
                    newUser = new Client
                    {
                        FullName = fullName,
                        UserName = userName,
                        Email = email,
                        PasswordHash = HashPassword(password),
                        CreatedDate = DateTime.Now,
                        Role = "Client"
                    };
                    
                    _logger.LogInformation("Attempting to insert new client: {UserName}", userName);
                    await _context.Clients.InsertOneAsync((Client)newUser);
                    _logger.LogInformation("Successfully inserted client: {UserName} with ID: {Id}", userName, newUser.Id);
                }
                else if (role.ToLower() == "mechanic")
                {
                    newUser = new Mechanic
                    {
                        FullName = fullName,
                        UserName = userName,
                        Email = email,
                        PasswordHash = HashPassword(password),
                        CreatedDate = DateTime.Now,
                        Role = "Mechanic",
                        Skills = new List<string>(),
                        Services = new List<string>()
                    };
                    
                    _logger.LogInformation("Attempting to insert new mechanic: {UserName}", userName);
                    await _context.Mechanics.InsertOneAsync((Mechanic)newUser);
                    _logger.LogInformation("Successfully inserted mechanic: {UserName} with ID: {Id}", userName, newUser.Id);
                }
                else
                {
                    throw new ArgumentException($"Invalid role specified: {role}. Role must be 'Client' or 'Mechanic'");
                }

                if (newUser?.Id == null)
                {
                    throw new InvalidOperationException("User was created but ID is null - database insertion may have failed");
                }

                return newUser;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error in RegisterUserAsync for user {UserName}: {Message}", userName, ex.Message);
                throw new InvalidOperationException($"Database error during user registration: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in RegisterUserAsync for user {UserName}: {Message}", userName, ex.Message);
                throw;
            }
        }public async Task<ApplicationUser?> LoginUserAsync(string emailOrUsername, string password)
        {
            ApplicationUser? user = null;
            
            // Try to find the user by email or username in clients
            var clientFilter = Builders<Client>.Filter.Or(
                Builders<Client>.Filter.Eq(c => c.Email, emailOrUsername),
                Builders<Client>.Filter.Eq(c => c.UserName, emailOrUsername)
            );
            
            user = await _context.Clients.Find(clientFilter).FirstOrDefaultAsync();
            
            // If not found in clients, try mechanics
            if (user == null)
            {
                var mechanicFilter = Builders<Mechanic>.Filter.Or(
                    Builders<Mechanic>.Filter.Eq(m => m.Email, emailOrUsername),
                    Builders<Mechanic>.Filter.Eq(m => m.UserName, emailOrUsername)
                );
                
                user = await _context.Mechanics.Find(mechanicFilter).FirstOrDefaultAsync();
            }
            
            // If user found and password is correct
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                // Update LastLoginDate
                if (user.Role == "Client")
                {
                    var clientUpdate = Builders<Client>.Update.Set(c => c.LastLoginDate, DateTime.Now);
                    await _context.Clients.UpdateOneAsync(c => c.Id == user.Id, clientUpdate);
                }
                else
                {
                    var mechanicUpdate = Builders<Mechanic>.Update.Set(m => m.LastLoginDate, DateTime.Now);
                    await _context.Mechanics.UpdateOneAsync(m => m.Id == user.Id, mechanicUpdate);
                }
                
                // Create authentication claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("FullName", user.FullName)
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };
                  if (_httpContextAccessor.HttpContext != null)
                {
                    await _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                }
                
                return user;
            }
            
            return null; // User not found or password incorrect
        }        public async Task<bool> LogoutUserAsync()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return true;
        }public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            try
            {
                var clientExists = await _context.Clients.Find(c => c.UserName == username).AnyAsync();
                var mechanicExists = await _context.Mechanics.Find(m => m.UserName == username).AnyAsync();
                return !(clientExists || mechanicExists);
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when checking username uniqueness: {Message}", ex.Message);
                // Return false to prevent registration when we can't confirm uniqueness
                return false;
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            try
            {
                var clientExists = await _context.Clients.Find(c => c.Email == email).AnyAsync();
                var mechanicExists = await _context.Mechanics.Find(m => m.Email == email).AnyAsync();
                return !(clientExists || mechanicExists);
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when checking email uniqueness: {Message}", ex.Message);
                // Return false to prevent registration when we can't confirm uniqueness
                return false;
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            string hashedInput = HashPassword(password);
            return string.Equals(hashedInput, passwordHash, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> UpdateMechanicProfileAsync(Mechanic mechanic)
        {
            try
            {
                var filter = Builders<Mechanic>.Filter.Eq(m => m.Id, mechanic.Id);
                var update = Builders<Mechanic>.Update
                    .Set(m => m.Skills, mechanic.Skills)
                    .Set(m => m.Services, mechanic.Services);

                var result = await _context.Mechanics.UpdateOneAsync(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating mechanic profile: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<Mechanic?> GetMechanicByIdAsync(string id)
        {
            try
            {
                var filter = Builders<Mechanic>.Filter.Eq(m => m.Id, id);
                return await _context.Mechanics.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving mechanic by ID: {Message}", ex.Message);
                return null;
            }
        }

        public async Task<Client?> GetClientByIdAsync(string id)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(c => c.Id, id);
                return await _context.Clients.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving client by ID: {Message}", ex.Message);
                return null;
            }
        }
    }
}
