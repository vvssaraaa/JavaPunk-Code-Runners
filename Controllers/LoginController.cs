using Javapunk.Data;
using Javapunk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Javapunk.Controllers;

public class LoginController : Controller
{
    private readonly UserService _userService;

    public LoginController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult LoginPage()
    {
        return View();
    }
    public IActionResult RegisterPage()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        Users? user = await _userService.GetUserWithUserName(username);
        if (user == null)
        {
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View("LoginPage");
        }
        // Check if the username and password are correct
        if (password == user.User_password)
        {
            // Set a session variable to indicate that the user is logged in
            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("User_name", user.User_name);
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Menu");
        }
        // If the login fails, display an error message
        ViewBag.ErrorMessage = "Invalid username or password.";
        return View("LoginPage");
    }
    [HttpPost]
    public async Task<IActionResult> RegisterUser(string username, string password)
    {
        if (await _userService.GetUserWithUserName(username) != null)
        {
            ViewBag.ErrorMessage = "User with this name already exists.";
            return View("RegisterPage");
        }
        Users? newUser = await _userService.CreateUser(username, password);
        if (newUser == null)
        {
            ViewBag.ErrorMessage = "Could not create user";
            return View("RegisterPage");
        }
        else
        {
            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("User_name", newUser.User_name);
            HttpContext.Session.SetInt32("UserId", newUser.Id);
            return RedirectToAction("Index", "Menu");
        }
        ;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}