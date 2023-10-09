using Microsoft.AspNetCore.Mvc;

namespace BilletReducBackend.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}