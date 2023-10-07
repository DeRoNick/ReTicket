using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReTicket.Application.Auth.Commands;

namespace ReTicket.MVC.Controllers;

public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost("/register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterUser.Command model)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(model).ConfigureAwait(false);
            return RedirectToAction("Login");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginUser.Command model)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(model).ConfigureAwait(false);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}