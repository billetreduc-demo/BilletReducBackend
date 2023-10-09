using BilletReducBackend.Contracts.Play;
using BilletReducBackend.Models;
using ErrorOr;

namespace BilletReducBackend.Services.Plays;

public interface IPlayService
{
    ErrorOr<Created> CreatePlay(Play play);
    ErrorOr<Play> GetPlay(Guid id);
    ErrorOr<Play> AddPlayReservation(Guid id);
    List<Play> GetPlays();
    List<Play> SearchPlays(string searchTerm);
}