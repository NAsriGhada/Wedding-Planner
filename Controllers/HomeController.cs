using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using weddingPlannerZied.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace weddingPlannerZied.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }


    // *************************REGISTER**************************
    [HttpPost("/register")]
    public IActionResult Register(User NewUser)
    {
        if (ModelState.IsValid)
        {
            if (_context.Users.Any(u => u.Email == NewUser.Email))
            {
                ModelState.AddModelError("Email", " email is already used 😓😈");
                return View("Index");
            }
            else
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                _context.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("userId", NewUser.UserId);
                return RedirectToAction("Dashboard");

            }
        }
        return View("Index");

    }


    // *************************************Login****************************
    [HttpPost("/login")]
    public IActionResult _LoginUser(LoginUser loginUser)
    {
        if (ModelState.IsValid)
        {
            var UserExist = _context.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);
            if (UserExist == null)
            {
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LoginUser>? hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(loginUser, UserExist.Password, loginUser.LoginPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("userId", UserExist.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    }



    // *************************VIEW DASHBORD**************************
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("Index");
        }
        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;
        List<Wedding> Wedders=_context.Weddings.Include(w=>w.User).ToList();
        ViewBag.Wedders = Wedders;

        
        return View();
    }


    // **************************************Add Wedding View*********************************************
    [HttpGet("/addwedding")]
    public IActionResult AddWedding()
    {
        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;
        return View();
    }
    // **************************************Add Wedding Post*********************************************


    [HttpPost("/new/wedding")]
    public IActionResult AddNewWedding(Wedding newWedding)
    {
        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;
        if (ModelState.IsValid)
        {
            newWedding.UserId = (int)HttpContext.Session.GetInt32("userId");
            _context.Add(newWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("AddWedding");
    }

    // **************************************Logout*********************************************


    [HttpGet("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }




















    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
