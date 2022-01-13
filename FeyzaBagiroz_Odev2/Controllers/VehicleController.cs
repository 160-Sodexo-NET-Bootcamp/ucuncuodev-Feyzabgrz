using AutoMapper;
using Data.Dtos;
using Data.UnitOfWork;
using Entities.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FeyzaBagiroz_Odev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<VehicleController> _logger;
        IMapper _mapper;

        public VehicleController(ILogger<VehicleController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


    


        /* Sistemdeki tüm araçları listeleyen endpoint */
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {

            try
            {

                var result = await _unitOfWork.Vehicle.GetAll();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /* Yeni bir araç ekleyen endpoint */

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDto entity)
        {
            try
            {
                Vehicle vehicle = _mapper.Map<Vehicle>(entity);
                
                var result = await _unitOfWork.Vehicle.Add(vehicle);
                _unitOfWork.Complete();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /* Araç bilgisi güncelleyen endpoint */


        [HttpPost("Update")]

        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDto entity)
        {

            try
            {
                Vehicle vehicle = _mapper.Map<Vehicle>(entity);

                var result = await _unitOfWork.Vehicle.Update(vehicle);
                _unitOfWork.Complete();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /*Aracı silen endpoint / Aracı silerken araca bağlı containerlarıda silecektir */

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleById(long id)
        {

            try
            {

                var result = _unitOfWork.Container.Where(x => x.VehicleId == id).ToList();

                if (result.Count != 0)
                {

                    foreach (var item in result)
                    {
                        var aa = _unitOfWork.Container.Delete(item.Id);


                    }
                }

                var response = _unitOfWork.Vehicle.Delete(id);

                _unitOfWork.Complete();

                return new JsonResult(response.Result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

   



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _unitOfWork.Vehicle.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
     
    }
}
