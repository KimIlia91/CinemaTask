using System.ComponentModel.DataAnnotations;

namespace Cinema.WEB.Helpers
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public FileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is null)
            {
                // если файл не выбран, то валидация проходит успешно
                return ValidationResult.Success!;
            }

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"Максимальный размер файла {_maxFileSize / 1024 / 1024} МБ");
            }

            return ValidationResult.Success!;
        }
    }
}
