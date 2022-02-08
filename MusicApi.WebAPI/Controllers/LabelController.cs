using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicApi.Models.Label;
using MusicApi.Service.Label;

namespace MusicApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        //CreateLabel endpoint
        [HttpPost]
        public async Task<IActionResult> CreateLabel([FromForm] LabelCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _labelService.CreateLabelAsync(request) == false)
                return BadRequest("Label could not be created.");

            return Ok("Label created successfully.");
        }

        // GetLabelById endpoint
        [HttpGet("{labelId:int}")]
        public async Task<IActionResult> GetLabelById([FromRoute] int labelId)
        {
            var detail = await _labelService.GetLabelByIdAsync(labelId);

            return detail is not null ? Ok(detail) : NotFound();
        }
    }
}