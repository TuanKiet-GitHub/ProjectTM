using MongoDB.Driver;
using ThapMuoi.Exceptions;
using ThapMuoi.Extensions;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Services.Core
{
    public class FileService :  BaseService, IFileService
    {
    private DataContext _context;
    private BaseMongoDb<FileModel, string> BaseMongoDb;

    IMongoCollection<FileModel> _collection;
    
    public FileService(
        DataContext context,
        IHttpContextAccessor contextAccessor) :
        base(context, contextAccessor)
    {
        _context = context;
        BaseMongoDb = new BaseMongoDb<FileModel, string>(_context.FILES);
        _collection = context.FILES;
    }


        public async Task<FileModel> SaveFileAsync(string fileName, string saveName, string path, string pathFolder, long fileSize, string fileExt, string foreignKey)
        {
        var entity = new FileModel();
        entity.FileName = fileName;
        entity.SaveName = saveName;
        entity.Path = path;
        entity.Size = fileSize;
        entity.Ext = fileExt;
        entity.CreatedAt = DateTime.Now;
        entity.IsDeleted = false;
        var result = await BaseMongoDb.CreateAsync(entity);

        if (result.Entity.Id == default || !result.Success)
            throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);

        return entity;
    }

    public Task<FileModel> SaveFileAsync(string filePath, string fileName, string newFileName, string fileExt, long fileSize, int index,
        string pathFolder)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteFilesById(string id)
        {
            if ( id == default)
                throw new ResponseMessageException().WithException(DefaultCode.EXCEPTION);
            
            var entity = _context.FILES.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
         
            if (entity == default)
                throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
            
            entity.IsDeleted = true; 
            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
                return false;
            return true;
        }

        //public async Task<bool> DeleteFiles(List<FileShort> list)
        //{
        //    if (list == default)
        //        throw new ResponseMessageException().WithException(DefaultCode.EXCEPTION);
        //    foreach (var item in list)
        //    {  
        //        var entity = _context.FILES.Find(x => !x.IsDeleted  && x.Id == item.FileId).FirstOrDefault();
        //        if (entity == default)
        //            throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
        //        entity.IsDeleted = true;
        //        var result = await BaseMongoDb.UpdateAsync(entity);
        //        if (!result.Success)
        //            return false;
        //    }
        //    return true;
        //}


        
        public async Task<bool> DeletedFiles(List<FileShort> list)
        {
            var baseDirectory = "../files";

            foreach (var item in list)
            {
                var entity = await _context.FILES.Find(x => x.Id == item.FileId).FirstOrDefaultAsync();
                if (entity == default)
                    throw new ResponseMessageException().WithException(DefaultCode.EXCEPTION);

                var filePath = entity.Path;
                

                try
                {
                    var fullPath = Path.GetFullPath(filePath);

                    if (!System.IO.File.Exists(fullPath))
                    {
                        continue; 
                    }

                 
                    System.IO.File.Delete(fullPath);

                    // Delete the corresponding database entry
                    await _context.FILES.DeleteOneAsync(x => x.Id == entity.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                }
            }

            return true;
        }
        //public async Task<bool> DeletedFilesWithIsDeletedFlag(PagingParam pagingParam)
        //{
        //    var pageSize = pagingParam.Limit;
        //    var pageIndex = pagingParam.Start;

        //    while (true)
        //    {
        //        var filesToDelete = await _context.FILES
        //            .Find(x => x.IsDeleted == true)
        //            .Skip((pageIndex - 1) * pageSize)
        //            .Limit(pageSize)
        //            .ToListAsync();

        //        if (filesToDelete.Count == 0)
        //            break;

        //        foreach (var entity in filesToDelete)
        //        {
        //            var fullPath = Path.GetFullPath(entity.Path);

        //            Console.WriteLine("Full file path: " + fullPath);

        //            try
        //            {
        //                if (!System.IO.File.Exists(fullPath))
        //                {
        //                    Console.WriteLine("File not found: " + fullPath);
        //                    continue; // Move on to the next file in the list
        //                }

        //                System.IO.File.Delete(fullPath);
        //                Console.WriteLine("File đã xoá: " + fullPath);

        //                // Delete the corresponding database entry
        //                await _context.FILES.DeleteOneAsync(x => x.Id == entity.Id);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error deleting file: {ex.Message}");
        //            }
        //        }

        //        pageIndex++;
        //    }

        //    return true;
        //}






        





    }
}