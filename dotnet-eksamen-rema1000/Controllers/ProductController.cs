using dotnet_eksamen_rema1000.Infrastructure;
using dotnet_eksamen_rema1000.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_eksamen_rema1000.Services;

namespace dotnet_eksamen_rema1000.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var createdProduct = await _productService.Create(product);

            if (createdProduct == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("CreateProduct", createdProduct);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAll();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public async Task<IActionResult> EditProduct([FromBody] Product product)
        {
            var result = await _productService.Update(product);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleteSuccess = await _productService.Delete(id);

            if (deleteSuccess)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
