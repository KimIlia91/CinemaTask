namespace Cinema.WEB.Services.IServices
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile? file, string? existingImagePath, string savePath);

        bool DeleteImage(string? imageUrl);
    }
}
