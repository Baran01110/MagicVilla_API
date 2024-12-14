using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas() 
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villaList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDto>> GetVillas(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa =await _db.Villas.FirstOrDefaultAsync(u => u.ID == id);
            if (villa == null) { return NotFound(); }
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody]VillaCreateDto createDto) 
        {
            if(await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower())!= null) 
            {
                ModelState.AddModelError("", "Villa Already Exists!");
                return BadRequest(ModelState);
            }

         if(createDto == null) 
            {
                return BadRequest(createDto);
            }
         //if(villaDto.ID > 0) 
         //   {
         //       return StatusCode(StatusCodes.Status500InternalServerError);
         //   }
         Villa model = _mapper.Map<Villa>(createDto);

            //Villa model = new()
            //{
            //    Amenity = createDto.Amenity,
            //    Details = createDto.Details,
            //    ImageUrl = createDto.ImageUrl,
            //    Name = createDto.Name,
            //    Occupancy = createDto.Occupancy,
            //    Rate = createDto.Rate,
            //    Sqft = createDto.Sqft,
            //};
           await _db.Villas.AddAsync(model);
           await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.ID }, model);
       }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id) 
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.ID == id);
            if (villa == null)
            {
                return NotFound();
            }
           _db.Villas.Remove(villa);
           await _db.SaveChangesAsync();
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.ID)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(u => u.ID == id);
            //villa.Name = villaDto.Name;
            //villa.Occupancy = villaDto.Occupancy;
            //villa.Sqft = villaDto.Sqft;
            Villa model = _mapper.Map<Villa>(updateDto);

            //Villa model = new()
            //{
            //    Amenity = updateDto.Amenity,
            //    Details = updateDto.Details,
            //    ID = villaDto.ID,
            //    ImageUrl = villaDto.ImageUrl,
            //    Name = villaDto.Name,
            //    Occupancy = villaDto.Occupancy,
            //    Rate = villaDto.Rate,
            //    Sqft = villaDto.Sqft,
            //};
            _db.Villas.Update(model);
          await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.ID == id);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            //VillaUpdateDto villaDto = new()
            //{
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    ID = villa.ID,
            //    ImageUrl = villa.ImageUrl,
            //    Name = villa.Name,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft,  
            //};

            if (villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villaDto, ModelState);
            Villa model = _mapper.Map<Villa>(villaDto);

            //Villa model = new()
            //{
            //    Amenity = villaDto.Amenity,
            //    Details = villaDto.Details,
            //    ID = villaDto.ID,
            //    ImageUrl = villaDto.ImageUrl,
            //    Name = villaDto.Name,
            //    Occupancy = villaDto.Occupancy,
            //    Rate = villaDto.Rate,
            //    Sqft = villaDto.Sqft,
            //};

            _db.Villas.Update(model);
           await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
