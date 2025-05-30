using System;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoFix.Models;
using AutoFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AutoFix.Controllers
{    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, IOrderService orderService, 
                                ILogger<AccountController> logger)
        {
            _authService = authService;
            _orderService = orderService;
            _logger = logger;
        }        [HttpGet]
        public IActionResult Register(string role = "Client")
        {
            ViewBag.Role = role;
            return View();
        }        [HttpPost]
        [ValidateAntiForgeryToken]        public async Task<IActionResult> Register(string fullName, string userName, string email, 
                                                string password, string confirmPassword, string role, 
                                                List<string>? skills = null, List<string>? services = null)
        {
            // Log the received parameters for debugging
            _logger.LogInformation("Registration attempt - FullName: '{FullName}', UserName: '{UserName}', Email: '{Email}', Role: '{Role}'", 
                                   fullName ?? "NULL", userName ?? "NULL", email ?? "NULL", role ?? "NULL");

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(userName) || 
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "All fields are required");
                ViewBag.Role = role ?? "Client";
                return View();
            }

            if (string.IsNullOrEmpty(role))
            {
                ModelState.AddModelError("", "Please select a role (Client or Mechanic)");
                ViewBag.Role = "Client";
                return View();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                ViewBag.Role = role;
                return View();
            }

            // Check for unique username and email
            if (!await _authService.IsUsernameUniqueAsync(userName))
            {
                ModelState.AddModelError("", "Username already exists");
                return View();
            }

            if (!await _authService.IsEmailUniqueAsync(email))
            {
                ModelState.AddModelError("", "Email already exists");
                return View();
            }

            try
            {
                var user = await _authService.RegisterUserAsync(fullName, userName, email, password, role);
                  // If the user is a mechanic, update their skills and services
                if (role.ToLower() == "mechanic" && user is Mechanic mechanic)
                {
                    // Update the mechanic's skills and services
                    mechanic.Skills = skills ?? new List<string>();
                    mechanic.Services = services ?? new List<string>();
                    
                    // Update in database
                    await _authService.UpdateMechanicProfileAsync(mechanic);
                }

                return RedirectToAction("Login");
            }            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Message}", ex.Message);
                ModelState.AddModelError("", $"Error registering user: {ex.Message}");
                ViewBag.Role = role ?? "Client";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string emailOrUsername, string password, bool rememberMe = false)
        {
            if (string.IsNullOrEmpty(emailOrUsername) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username/Email and Password are required");
                return View();
            }

            var user = await _authService.LoginUserAsync(emailOrUsername, password);
            
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutUserAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            
            if (role == "Mechanic")
            {
                var mechanicFilter = Builders<Mechanic>.Filter.Eq(m => m.Id, userId);
                var mechanic = await _authService.GetMechanicByIdAsync(userId);
                
                if (mechanic == null)
                {
                    return NotFound();
                }
                
                // Get completed orders count
                ViewBag.CompletedOrdersCount = mechanic.CompletedOrders;
                
                return View("MechanicProfile", mechanic);
            }
            else
            {                var client = await _authService.GetClientByIdAsync(userId);
                
                if (client == null)
                {
                    return NotFound();
                }                // Get recent orders
                try 
                {
                    var allOrders = await _orderService.GetClientOrdersAsync(userId);
                    var recentOrders = allOrders.OrderByDescending(o => o.OrderDate).Take(5).ToList();
                    ViewBag.RecentOrders = recentOrders;
                    ViewBag.OrdersCount = allOrders.Count;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to get client orders");
                    ViewBag.RecentOrders = new List<ClientOrder>();
                    ViewBag.OrdersCount = 0;
                }
                
                return View("ClientProfile", client);
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> UpdateProfile(string id, List<string> Skills, List<string> Services, string Bio)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var mechanic = await _authService.GetMechanicByIdAsync(id);
            if (mechanic == null)
            {
                return NotFound();
            }
            
            // Only allow updating own profile
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != id)
            {
                return Forbid();
            }
            
            // Update mechanic properties
            mechanic.Skills = Skills ?? new List<string>();
            mechanic.Services = Services ?? new List<string>();
            mechanic.Bio = Bio ?? string.Empty;
            
            var success = await _authService.UpdateMechanicProfileAsync(mechanic);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to update profile. Please try again.");
                ViewBag.CompletedOrdersCount = mechanic.CompletedOrders;
                return View("MechanicProfile", mechanic);
            }
            
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult RegisterClient()
        {
            return View("Register", new { role = "Client" });
        }

        [HttpGet]
        public IActionResult RegisterMechanic()
        {
            return View("Register", new { role = "Mechanic" });
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
