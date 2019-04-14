using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detego.WebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detego.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            return Ok(categories);
        }

    }
}