using BilletReducBackend.Models;
using BilletReducBackend.ServiceErrors;
using ErrorOr;

namespace BilletReducBackend.Services.Plays;

public class PlayService : IPlayService
{
    private static readonly Dictionary<Guid, Play> Plays = new();

    public ErrorOr<Created> CreatePlay(Play play)
    {
        Plays.Add(play.Id, play);

        return Result.Created;
    }

    public ErrorOr<Play> GetPlay(Guid id)
    {
        if (Plays.TryGetValue(id, out var play))
        {
            return play;
        }

        return Errors.Play.NotFound;
    }
    
    public ErrorOr<Play> AddPlayReservation(Guid id)
    {
        if (!Plays.TryGetValue(id, out var play))
        {
            return Errors.Play.NotFound;
        }

        if (play.NumberOfTickets == play.NumberOfReservations)
        {
            return Errors.Play.NoMoreTickets;
        }
        
        play.NumberOfReservations++;
        return play;
    }

    public List<Play> GetPlays()
    {
        return Plays.Values.ToList();
    }
    
    public List<Play> SearchPlays(string searchTerm)
    {
        searchTerm = (searchTerm?.Trim().ToLowerInvariant()) ?? string.Empty;
        if (string.IsNullOrEmpty(searchTerm)) return new List<Play>();

        return Plays.Values
            .Where(p => p.Title.ToLowerInvariant().Contains(searchTerm)
                        || p.Genre.ToLowerInvariant().Contains(searchTerm)
                        || p.Description.ToLowerInvariant().Contains(searchTerm))
            .ToList();
    }
}