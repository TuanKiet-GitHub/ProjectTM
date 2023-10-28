using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using ThapMuoi.Exceptions;
using ThapMuoi.Extensions;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;
using ThapMuoi.ViewModels;

namespace ThapMuoi.Services.Core
{
    public class DonViService : BaseService, IDonViService
    {
        private DataContext _context;
        private BaseMongoDb<DonVi, string> BaseMongoDb;
        private IMongoCollection<DonVi> _collection;
        private IMongoCollection<User> _collectionUser;

        public DonViService(DataContext context,
            IHttpContextAccessor contextAccessor)
            : base(context, contextAccessor)
        {
            _context = context;
            _collection = context.DONVI;
            _collectionUser = context.USERS;
            BaseMongoDb = new BaseMongoDb<DonVi, string>(_context.DONVI);
        }

        public async Task<dynamic> Create(DonVi model)
        {
            try
            {
                if (model == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

              

                var donVi = _collection.Find(x => x.MaCoQuan == model.MaCoQuan && !x.IsDeleted).FirstOrDefault();

                if (donVi != default)
                    throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);


                var entity = new DonVi()
                {
                    Id = BsonObjectId.GenerateNewId().ToString(),
                    Name = model.Name,
                    MaCoQuan = model.MaCoQuan,
                    CapDV = model.CapDV,
                    DonViCha = model.DonViCha,
                };
                var result = await BaseMongoDb.CreateAsync(entity);
                if (model.DonViCha != null)
                {
                    var builder = Builders<User>.Filter;
                    var filter = builder.Empty;
                    filter = builder.And(filter,
                        builder.Where(x => x.DonViIds != default && x.DonViIds.Any(s => s == model.DonViCha)));
                    var update = Builders<User>.Update.Push(y => y.DonViIds, entity.Id);
                    var resultUser = _collectionUser.UpdateManyAsync(filter, update);
                    if (resultUser == default)
                    {
                        throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
                    }
                }

                if (result.Entity.Id == default || !result.Success)
                    throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);

                return entity;
            }  
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString)
                    .WithDetail(e.Error);
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

        public async Task<dynamic> Update(DonVi model)
        {
            try
            {
                if (model == default)
                    throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            
            
            
                var donVi = _collection.Find(x => x.Id != model.Id && x.MaCoQuan == model.MaCoQuan && !x.IsDeleted).FirstOrDefault();

                if (donVi != default)
                    throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);

                
                var entity = _collection.Find(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefault();

                if (entity == default)
                    throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);

                entity.Name = model.Name;
                entity.MaCoQuan = model.MaCoQuan;
                entity.CapDV = model.CapDV;
                entity.DonViCha = model.DonViCha;

                var result = await BaseMongoDb.UpdateAsync(entity);
                if (!result.Success)
                    throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);

