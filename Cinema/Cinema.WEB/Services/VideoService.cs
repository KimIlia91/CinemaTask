using Cinema.WEB.Services.IServices;
using System.Diagnostics;

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
            if (!IsValidVideo(file)) 
                return existingImagePath;

            return await UploadVideoAsync(file, savePath);
        }

        public bool DeleteVideo(string? videoUrl)
        {
            if (videoUrl is not null)
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, videoUrl.TrimStart('\\'));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                    return true;
                }
            }

            return false;
        }

        private static bool IsValidVideo(IFormFile? file)
        {
            if (file is null)
                return false;

            if (file.Length <= 0 || file.Length > 4294967296)
                throw new ArgumentException("Недопустимый размер файла");

            var allowedTypes = new[] { "video/mp4", "video/x-msvideo", "video/quicktime", "video/x-matroska", "video/avi", "video/mpeg", "video/x-ms-wmv", "video/webm", "video/x-flv" };
            if (!allowedTypes.Contains(file.ContentType))
                throw new ArgumentException("Недопустимый тип файла");

            var allowedExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv", ".AVI", ".MPEG", ".MOV", ".WMV", ".MP4", ".FLV", ".WebM" };
            var fileExtension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Недопустимое расширение файла");

            return true;
        }

        private async Task<string?> UploadVideoAsync(IFormFile? file, string savePath)
        {
            if (file is null || file.Length == 0)
                return null;

            var videoPath = Path.Combine(_hostEnvironment.WebRootPath, savePath);
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(videoPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (fileExtension.CompareTo(".mp4") == 0 || fileExtension.CompareTo(".MP4") == 0)
                return $"\\{savePath}\\{fileName}";

            var mp4VideoName = ConvertVideoToMp4(fileName, filePath);
            System.IO.File.Delete(filePath);
            return $"\\{savePath}\\{mp4VideoName}";
        }

        private string ConvertVideoToMp4(string fileName, string filePath)
        {
            var outputFileName = Path.ChangeExtension(fileName, ".mp4");
            var outputFilePath = Path.ChangeExtension(filePath, ".mp4");
            var ffmpegPath = Path.Combine(_hostEnvironment.WebRootPath, @"ffmpeg\ffmpeg.exe");
            var arguments = $"-i \"{filePath}\" -c:v libx264 -preset fast -crf 22 -c:a aac -b:a 128k -movflags +faststart \"{outputFilePath}\"";
            var processInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            var process = new Process { StartInfo = processInfo };
            process.Start();
            process.WaitForExit();
            return outputFileName;
        }
    }
}