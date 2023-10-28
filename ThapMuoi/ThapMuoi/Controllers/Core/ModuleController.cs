using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThapMuoi.Constants;
using ThapMuoi.Exceptions;
using ThapMuoi.FromBodyModels;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;
using ThapMuoi.Models.PagingParam;

namespace ThapMuoi.Controllers.Core
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ModuleController : DefaultReposityController<Module>
    {
        private readonly IModuleService _service;
        private DataContext _dataContext;
        private static string NameCollection = DefaultNameCollection.MODULE;
        public ModuleController(DataContext context, IModuleService service) : base(context, NameCollection)
        {
            _service = service;
            _dataContext = context;
        }



        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAllCore();
                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString)
                );
            }
        }
        [HttpPost]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] IdFromBodyModel model)
        {
            try
            {
                var response = await _service.GetById(model);
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
                        .WithMessage(ex.ResultString)
                );
            }
        }

        [HttpPost]
        [Route("get-paging-params")]
        public async Task<IActionResult> GetPagingCore([FromBody] PagingParam pagingParam)
        {
            try
            {
                var response = await _service.GetPagingCore(pagingParam);
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
                        .WithMessage(ex.ResultString)
                );
            }
        }
    }
}