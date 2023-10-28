using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using ThapMuoi.Exceptions;
using ThapMuoi.Extensions;
using ThapMuoi.FromBodyModels;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Services.Core
{
    public class UnitRoleService : BaseService, IUnitRoleService
    {
        private DataContext _context;
        private BaseMongoDb<UnitRole, string> BaseMongoDb;
        private BaseMongoDb<User, string> BaseMongoDbUser;
        private IDonViService _donViService;
        private IMenuService _menuService;

        public UnitRoleService( DataContext context,
            IHttpContextAccessor contextAccessor,
            IMenuService menuService,
            IDonViService donViService
            )
            : base(context, contextAccessor)
        {
            _context = context;
            _donViService = donViService;
            BaseMongoDb = new BaseMongoDb<UnitRole, string>(_context.UNIT_ROLE);
        }

        public async Task<dynamic> Create(UnitRole model)
        {
            try
            {
                if (model == default) throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

                var checkName = _context.UNIT_ROLE.Find(x => x.Name == model.Name && !x.IsDeleted).FirstOrDefault();

                if (checkName != default) throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);


                var entity = new UnitRole
                {
                    Id = BsonObjectId.GenerateNewId().ToString(),
                    Name = model.Name,
                    Code = model.Code,
                    Level = model.Level,
                    Sort = model.Sort,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserName,
                    DonVis = model.DonVis,
                    UnitUsing = model.UnitUsing,
                    IsSpecialUnit = model.IsSpecialUnit,
                    IsOnlySeeMe = model.IsOnlySeeMe,
                };
                entity.SetUnitRole(_donViService);
                var result = await BaseMongoDb.CreateAsync(entity);
                if (result.Entity.Id == default || !result.Success)
                    throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
                return new UnitRoleShortShow(entity);
            }
            catch (ResponseMessageException e)
            {
                new ResultMessageResponse()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString)
                    .WithDetail(e.Error);
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
        public async Task<dynamic> Update(UnitRole model)
        {
            try
            {
                if (model == default)  throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

            var entity = _context.UNIT_ROLE.Find(x => x.Id == model.Id).FirstOrDefault();
            if (entity == default) throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);


           
            var checkName = _context.UNIT_ROLE.Find(x => x.Id != model.Id
                                                            && x.Name.ToLower() == model.Name.ToLower()
                                                            && !x.IsDeleted 
            ).FirstOrDefault();
            
            if (checkName != default) throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);

            

            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.Sort = model.Sort;
            entity.IsSpecialUnit = model.IsSpecialUnit;
            entity.IsOnlySeeMe = model.IsOnlySeeMe;
            entity.Level = model.Level;
            entity.ModifiedAt = DateTime.Now;
            entity.CreatedBy = CurrentUserName;
            entity.SetUnitRole(_donViService);
            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
                throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
            
            
            await UpdateRolesInUser(entity);
            return new UnitRoleShortShow(entity);;
            }
            catch (ResponseMessageException e)
            {
                new ResultMessageResponse().WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString)
                    .WithDetail(e.Error);
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
        
        
        //   Lay quyen dua theo username 
        public async  Task<dynamic> GetByCurrentUser()
        {
            if (CurrentUser == null)
                throw new ResponseMessageException().WithException(DefaultCode.EXCEPTION);
            var result =  _context.UNIT_ROLE.AsQueryable().Where(x => x.IsDeleted != true &&
            ((x.UnitUsing != null && x.UnitUsingIds.Equals(CurrentUser.DonVi.Id))
            || x.UnitUsing == null || 
            x.UnitUsing.Count() == 0
            ) && ( CurrentUser.LevelUnitRole !=  null && x.Level >= CurrentUser.LevelUnitRole)
            )
                .Select(x => 
                new UnitRoleShort(
                    x.Id , x.Name , x.Code,x.IsSpecialUnit, x.IsOnlySeeMe,
                    x.DonViIds , x.ListMenu,x.DonVis,  x.UnitUsingIds,x.UnitUsing , x.ListAction , x.Level
                )).ToList();
            if (CurrentUser.IsOnlySeeMe)
            {
                return result.Where(x => x.IsOnlySeeMe == true).ToList();
            }

            return result;
        }
        
        public async Task<dynamic> UpdateAction(IdFromBodyUnitRole model)
        {
            try
            {
                if (model == default) throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

                var entity = _context.UNIT_ROLE.Find(x => x.Id == model.Id).FirstOrDefault();
                if (entity == default) throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
                if (model.ListAction != null)
                {
                    entity.SetAction(model.ListAction);
                }

                entity.ModifiedAt = DateTime.Now;
                var result = await BaseMongoDb.UpdateAsync(entity);
                if (!result.Success) throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
                return entity.ListAction;
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

        private async Task UpdateRolesInUser(UnitRole role)
        {
            var filterUser = Builders<User>.Filter.Eq(x => x.UnitRoleId, role.Id);
          //  var a = _context.Users.CountDocumentsAsync(filterUser).Result;
            var update = Builders<User>.Update
                .Set(s => s.UnitRoleId, role.Id)
                .Set(s=>s.UnitRoleCode,role.Code)
                .Set(s=>s.LevelUnitRole, role.Level)
                .Set(s=>s.IsOnlySeeMe,role.IsOnlySeeMe)
                .Set(s => s.DonViIdsByUnitRole, role.DonViIds)
                .Set(s => s.UnitTreeRange, role.DonVis)
                .Set(s => s.ModifiedBy, CurrentUser.UserName)
                .Set(s => s.ModifiedAt, DateTime.Now);
            UpdateResult actionResult
                = await _context.USERS.UpdateManyAsync(filterUser, update);
        }
        
    }
    
}