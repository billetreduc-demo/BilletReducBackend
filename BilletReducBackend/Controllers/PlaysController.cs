using BilletReducBackend.Contracts.Play;
using BilletReducBackend.Models;
using BilletReducBackend.Services.Plays;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BilletReducBackend.Controllers;

public class PlaysController : ApiController
{
    private readonly IPlayService _playService;

    public PlaysController(IPlayService playService)
    {
        _playService = playService;
    }

    [HttpPost()]
    public IActionResult CreatePlay(CreatePlayRequest request)
    {
        ErrorOr<Play> requestToPlayResult = Play.From(request);

        if (requestToPlayResult.IsError)
        {
            return Problem(requestToPlayResult.Errors);
        }

        var play = requestToPlayResult.Value;
        ErrorOr<Created> createPlayResult = _playService.CreatePlay(play);

        return createPlayResult.Match(
            _ => CreatedAsGetPlay(play),
            Problem
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPlay(Guid id)
    {
        ErrorOr<Play> getPlayResult = _playService.GetPlay(id);

        return getPlayResult.Match(
            play => Ok(MapPlayResponse(play)),
            Problem
        );
    }
    
    [HttpPost("{id:guid}/reservation")]
    public IActionResult AddPlayReservation(Guid id)
    {
        ErrorOr<Play> addPlayReservationResult = _playService.AddPlayReservation(id);
        
        return addPlayReservationResult.Match(
            play => Ok(MapPlayResponse(play)),
            Problem
        );
    }

    [HttpGet()]
    public IActionResult GetPlays()
    {
        List<Play> plays = _playService.GetPlays();

        return Ok(new GetPlaysResponse(
            plays.Select(MapPlayResponse).ToList()
        ));
    }
    
    [HttpGet("search")]
    public IActionResult SearchPlays([FromQuery] string searchTerm)
    {
        List<Play> plays = _playService.SearchPlays(searchTerm);

        return Ok(new GetPlaysResponse(
            plays.Select(MapPlayResponse).ToList()
        ));
    }

    private static PlayResponse MapPlayResponse(Play play)
    {
        return new PlayResponse(
            play.Id,
            play.Title,
            play.Genre,
            play.Description,
            play.UniqueImageId,
            play.Price,
            play.DateTime,
            play.NumberOfTickets,
            play.NumberOfReservations
        );
    }

    private CreatedAtActionResult CreatedAsGetPlay(Play play)
    {
        return CreatedAtAction(
            actionName: nameof(GetPlay),
            routeValues: new { id = play.Id },
            value: MapPlayResponse(play)
        );
    }
}