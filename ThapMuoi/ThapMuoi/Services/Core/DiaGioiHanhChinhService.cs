using MongoDB.Driver;
using ThapMuoi.Exceptions;
using ThapMuoi.Extensions;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Services.Core
{
    public class DiaGioiHanhChinhService : BaseService, IDiaGioiHanhChinhService
    {
        private DataContext _context;
        private BaseMongoDb<DiaGioiHanhChinhModel, string> BaseMongoDb;
        private IMongoCollection<DiaGioiHanhChinhModel> _collection;

        
        public DiaGioiHanhChinhService( DataContext context,
            IHttpContextAccessor contextAccessor)
            : base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<DiaGioiHanhChinhModel, string>(_context.DIAGIOIHANHCHINH);
            _collection = context.DIAGIOIHANHCHINH;
        }
        
        public async Task<dynamic> Create(DiaGioiHanhChinhModel model)
        {
            try{
                
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            var entity = new DiaGioiHanhChinhModel()
            {
                Name = model.Name,
                Code = model.Code,
                IdDonViCha = model.IdDonViCha,
            };
            var code = _collection.Find(x => x.Code == model.Code && !x.IsDeleted).FirstOrDefault();
            if (code != null)
                throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);
            if (model.IdDonViCha != null)
            {
                var donVi = _collection.Find(x => x.Code == model.IdDonViCha && !x.IsDeleted).FirstOrDefault();
                if (donVi == null)
                    throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage("Đơn vị cha không đúng !");
                
                entity.Level= donVi.Level + 1; 
            }
            var result = await BaseMongoDb.CreateAsync(entity);
            if (result.Entity.Id == default || !result.Success)
                throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
                
            return entity;
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("is not a valid 24 digit hex string."))
                {
                    throw new ResponseMessageException().WithException(DefaultCode.ID_NOT_CORRECT_FORMAT);
                }

                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(ex.Message);
            }
        }

        public async Task<dynamic> Update(DiaGioiHanhChinhModel model)
        {
            try
            {  
                if (model == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

                
                var code = _collection.Find(x => x.Code == model.Code && !x.IsDeleted && x.Id != model.Id).FirstOrDefault();
                if (code != null)
                    throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);

                
                
                var entity = _collection.Find(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefault();

                if (entity == default)
                    throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);

                entity.Name = model.Name;
                entity.IdDonViCha = model.IdDonViCha;
                entity.Code = model.Code;
                entity.Sort = model.Sort;
                if (model.IdDonViCha != null)
                {
                    var donVi = _collection.Find(x => x.Code == model.IdDonViCha && !x.IsDeleted).FirstOrDefault();
                    if (donVi == null)
                        throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage("Đơn vị cha không đúng !");
                    entity.Level= donVi.Level + 1; 
                }
                var result = await BaseMongoDb.UpdateAsync(entity);
                if (!result.Success)
                    throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
                
                return entity;
            } 
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("is not a valid 24 digit hex string."))
                {
                    throw new ResponseMessageException().WithException(DefaultCode.ID_NOT_CORRECT_FORMAT);
                }
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage(e.Message);
            }
            
         
        }
        

        public async Task<dynamic> GetListChildByCode(string code)
        {
            var list = _collection.AsQueryable().Where(x => !x.IsDeleted && x.Code != null && x.IdDonViCha == code).
                Select(x=> new {id = x.Id, code = x.Code,name =  x.Name }).ToList();
            return list;
        }
        public async  Task<dynamic> GetListByLevel(int level)
        {
            var list = _collection.AsQueryable().Where(x => x.IsDeleted != true && x.Id != null && x.Level== level)
                .Select(x=> new {id = x.Id, code = x.Code,name =  x.Name }).ToList();
            return list;
        }
    }
}