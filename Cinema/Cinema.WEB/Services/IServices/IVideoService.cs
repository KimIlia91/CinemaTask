﻿namespace Cinema.WEB.Services.IServices
{
    public interface IVideoService
    {
        Task<string?> SaveVideoAsync(IFormFile? file, string? existingImagePath, string savePath);

        bool DeleteImage(string? videoUrl);
    }
}
