using System;
using Training.Lib.DataModel;

namespace Training.Lib.DataViewModel
{
    /// <summary>
    /// Data view model for training - Resource to return from API 
    /// </summary>
    public class WDSTraningViewModel : ITraining
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TrainingDays { get; set; }
    }
}
