namespace BilletReducBackend.Contracts.Play;

public record PlayResponse (
    Guid Id,
    string Title,
    string Genre,
    string Description,
    string UniqueImageId,
    float Price,
    DateTime DateTime,
    int NumberOfTickets,
    int NumberOfReservations
);