                return entity;
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ResultString)
                    .WithDetail(e.Error);
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

     
        
        
        

        public async Task<dynamic> GetTree()
        {
            List<DonViTreeVM> list = new List<DonViTreeVM>();
            List<DonViShort> listUnitTreeRange = new List<DonViShort>();
            
            if (CurrentUser.UnitTreeRange == default || CurrentUser.UnitTreeRange.Count == 0)
            {
                listUnitTreeRange.Add(CurrentUser.DonVi);
            }
            else
            {
                listUnitTreeRange.AddRange(CurrentUser.UnitTreeRange);
            }
            foreach (var donViShort in listUnitTreeRange)
            {
                var listDonVi = await _context.DONVI.Find(x  => !x.IsDeleted).ToListAsync();
                var parents = listDonVi.Where(x => x.Id == donViShort.Id).ToList();
                var listId = new List<String>();
                foreach (var item in parents)
                {
                    DonViTreeVM donVi = new DonViTreeVM(item);
                        list.Add(donVi);
                        listId.Add(donVi.Id);
                        GetLoopItem(ref list,listDonVi, donVi);
                }
            }
            return list;
        }
        
        
        public async Task<dynamic> GetTreeAll()
        {
            List<DonViTreeVM> list = new List<DonViTreeVM>();
         
                var listDonVi = await _collection.Find(x  => !x.IsDeleted).ToListAsync();
                var parents = listDonVi.Where(x => x.DonViCha == null).ToList();
                var listId = new List<String>();
                foreach (var item in parents)
                {
                    
                        DonViTreeVM donVi = new DonViTreeVM(item);
                        list.Add(donVi);
                        GetLoopItem(ref list,listDonVi, donVi);
                }
                return list;
        }
        
        
        
        
        public async Task<dynamic> GetTreeFlatten()
        {
            List<DonViTreeVMGetAll> list = new List<DonViTreeVMGetAll>();
         
            var listDonVi = await _collection.Find(x  => !x.IsDeleted).ToListAsync();
            var parents = listDonVi.Where(x => x.DonViCha == null).ToList();
            var listId = new List<String>();
            foreach (var item in parents)
            {
                    
                DonViTreeVMGetAll donVi = new DonViTreeVMGetAll(item);
                list.Add(donVi);
                GetLoopItemGetAll(ref list,listDonVi, donVi);
            }
            return list;
        }
        
        
        
        
        
        
        public async Task<dynamic> GetTreeSelected()
        {
            List<DonViSelectedTreeVM> list = new List<DonViSelectedTreeVM>();
         
            var listDonVi = await _collection.Find(x  => !x.IsDeleted).ToListAsync();
            var parents = listDonVi.Where(x => x.DonViCha == null).ToList();
            var listId = new List<String>();
            foreach (var item in parents)
            {
                    
                DonViSelectedTreeVM donVi = new DonViSelectedTreeVM(item);
                list.Add(donVi);
                GetLoopItemSelected(ref list,listDonVi, donVi);
            }
            return list;
        }

        private List<DonViTreeVM> GetLoopItem(ref List<DonViTreeVM> list, List<DonVi> items, DonViTreeVM target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.DonViCha == target.Id).OrderBy(x=>x.Name).ToList();
                if (coquan.Count > 0)
                {
                    target.Children = new List<DonViTreeVM>();
                    foreach (var item in coquan)
                    {
                        DonViTreeVM itemDV = new DonViTreeVM(item);
                        target.Children.Add(itemDV);
                        GetLoopItem(ref list, items, itemDV);
                    }
                }

                return null;
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
            return null;
        }


        
        
        
        
        private List<DonViTreeVM> GetLoopItemGetAll(ref List<DonViTreeVMGetAll> list, List<DonVi> items, DonViTreeVMGetAll target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.DonViCha == target.Id).OrderBy(x=>x.Name).ToList();
                if (coquan.Count > 0)
                {
                    foreach (var item in coquan)
                    {
                        DonViTreeVMGetAll itemDV = new DonViTreeVMGetAll(item);
                        list.Add(itemDV);
                        GetLoopItemGetAll(ref list, items, itemDV);
                    }
                }

                return null;
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
            return null;
        }



        private List<DonViTreeVM> GetLoopItemSelected(ref List<DonViSelectedTreeVM> list, List<DonVi> items,DonViSelectedTreeVM target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.DonViCha == target.Id).OrderBy(x=>x.Name).ToList();
                if (coquan.Count > 0)
                {
                    target.Children = new List<DonViSelectedTreeVM>();
                    foreach (var item in coquan)
                    {
                        DonViSelectedTreeVM itemDV = new DonViSelectedTreeVM(item);
                        target.Children.Add(itemDV);
                        GetLoopItemSelected(ref list, items, itemDV);
                    }
                }

                return null;
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
            return null;
        }


        private Task<List<DonViTreeVM>> GetLoopItemDonViSuDung(ref List<DonViTreeVM> list, DonViTreeVM target)
        {
            try
            {
                var coquan =  _context.DONVI.Find(x => x.DonViCha == target.Id && x.IsDeleted == false).ToList();
                if (coquan.Count > 0)
                {
                    target.Children = new List<DonViTreeVM>();
                    foreach (var item in coquan)
                    {
                        DonViTreeVM itemDV = new DonViTreeVM(item);
                        target.Children.Add(itemDV);
                        GetLoopItemDonViSuDung(ref list, itemDV);
                    }
                }

                return null;
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

            return null;
        }
                
        
        
        
        
        
        
        
        
        
        public List<string> GetListDonViId(string coQuanId)
        {
            try
            {
                var listDonVi = _context.DONVI.AsQueryable().Where(x=>x.IsDeleted == false).ToList();
                var parents = listDonVi.Where(x => x.Id == coQuanId).ToList();
                List<string> list = new List<string>();
                foreach (var item in parents)
                {
                    list.Add(item.Id);
                    GetLoopItemCoQuan(ref list, listDonVi, item.Id);
                }

                return list;
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
        
        
        
        
        
      

        private List<string> GetLoopItemCoQuan(ref List<string> list, List<DonVi> items, string target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.DonViCha == target).ToList();
                if (coquan.Count > 0)
                {
                    foreach (var item in coquan)
                    {
                        list.Add(item.Id);
                        GetLoopItemCoQuan(ref list, items, item.Id);
                    }
                }
                return null;
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

            return null;
        }
    }
}