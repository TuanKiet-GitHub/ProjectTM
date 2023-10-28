using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThapMuoi.Constants;
using ThapMuoi.Exceptions;
using ThapMuoi.FromBodyModels;
using ThapMuoi.Helpers;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Controllers.Core
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UnitRoleController: DefaultReposityController<UnitRole>
    {
        private IUnitRoleService _service;
        private DataContext _dataContext;
        private static string NameCollection = DefaultNameCollection.UNIT_ROLE;
        public UnitRoleController(DataContext context, IUnitRoleService service) : base(context, NameCollection)
        {
            _service = service;
            _dataContext = context;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UnitRole model)
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
        public async Task<IActionResult> Update([FromBody] UnitRole model)
        {
            try
            {
                var data = await _service.Update(model);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.UPDATE_SUCCESS)
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
        [Route("update-action")]
        public async Task<IActionResult> UpdateAction([FromBody] IdFromBodyUnitRole model)
        {
            try
            {
                var response = await _service.UpdateAction(model);
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
                        .WithMessage(ex.ResultString)
                );
            }
        }
    }
}
