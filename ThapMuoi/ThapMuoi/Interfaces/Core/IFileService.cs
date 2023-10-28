using ThapMuoi.Models.Core;

namespace ThapMuoi.Interfaces.Core
{
    public interface IFileService
    {
       // File GetById(string id);

        //Task<dynamic> GetListFilesById(string id);


        Task<FileModel> SaveFileAsync(string fileName, string saveName, string path, string pathFolder, long fileSize, string fileExt, string foreignKey);

      //  Task<File> SaveFileAsyncType(string filePath, string fileName, string newFileName, string fileExt, long fileSize, int index, string idCanBo, string pathFolder);
       // Task<File> SaveFileAsync(string fileId, string filePath, string fileName, string newFileName, string fileExt, long fileSize);

        Task<bool> DeleteFilesById(string id);


        //Task<bool> DeletedFilesWithIsDeletedFlag(PagingParam pagingParam);
        //Task<bool> DeleteFiles(List<FileShort> list);


       // Task<bool> DeletedFiles(List<FileShort> list);




    }
}