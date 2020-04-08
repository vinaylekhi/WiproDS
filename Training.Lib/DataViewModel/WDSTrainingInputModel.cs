using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Training.Lib.DataModel;

namespace Training.Lib.DataViewModel
{
    /// <summary>
    /// Training input class with validation
    /// </summary>
    public class WDSTrainingInputModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //Accept date in specific format only
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        //Accept date in specific format only
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Get the Data transfer object to save training in database
        /// </summary>
        /// <returns></returns>
        public WDSTraining GetTrainingObjToAdd()
        {
            return new WDSTraining
            {
                Id = 0,
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
        /// <summary>
        /// Custom valiadtion for dates
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.Date < DateTime.Today.Date)
            {
                yield return new ValidationResult(
                    "Start date must be after or equal to current date", new[] { "WDSTrainingInputModel" });
            }
            if (EndDate.Date < DateTime.Today.Date)
            {
                yield return new ValidationResult(
                    "End date must be after or equal to current date", new[] { "WDSTrainingInputModel" });
            }
            if (EndDate.Date < StartDate.Date)
            {
                yield return new ValidationResult(
                    "End date must be after or equal to start date", new[] { "WDSTrainingInputModel" });
            }
        }
    }
}
