using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Lib.DataModel;
using Training.Lib.DataViewModel;

namespace Training.Lib.Services
{
    public class TrainingService : ITrainingService
    {
        /// <summary>
        /// Get Database context via dependency injection
        /// </summary>
        private readonly TrainingDBContext _context;
        /// <summary>
        /// Constructor received dependent database context object from IoC container via dependency injection
        /// </summary>
        /// <param name="context"></param>
        public TrainingService(TrainingDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Async method to add new training
        /// </summary>
        /// <param name="iTraining"></param>
        /// <returns></returns>
        public async Task<WDSTraningViewModel> AddTrainingAsync(WDSTraining iTraining)
        {
            if (iTraining == null)
                throw new NullReferenceException();

            _context.Trainings.Add(iTraining);
            var _task = _context.SaveChangesAsync();
            var _result = new WDSTraningViewModel
            {
                Id = iTraining.Id,
                Name = iTraining.Name,
                StartDate = iTraining.StartDate,
                EndDate = iTraining.EndDate,
                TrainingDays = Convert.ToInt32((iTraining.EndDate - iTraining.StartDate).TotalDays)
            };
            await Task.WhenAll(_task);
            _result.Id = iTraining.Id;
            return _result;
        }
        /// <summary>
        /// Async method to get the list of all stored trainings
        /// </summary>
        /// <returns></returns>
        public async Task<List<WDSTraningViewModel>> GetTrainingsAsync()
        {
            return await _context.Trainings.Select(t => new WDSTraningViewModel
            {
                Id = t.Id,
                Name = t.Name,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                TrainingDays = Convert.ToInt32((t.EndDate - t.StartDate).TotalDays)
            }).ToListAsync();
        }
    }
}
