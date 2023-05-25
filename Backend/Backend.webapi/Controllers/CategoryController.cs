using Backend.Services.Interface;
using Backend.Shared.Dtos.CategoryDtos;
using Backend.Shared.Exceptions.CategoryExceptions;
using Backend.Shared.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.webapi;

[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
//[Authorize]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(
        ICategoryService categoryService,
        IUserService userService
        ) : base(userService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("AddCategory")]
    public IActionResult AddCategory(AddCategoryDto categoryDto)
    {
        try
        {
            var addedCategory = _categoryService.AddCategory(categoryDto, AuthUser);
            return Created("/AddCategory", addedCategory);
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAllCategoriesForUserId")]
    public ActionResult<IEnumerable<GetCategoryDto>> GetAllCategoriesForUserId(int id)
    {
        try
        {
            return Ok(_categoryService.GetAllCategoriesForUserId(AuthUser));
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPut("UpdateCategory")]
    public IActionResult UpdateCategory(UpdateCategoryDto categoryDto)
    {
        try
        {
            return Ok(_categoryService.UpdateCategory(categoryDto, AuthUser));
        }
        catch (Exception e) when (e is CategoryWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is CategoryWithGivenNameAndUserWithThisIdAlreadyExistsException)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("DeleteCategoryById")]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            return Ok(_categoryService.DeleteCategory(id));
        }
        catch (Exception e) when (e is CategoryWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetAllCategoriesWithProductsByUserId")]
    public IActionResult GetAllCategoriesWithProductsByUserId()
    {
        try
        {
            return Ok(_categoryService.GetCategoryWithProducts(AuthUser));
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch
        {
            return BadRequest();
        }
    }
}