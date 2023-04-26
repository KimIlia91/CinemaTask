namespace Cinema.WEB.Models.PersonModels.PersonDtos
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Country { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsActor { get; set; } = false;

        public bool IsDirector { get; set; } = false;

        public bool IsScreenwriter { get; set; } = false;
    }
}
