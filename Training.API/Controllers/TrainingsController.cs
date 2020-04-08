using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Lib.DataViewModel;
using Training.Lib.Services;

namespace Training.API.Controllers
{
    /// <summary>
    /// Training APIs to get all the existing trainings from database and to add new training.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingsController : ControllerBase
    {
        //Get training service via dependency injection
        private readonly ITrainingService _trainingService;
        /// <summary>
        /// Constructor gets the dependent TrainingService instance from IoC via dependency injection
        /// </summary>
        /// <param name="trainingService"></param>
        public TrainingsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }
        /// <summary>
        /// Get all the existing trainings from database 
        /// </summary>
        /// <returns></returns>
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<List<WDSTraningViewModel>>> Get()
        {
            return Ok(await _trainingService.GetTrainingsAsync());
        }
        /// <summary>
        /// Add new training to the database
        /// </summary>
        /// <param name="newTraining"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<WDSTraningViewModel>> Add(WDSTrainingInputModel newTraining)
        {
            return CreatedAtAction("Add", await _trainingService.AddTrainingAsync(newTraining.GetTrainingObjToAdd()));
        }
    }
}
