using MongoDB.Driver;
using ThapMuoi.Constants;
using ThapMuoi.Exceptions;
using ThapMuoi.Extensions;
using ThapMuoi.FromBodyModels;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;
using ThapMuoi.Models.PagingParam;

namespace ThapMuoi.Services.Core
{
    public class ModuleService : BaseService, IModuleService
    {
        private DataContext _context;
        private BaseMongoDb<Module, string> BaseMongoDb;
        public ModuleService(
            IDonViService donViService,
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<Module, string>(_context.MODULE);
        }


        public async Task<dynamic> GetAllCore()
        {

            var filter = Builders<Menu>.Filter.Where(x => !x.IsDeleted);
            var list = await _context.MENU.Aggregate().Match(filter).SortByDescending(x => x.Sort)
                .Project<MenuListShort1>(MenuModuel.Projection_BasicCommon).ToListAsync();
            return list;
        }

        public async Task<dynamic> GetById(IdFromBodyModel fromBodyModel)
        {
            try
            {
                var filter = Builders<Menu>.Filter.Eq("IsDeleted", false);
                filter = Builders<Menu>.Filter.And(filter, Builders<Menu>.Filter.Eq("Id", fromBodyModel.Id));
                var data = await _context.MENU.Find(filter).FirstOrDefaultAsync();
                if (data == default)
                    throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
                return data;
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

        public async Task<dynamic> GetPagingCore(PagingParam pagingParam)
        {
            try
            {
                PagingModel<dynamic> result = new PagingModel<dynamic>();
                var builder = Builders<Menu>.Filter;
                var filter = builder.Empty;
                filter = builder.And(filter, builder.Eq("IsDeleted", false));
                if (!String.IsNullOrEmpty(pagingParam.Content))
                {
                    filter = builder.And(filter,
                        (builder.Regex("UnsignedName", FormatterString.ConvertToUnsign(pagingParam.Content)) |
                         builder.Regex("Code", pagingParam.Content)
                        ));
                }
                result.TotalRows = await _context.MENU.CountDocumentsAsync(filter);
                var list = await _context.MENU.Find(filter)
                    .Skip(pagingParam.Skip)
                    .Limit(pagingParam.Limit)
                    .Project<MenuListShort1>(MenuModuel.Projection_BasicCommon)
                    .ToListAsync();
                result.Data = list.ToList();
                return result;
            }
            catch (ResponseMessageException e)
            {
                new ResultMessageResponse().WithCode(e.ResultCode)
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
            return null;
        }
    }
}