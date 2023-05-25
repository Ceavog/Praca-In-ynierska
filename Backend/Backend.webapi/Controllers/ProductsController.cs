using System.Diagnostics;
using Backend.Services.Interface;
using Backend.Shared.Dtos;
using Backend.Shared.Dtos.ProductDtos;
using Backend.Shared.Exceptions.ProductExceptions;
using Backend.Shared.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace Backend.webapi;


[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
//[Authorize]
public class ProductsController : BaseController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService, IUserService userService) : base(userService)
    {
        _productService = productService;
    }

    [HttpGet("GetAllProductsByUserId")]
    public ActionResult<IEnumerable<GetProductDto>> GetAllProductsByUserId()
    {
        try
        {
            return Ok(Json(_productService.GetAllProductsByUserId(AuthUser)));
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetProductById")]
    public ActionResult<GetProductDto> GetProductById(int id)
    {
        try
        {
            return Ok(Json(_productService.GetProductById(id)));
        }
        catch (Exception e) when (e is ProductWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("AddProduct")]
    public ActionResult<AddProductDto> AddProduct(AddProductDto productDto)
    {
        try
        {
            var addedProduct = _productService.AddProduct(productDto, AuthUser);
            return Created("/AddProduct", Json(addedProduct));
        }
        catch (Exception e) when (e is ProductWithThisNameAlreadyExistsForThisUserException)
        {
            return Conflict();
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

    [HttpPut("UpdateProduct")]
    public ActionResult<UpdateProductDto> UpdateProduct(UpdateProductDto productDto)
    {
        try
        {
            var updatedProduct = _productService.UpdateProduct(productDto, AuthUser);
            return Ok(Json(updatedProduct));
        }
        catch (Exception e) when (e is ProductWithThisNameAlreadyExistsForThisUserException)
        {
            return Conflict();
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is ProductWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("DeleteProductById")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            return Ok(Json(_productService.DeleteProduct(id)));
        }
        catch (Exception e) when (e is UserWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is ProductWithGivenIdDoesNotExistsException)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("AddManyProducts")]
    public IActionResult AddRange(ICollection<ProductDto> productsDto)
    {
        throw new NotImplementedException();
    }
    
    
}