using dotnet_eksamen_rema1000.Infrastructure;
using dotnet_eksamen_rema1000.Models;
using dotnet_eksamen_rema1000.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_eksamen_rema1000.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var createdCategory = await _categoryService.Create(category);

            if(createdCategory == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("CreateCategory", createdCategory);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAll();
            
            if(categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody] Category category)
        {
            var result = await _categoryService.Update(category);

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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleteSuccess = await _categoryService.Delete(id);

            if (deleteSuccess)
            {
                return Ok();
            }
            
            return NotFound();
        }
    }
}
