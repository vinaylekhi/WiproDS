using System;
using System.Threading.Tasks;
using Training.Lib.DataModel;
using Training.Lib.DataViewModel;
using Training.Lib.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;
namespace Training.Tests
{
    public class TrainingLibTests
    {
        private readonly TrainingService _sut;
        public TrainingLibTests()
        {
            _sut = new TrainingService(new InMemoryDbContextFactory().GetInMemoryDbContext());
        }
        /// <summary>
        /// get the same output back as system under test doesn't manipulate input values
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddTrainingAsync_GetSameAttributesAfterCall()
        {
            //Arrange
            var actual = new WDSTraining
            {
                Name = "Test Training",
                StartDate = DateTime.Parse("12/04/2020"),
                EndDate = DateTime.Parse("28/04/2020")
            };
            //Act
            var result = await _sut.AddTrainingAsync(actual);

            //Assert
            Assert.Equal(actual.Name, result.Name);
            Assert.Equal(actual.StartDate, result.StartDate);
            Assert.Equal(actual.EndDate, result.EndDate);
        }
        /// <summary>
        /// Verify number of days of training
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddTrainingAsync_VerifyTotalTrainingDays()
        {
            //Arrange
            var actual = new WDSTraining
            {
                Name = "Test Training",
                StartDate = DateTime.Parse("20/04/2020"),
                EndDate = DateTime.Parse("30/04/2020")
            };
            //Act
            var result = await _sut.AddTrainingAsync(actual);

            //Assert
            Assert.Equal(10, result.TrainingDays);

        }

        /// <summary>
        /// Check if null input object is passed then system under test throws NullReferenceException
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddTrainingAsync_GetNullReferenceException()
        {
            //Arrange   
            await Assert.ThrowsAsync<NullReferenceException>(() => _sut.AddTrainingAsync(null));
        }
    }
    /// <summary>
    /// Mock DBContext
    /// </summary>
    public class InMemoryDbContextFactory
    {
        public TrainingDBContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TrainingDBContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            .Options;
            var dbContext = new TrainingDBContext(options);

            return dbContext;
        }
    }
}
