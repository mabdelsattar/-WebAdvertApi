using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using AdvertAPI.Sevices;
using Microsoft.AspNetCore.Mvc;

namespace AdvertAPI.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    //if you want another controller it will be another controller with v2
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;
        public AdvertController(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        //to identify what is response type 
        [ProducesResponseType(400)]
        [ProducesResponseType(200,Type= typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId;
            try {
               recordId = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex) 
            {
                return StatusCode(500,ex.Message);
              
            }

            return StatusCode(201,new CreateAdvertResponse() { Id = recordId });
        }


        [HttpPut]
        [Route("Confirm")]
        //to identify what is response type 
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            string recordId;
            try
            {
                 await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return new OkResult();
        }
    }
}