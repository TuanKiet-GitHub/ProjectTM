using Minio;
using Minio.DataModel.Args;
using ThapMuoi.Exceptions;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Appsettings;

namespace ThapMuoi.Services.Core
{
    public class FileMinioService :  BaseService, IFileMinioService 
    {
        
        private readonly IMinioClient _minio;
        private MinioSetting _minioSetting;
        public FileMinioService(
            DataContext context,
            IMinioClient minio,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _minio = minio;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            _minioSetting = _configuration.GetSection("MinioSettings").Get<MinioSetting>();
        }

        public async Task<string> UploadFile(IFormFile file)
    {
        var key = String.Empty;
        try
        {
            string endpoint = "minio.dongthap.gov.vn:9000";
            string accessKey = "CKghMslGxFQhnlTN";
            string secretKey = "UKnz2ype9MTCKNZqH2wbFcxS1Vph7ncx";
            key = Guid.NewGuid().ToString();
            var stream = file.OpenReadStream();
            var request = new PutObjectArgs()
                .WithBucket("hscbcc")
                .WithObject("KIET/" +file.FileName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType("applicaion/octet-stream");

            MinioClient minioClient = (MinioClient)new MinioClient()
                .WithEndpoint(endpoint)
                
                .WithCredentials(accessKey, secretKey)
                .WithSSL()
                .Build();
            await minioClient.PutObjectAsync(request);
        }
        catch (Exception e)
        {
            throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.Message);
        }
        return key;
    }
    }
}