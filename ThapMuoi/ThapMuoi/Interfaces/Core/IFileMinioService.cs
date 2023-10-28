namespace ThapMuoi.Interfaces.Core
{
    public interface IFileMinioService
    {
        public Task<string> UploadFile(IFormFile file);
    }
}