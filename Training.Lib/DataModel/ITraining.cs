using System;

namespace Training.Lib.DataModel
{
    public interface ITraining
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

    }
}
