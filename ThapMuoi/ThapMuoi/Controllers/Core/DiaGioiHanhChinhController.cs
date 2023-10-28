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
    public class DiaGioiHanhChinhController : DefaultReposityController<DiaGioiHanhChinhModel>
    {
        private DataContext _dataContext;
        private static string NameCollection = DefaultNameCollection.DIAGIOIHANHCHINH;
        private IDiaGioiHanhChinhService _diaGioiHanhChinhService;

        public DiaGioiHanhChinhController(DataContext context, IDiaGioiHanhChinhService service) : base(context, NameCollection)
        {
            _diaGioiHanhChinhService = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]  DiaGioiHanhChinhModel model)
        {
            try
            {
                var response = await   _diaGioiHanhChinhService.Create(model);
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
        public async Task<IActionResult> Update([FromBody] DiaGioiHanhChinhModel model)
        {
            try
            {
                var response = await   _diaGioiHanhChinhService.Update(model);

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
        [Route("get-list-Child/{code}")]
        public async Task<IActionResult> GetListChild(string code)
        {
            try
            {
                var response = await _diaGioiHanhChinhService.GetListChildByCode(code);
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
        [Route("get-list-by-level/{id}")]
        public async Task<IActionResult> GetListByLevel(int  id = 0)
        {
            try
            {
                var response = await _diaGioiHanhChinhService.GetListByLevel(id);
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
        
        
        
     /*   [HttpPost]
        [Route("get-paging-params-core")]
        public override async Task<IActionResult> GetPagingCore([FromBody] PagingParam pagingParam)
        {
            try
            {
                var response = await _diaGioiHanhChinhService.GetListByLevel(0);
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
        }*/
        
    }
}
