using System;
using System.ComponentModel.DataAnnotations;

namespace Training.Lib.DataModel
{
    /// <summary>
    /// Training DTO
    /// </summary>
    public class WDSTraining : ITraining
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}
