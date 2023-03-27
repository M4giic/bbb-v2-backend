using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OperationService.Application;
using OperationService.Request.Request.Type;

/*
 *  Tags or Types???
 * 
 *  Tags maybe moved to a different microservice
 *      Pros:
 *          - Tags are a another layer of complexity for this application
 *          - Another DB will be beneficial 
 *      Cons:
 *          - This funcionality is tightly coupled with operations
 *          - Relation will be done between 2 dbs. Seems complex
 */

namespace OperationService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeController : ControllerBase
{
    //todo tags should be limited to a single user 
    private readonly IMapper _mapper;
    private readonly ITypeService _typeService;

    public TypeController(IMapper mapper, ITypeService typeService)
    {
        _mapper = mapper;
        _typeService = typeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewType(AddNewTypeRequest request)
    {
        try
        {
            var result  = _typeService.AddNewType(request);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{typeId:int}")]
    public async Task<IActionResult> UpdateType(UpdateTypeRequest request, [FromRoute] int typeId)
    {
        try
        {
            request.TypeId = typeId;
            var result  = await _typeService.UpdateType(request);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet]
    [Route("{typeFamilyId:int}")]
    public async Task<IActionResult> GetAllTypes([FromQuery] string typeFamilyName, [FromQuery] int? typeFamilyId)
    {
        try
        {
            if (typeFamilyName is not null)
            {
                var result  = await _typeService.GetTypesByTypesFamilyName(typeFamilyName);
                return Ok(result);
            }
            else if(typeFamilyId is not null)
            {
                var result  =await _typeService.GetTypesByTypesFamilyId((int)typeFamilyId);
                return Ok(result);
            }
            
            return BadRequest("Incorrect Query Parameters");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{typeFamilyId:int}")]
    public async Task<IActionResult> RemoveTypeFamily([FromQuery] int familyTypeId)
    {
        try
        {
            _typeService.DisableFamilyType(familyTypeId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            
        }
    }
    

    [HttpPost]
    [Route("typeFamilies")]
    public async Task<IActionResult> AddNewTypeFamily(AddNewTypeFamilyRequest request)
    {
        try
        {
            var result = await _typeService.AddNewTypeFamily(request);

            //todo can be changed to created later 
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("typeFamilies/{typeFamilyId:int}")]
    public async Task<IActionResult> UpdateTypeFamily(UpdateTypeFamilyRequest request, [FromRoute] int typeFamilyId)
    {
        try
        {
            request.Id = typeFamilyId;
            var result =await _typeService.UpdateTypeFamily(request);

            //todo can be changed to created later 
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
    
    [HttpGet]
    [Route("typeFamilies")]
    public async Task<IActionResult> GetAllTypeFamily()
    {
        try
        {
            var result = await _typeService.GetAllTypesFamilies();

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}