
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace GameService.Services;

public class FileService : IFileService
{
    private readonly Cloudinary cloudinary;
    private readonly Account account;
    private readonly IConfiguration _configuration;
    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
        account = new Account(
            configuration.GetValue<string>("Cloudinary:cloudName")
            , configuration.GetValue<string>("Cloudinary:a  piKey")
            , configuration.GetValue<string>("Cloudinary:apiSecret"));
        cloudinary = new Cloudinary(account);
        cloudinary.Api.Client.Timeout = TimeSpan.FromMinutes(30);
    }
    public async Task<string> UploadVideo(IFormFile File)
    {
        var uploadResult = new VideoUploadResult();
        if (File.Length > 0)
        {
            using var stream = File.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(File.FileName, stream),
                Folder = "g-steam_microservices"
            };

            uploadResult = await cloudinary.UploadAsync(uploadParams);
            string videoUrl = cloudinary.Api.UrlVideoUp.BuildUrl(uploadResult.PublicId);
            return videoUrl;
        }
        return "";

    }

    public async Task<string> UploadImage(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "g-steam_microservices"
            };

            uploadResult = await cloudinary.UploadAsync(uploadParams);
            string imageUrl = cloudinary.Api.UrlImgUp.BuildUrl(uploadResult.PublicId);
            // string imageUrl = cloudinary.Api.UrlVideoUp.BuildUrl(uploadResult.PublicId);
            return imageUrl;
        }
        return "";
    }

    public async Task<string> UploadZip(IFormFile file)
    {
        var uploadResult = new RawUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "g-steam_microservices"
            };

            uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
        return "";
    }

    public async Task<string> DownloadGame(string publicId)
    {
        var getUrl = cloudinary.Api.UrlImgUp
            .Transform(new Transformation().Quality("auto").FetchFormat("auto"))
            .BuildUrl(publicId);
        return getUrl;
    }
}