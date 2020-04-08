using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Lib.DataModel
{
    /// <summary>
    /// DBContext for data access
    /// </summary>
    public class TrainingDBContext : DbContext
    {
        /// <summary>
        /// Training DbSet
        /// </summary>
        public DbSet<WDSTraining> Trainings { get; set; }
        public TrainingDBContext(DbContextOptions<TrainingDBContext> options)
            : base(options)
        {
        }
    }
}
