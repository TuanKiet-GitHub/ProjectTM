using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThapMuoi.Constants;
using ThapMuoi.Exceptions;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Controllers.Core
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DonViController : DefaultReposityController<DonVi>

    {
        private IDonViService _service;
        private DataContext _dataContext;
        private static string NameCollection = DefaultNameCollection.DONVI;
        public DonViController(DataContext context, IDonViService service) : base(context, NameCollection)
        {
            _service = service;
            _dataContext = context;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] DonVi model)
        {
            try
            {
                var response = await _service.Create(model);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.CREATE_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] DonVi model)
        {
            try
            {
                var response = await _service.Update(model);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.UPDATE_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }

      
        
        
        
        

        
        
        

        
        
        
        
        
        [HttpGet]
        [Route("getTree")]
        public async Task<IActionResult> GetTree()
        {
            try
            {
                var response = await _service.GetTree();
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }
        [HttpGet]
        [Route("getTreeAll")]
        public async Task<IActionResult> GetTreeAll()
        {
            try
            {
                var response = await _service.GetTreeAll();
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }
        
        
        [HttpGet]
        [Route("getTreeFlatten")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetTreeFlatten();
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }
        
        
        
        
        [HttpGet]
        [Route("getTreeSelected")]
        public async Task<IActionResult> GetTreeSelected()
        {
            try
            {
                var response = await _service.GetTreeSelected();
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }
        
        

        
        
        
        
        
        
    }
}
