using Cinema.WEB.Services.IServices;

namespace Cinema.WEB.Services
{
    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public VideoService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string?> SaveVideoAsync(IFormFile? file, string? existingImagePath, string savePath)
        {
            if (!IsValidVideo(file)) return existingImagePath;

            if (!string.IsNullOrEmpty(existingImagePath))
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, existingImagePath.TrimStart('\\'));

                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file!.FileName);
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, savePath);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"\\{savePath}\\{fileName}";
        }

        public bool DeleteImage(string? videoUrl)
        {
            if (videoUrl is null) return false;

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, videoUrl.TrimStart('\\'));

            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
                return true;
            }

            return false;
        }

        private static bool IsValidVideo(IFormFile? file)
        {
            if (file is null)
                return false;

            if (file.Length <= 0 || file.Length > 4294967296)
                throw new ArgumentException("Недопустимый размер файла");

            var allowedTypes = new[] { "video/mp4", "video/x-msvideo", "video/quicktime", "video/x-matroska" };

            if (!allowedTypes.Contains(file.ContentType))
                throw new ArgumentException("Недопустимый тип файла");

            var allowedExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };
            var fileExtension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Недопустимое расширение файла");

            return true;
        }
    }
}
