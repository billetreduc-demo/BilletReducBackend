using ErrorOr;

namespace BilletReducBackend.ServiceErrors;

public static class Errors
{
    public static class Play
    {
        public static Error NotFound => Error.NotFound(
            code: "Play.NotFound",
            description: "Play not found."
        );
        
        public static Error NoMoreTickets => Error.Validation(
            code: "Play.NoMoreTickets",
            description: "There are no more tickets for this play."
        );

        public static Error InvalidTitle => Error.Validation(
            code: "Play.InvalidTitle",
            description: $"Play title must be between {Models.Play.MinTitleLength}" +
                $" and {Models.Play.MaxTitleLength} characters long."
        );
        
        public static Error InvalidGenre => Error.Validation(
            code: "Play.InvalidGenre",
            description: $"Play genre must be between {Models.Play.MinGenreLength}" +
                $" and {Models.Play.MaxGenreLength} characters long."
        );

        public static Error InvalidDescription => Error.Validation(
            code: "Play.InvalidDescription",
            description: $"Play description must be between {Models.Play.MinDescriptionLength}" +
                $" and {Models.Play.MaxDescriptionLength} characters long."
        );
        
        public static Error InvalidPrice => Error.Validation(
            code: "Play.InvalidPrice",
            description: "Play price must be greater than 0."
        );
        
        public static Error InvalidNumberOfTickets => Error.Validation(
            code: "Play.InvalidNumberOfTickets",
            description: "Play number of tickets must be greater than 0."
        );
    }
}