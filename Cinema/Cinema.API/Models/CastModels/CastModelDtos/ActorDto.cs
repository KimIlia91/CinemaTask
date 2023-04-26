namespace Cinema.API.Models.CastModels.CastModelDtos
{
    public class ActorDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
