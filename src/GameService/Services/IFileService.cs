namespace GameService.Services;

public interface IFileService
{
    Task<string> UploadVideo(IFormFile File);
    Task<string> UploadImage(IFormFile File);
    Task<string> UploadZip(IFormFile file);
    Task<string> DownloadGame(string publicId);
}