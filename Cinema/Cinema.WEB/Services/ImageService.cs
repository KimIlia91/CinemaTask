using Cinema.WEB.Services.IServices;

namespace Cinema.WEB.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string?> SaveImageAsync(IFormFile? file, string? existingImagePath, string savePath)
        {
            if (file is null) return existingImagePath;

            if (!string.IsNullOrEmpty(existingImagePath))
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, existingImagePath.TrimStart('\\'));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, savePath);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"\\{savePath}\\{fileName}";
        }

        public bool DeleteImage(string? imageUrl)
        {
            if (imageUrl != null)
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, imageUrl.TrimStart('\\'));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                    return true;
                }
            }

            return false;
        }
    }
}
