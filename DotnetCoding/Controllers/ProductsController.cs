using DotnetCoding.Contracts;
using DotnetCoding.Core.Models;
using DotnetCoding.Infrastructure.Extensions;
using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productDetails = await _productService.GetProductById(id);
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductRequest request)
        {
            var product = await _productService.PutProduct(id, request);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductRequest request)
        {
            var productResponse = await _productService.PostProduct(request);
            if (productResponse == null)
            {
                return NotFound();
            }
            return StatusCode(201, productResponse);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);

            return NoContent();
        }

        [HttpGet("ProductApprovalQueue")]
        public async Task<IActionResult> GetProductApprovalQueue()
        {

            var productAuditDetails = await _productService.GetProductApprovalQueue();
            if (productAuditDetails == null)
            {
                return NotFound();
            }
            return Ok(productAuditDetails);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchProducts([FromBody]Contracts.SearchRequest request)
        {
            var approvedProducts = await _productService.SearchProducts(request.ToDomain());
            if (approvedProducts == null)
            {
                return NotFound();
            }
            return Ok(approvedProducts);
        }

        [HttpPost("Review")]
        public async Task<IActionResult> ReviewProductRequest(Contracts.ProductReviewRequest request)
        {

            await _productService.ReviewProductRequest(request.AsProductReviewRequest());

            return Ok();
        }

    }
}
