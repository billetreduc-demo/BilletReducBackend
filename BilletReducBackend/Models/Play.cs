using BilletReducBackend.Contracts.Play;
using BilletReducBackend.ServiceErrors;
using ErrorOr;

namespace BilletReducBackend.Models;

public class Play
{
    public const int MinTitleLength = 3;
    public const int MaxTitleLength = 30;

    public const int MinGenreLength = 3;
    public const int MaxGenreLength = 15;

    public const int MinDescriptionLength = 0;
    public const int MaxDescriptionLength = 400;

    private Play(Guid id, string title, string genre, string description, string uniqueImageId, float price,
        DateTime dateTime, int numberOfTickets, int numberOfReservations)
    {
        Id = id;
        Title = title;
        Genre = genre;
        Description = description;
        UniqueImageId = uniqueImageId;
        Price = price;
        DateTime = dateTime;
        NumberOfTickets = numberOfTickets;
        NumberOfReservations = numberOfReservations;
    }

    public Guid Id { get; }
    public string Title { get; }
    public string Genre { get; }
    public string Description { get; }
    public string UniqueImageId { get; }
    public float Price { get; }
    public DateTime DateTime { get; }
    public int NumberOfTickets { get; }
    public int NumberOfReservations { get; set; }

    private static ErrorOr<Play> Create(
        string title,
        string genre,
        string description,
        string uniqueImageId,
        float price,
        DateTime dateTime,
        int numberOfTickets
    )
    {
        List<Error> errors = new();

        if (title.Length is < MinTitleLength or > MaxTitleLength) errors.Add(Errors.Play.InvalidTitle);

        if (genre.Length is < MinGenreLength or > MaxGenreLength) errors.Add(Errors.Play.InvalidGenre);

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            errors.Add(Errors.Play.InvalidDescription);
        
        if (price < 0) errors.Add(Errors.Play.InvalidPrice);
        
        if (numberOfTickets < 0) errors.Add(Errors.Play.InvalidNumberOfTickets);

        if (errors.Any()) return errors;

        return new Play(
            Guid.NewGuid(),
            title,
            genre,
            description,
            uniqueImageId,
            price,
            dateTime,
            numberOfTickets,
            0
        );
    }

    public static ErrorOr<Play> From(CreatePlayRequest request)
    {
        return Create(
            request.Title,
            request.Genre,
            request.Description,
            request.UniqueImageId,
            request.Price,
            request.DateTime,
            request.NumberOfTickets
        );
    }
}