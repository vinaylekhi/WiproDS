using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Lib.DataModel;
using Training.Lib.DataViewModel;

namespace Training.Lib.Services
{
    /// <summary>
    /// Specification for training service
    /// </summary>
    public interface ITrainingService
    {
        /// <summary>
        /// Get all the trainings from database
        /// </summary>
        /// <returns></returns>
        Task<List<WDSTraningViewModel>> GetTrainingsAsync();
        /// <summary>
        /// Add new training ro database
        /// </summary>
        /// <param name="iTraining"></param>
        /// <returns></returns>
        Task<WDSTraningViewModel> AddTrainingAsync(WDSTraining iTraining);
    }
}
