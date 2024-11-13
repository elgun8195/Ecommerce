using AzureProject.Business.Concrete;
using AzureProject.Entity.Concrete;
using AzureProject.Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AzureProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly SliderManager _sliderManager;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpClientFactory _httpClientFactory;
        public SliderController(SliderManager sliderManager, IWebHostEnvironment env, IHttpClientFactory httpClientFactory)
        {
            _sliderManager = sliderManager;
            _env = env;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var sliders = _sliderManager.GetList();
            return Ok(sliders);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var slider = _sliderManager.GetByIdSlider(id);
            if (slider == null) { return NotFound(); }
            return Ok(slider);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] SliderReturnDto slider)
        {
            var existSlider = _sliderManager.GetByIdSlider(id);

            if (existSlider == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(slider.Title))
            {
                if (_sliderManager.GetList().Any(x => x.Title.ToUpper().Trim() == slider.Title.ToUpper().Trim() && x.Id != id))
                {
                    return StatusCode(409);
                }
                existSlider.Title = slider.Title;
            }

            if (!string.IsNullOrWhiteSpace(slider.Description))
            {
                existSlider.Description = slider.Description;
            }

            // Yeni fotoğraf varsa, eski fotoğrafı sil və yenisini yüklə
            if (slider.Photo != null && slider.Photo.Length > 0)
            {
                // Eski fotoğrafı sil
                if (!string.IsNullOrEmpty(existSlider.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/slider", existSlider.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                 
                var uniqueFileName = $"{Guid.NewGuid()}_{slider.Photo.FileName}";
                var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/slider", uniqueFileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    slider.Photo.CopyTo(stream);
                }

                // Yeni dosya yolunu kaydet
                existSlider.ImageUrl =  uniqueFileName;
            }

            existSlider.UpdatedTime = DateTime.UtcNow;
            _sliderManager.SliderUpdate(existSlider);
            return NoContent();
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] SliderReturnDto sliderDto)
        {
            // Slayder başlığının təkrarı yoxlanır
            if (_sliderManager.GetList().Any(g => g.Title.ToUpper().Trim() == sliderDto.Title.Trim().ToUpper()))
                return StatusCode(409, "Bu başlıqla artıq slayder mövcuddur.");

            var slider = new Slider
            {
                Title = sliderDto.Title,
                Description = sliderDto.Description,
                CreatedTime = DateTime.Now
            };

            if (sliderDto.Photo != null && sliderDto.Photo.Length > 0)
            {
                // Şəkil üçün unikal ad yaratmaq (GUID əlavə etməklə)
                var uniqueFileName = $"{Guid.NewGuid()}_{sliderDto.Photo.FileName}";
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/slider", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await sliderDto.Photo.CopyToAsync(stream);
                }

                slider.ImageUrl =uniqueFileName;
            }

            // Slider obyektini bazaya əlavə edirik
            _sliderManager.SliderAdd(slider);
            return StatusCode(201, slider);
        }


    }
}
