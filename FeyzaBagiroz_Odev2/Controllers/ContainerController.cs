using AutoMapper;
using Data.Dtos;
using Data.UnitOfWork;
using Entities.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeyzaBagiroz_Odev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ContainerController> _logger;
        IMapper _mapper;

        public ContainerController(ILogger<ContainerController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /* Sistemdeki tüm containerları listeleyen endpoint */

        [HttpGet]
        public async Task<IActionResult> GetAllContainers()
        {
            //gelen veriyi jsonResult tipinde geri döndüm.
            try
            {
                var result = await _unitOfWork.Container.GetAll();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /* Sisteme yeni bir container ekleyen endpoint */
        [HttpPost]
        public async Task<IActionResult> AddContainer([FromBody] ContainerDto model)
        {

            try
            {

                Container container = _mapper.Map<Container>(model);

                var result = await _unitOfWork.Container.Add(container);
                _unitOfWork.Complete();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        /* Container bilgisini güncelleyen api / güncelleme sırasında vehicle ıd güncellenmeyecektir */

        [HttpPost("Update")]

        public async Task<IActionResult> UpdateContainer([FromBody] ContainerDto model)
        {
            //Burada vehicleId alanının günellenmemesi için bir viewmodel oluşturdum ve parametre olarak bunu gönderdim
            //parametre olarak gönderilen id nin boş olma ve ya sistemde olmama durumu control edildi
            try
            {
                if (model != null && model.Id > 0)
                {
                    var result = await _unitOfWork.Container.GetById(model.Id);

                    if (result.Data != null)
                    {
                        //ContainerName alanı boş değil ise güncelleme işlemi yapacak
                        if (!string.IsNullOrWhiteSpace(model.ContainerName))
                            result.Data.ContainerName = model.ContainerName;

                        result.Data.Latitude = model.Latitude;
                        result.Data.Longitude = model.Longitude;

                        Container container = _mapper.Map<Container>(model);

                        var response = await _unitOfWork.Container.Update(result.Data);
                        _unitOfWork.Complete();

                        return new JsonResult(result);
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return NotFound();
        }

        /* Container silecek endpoint */

        [HttpDelete]
        public async Task<IActionResult> DeleteContainerById(long id)
        {
            try
            {
                var result = await _unitOfWork.Container.Delete(id);
                _unitOfWork.Complete();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        /* VEHİCLEID İLE İSTEK YAPILDIĞINDA O ARACA AİT TÜM CONTAİNERLARI LİSTELEYEN APİ */

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByVehicleId(long id)
        {
            try
            {
                var result = _unitOfWork.Container.Where(x => x.VehicleId == id).ToList();

                if (result is null)
                {
                    return NotFound("Bulunamadı");
                }
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                return null;
            }
        }




        /* VehicleId ve n parametresi alarak araca ait containerlerı eşit eleman olacak şekilde n kümeye ayırıp tek response döndüre api
   */
        [HttpGet("{vehicleId} / {n}")]
        public async Task<IActionResult> GroupByContainer(long vehicleId, int n)
        {
            try
            {
                var list = new List<List<Container>>();
                var result = _unitOfWork.Container.Where(x => x.VehicleId == vehicleId).ToList();
                var elementCount = result.Count / n;
                var index = 0;
                for (int i = 0; i < n; i++)
                {
                    list.Add(result.GetRange(index, elementCount));
                    index += elementCount;
                }

                if (result is null)
                {
                    return NotFound();
                }

                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                return null;
            }

        }













    }
}
