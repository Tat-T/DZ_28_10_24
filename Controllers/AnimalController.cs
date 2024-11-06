using Microsoft.AspNetCore.Mvc;
using AnimalApp.Models;
using AnimalApp.Services;

namespace AnimalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IOutputService _outputService;

        public AnimalController(IOutputService outputService)
        {
            _outputService = outputService;
        }

        // Возвращаем список всех животных
        [HttpGet]
        public ActionResult<List<Animal>> GetAnimals()
        {
            var animals = new List<Animal>
            {
                new Animal { Name = "Dog", Sound = "Bark" },
                new Animal { Name = "Cat", Sound = "Meow" }
            };
            return Ok(animals);
        }

        // Сохраняем конкретное животное
        [HttpPost("SaveAnimal")]
        public IActionResult SaveAnimal([FromBody] Animal animal)
        {
            var animals = new List<Animal> { animal };
            _outputService.OutputToFile(animals, "json"); // Сохраняем как JSON (можно изменить формат по необходимости)
            return Ok();
        }

        // Сохраняем всех животных в формате JSON или XML
       [HttpPost("output")]
        public IActionResult OutputAnimals([FromQuery] string format)
        {
            var animals = new List<Animal>
            {
                new Animal { Name = "Dog", Sound = "Bark" },
                new Animal { Name = "Cat", Sound = "Meow" }
            };
            _outputService.OutputToFile(animals, format);
            return Ok();
        }
    }
}
