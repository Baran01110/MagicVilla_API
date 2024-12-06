﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas() 
        {
            return VillaStore.villaList;
        }
        
        [HttpGet("id")]
        public VillaDto GetVillas(int id) 
        {
            return VillaStore.villaList.FirstOrDefault(u=>u.ID==id);
        }
    }
}
