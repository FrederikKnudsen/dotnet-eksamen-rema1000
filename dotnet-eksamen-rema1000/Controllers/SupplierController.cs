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
    [Route("api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;
        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            var createdSupplier = await _supplierService.Create(supplier);

            if (createdSupplier == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("CreateSupplier", createdSupplier);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAll();

            if (suppliers == null)
            {
                return NotFound();
            }

            return Ok(suppliers);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var supplier = await _supplierService.Get(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public async Task<IActionResult> EditSupplier([FromBody] Supplier supplier)
        {
            var result = await _supplierService.Update(supplier);

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
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var deleteSuccess = await _supplierService.Delete(id);

            if (deleteSuccess)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
