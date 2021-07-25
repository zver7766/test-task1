using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Tests.Helpers;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using Moq;
using Xunit;

namespace API.Tests.DbTests
{
    public class DbTest
    {
        [Fact]
        public async void Db_UnitOfWork_GetEntityWithSpec_Returns_Advertisement()
        {
            // Arrange

            var entity = TestHelper.GetListOfAds().First();

            var mockDb = new Mock<IUnitOfWork>();
            var spec = new Mock<ISpecification<Advertisement>>();

            spec.Setup(x => x.Criteria).Returns(x => x.IsActive);

            mockDb.Setup(db => db.Repository<Advertisement>().GetEntityWithSpec(spec.Object))
                .Returns(Task.FromResult(entity));


            var dbContext = mockDb.Object;

            // Act

            var data = await dbContext.Repository<Advertisement>().GetEntityWithSpec(spec.Object);
            // Assert

            Assert.Equal(entity, data);
        }

        [Fact]
        public async void Db_UnitOfWork_Complete_Returns_Int_Of_Successfully_Saving()
        {
            // Arrange
            const int SuccessfulResultFromDb = 1;

            var mockDb = new Mock<IUnitOfWork>();
            mockDb.Setup(db => db.Complete()).Returns(Task.FromResult(1));

            var dbContext = mockDb.Object;
            // Act
            
            var result = await dbContext.Complete(); 
            // Assert

             Assert.Equal(SuccessfulResultFromDb,result);
        }

        [Fact]
        public async void Db_UnitOfWork_Get_Returns_Advertisement()
        {
            // Arrange

            var entities = TestHelper.GetListOfAds();

            var mockDb = new Mock<IUnitOfWork>();
            var spec = new Mock<ISpecification<Advertisement>>();

            spec.Setup(x => x.Criteria).Returns(x => x.IsActive);

            mockDb.Setup(db => db.Repository<Advertisement>().ListAsync(spec.Object))
                .Returns(Task.FromResult((IReadOnlyList<Advertisement>) entities));


            var dbContext = mockDb.Object;
            // Act

            var data = await dbContext.Repository<Advertisement>().ListAsync(spec.Object);
            // Assert
            
            Assert.Equal(entities, data);
        }


        [Fact]
        public async void Db_UnitOfWork_Count_Returns_Number_Of_Ads()
        {
            // Arrange

            var entities = TestHelper.GetListOfAds();

            var mockDb = new Mock<IUnitOfWork>();
            var spec = new Mock<ISpecification<Advertisement>>();

            spec.Setup(x => x.Criteria).Returns(x => x.IsActive);

            mockDb.Setup(db => db.Repository<Advertisement>().CountAsync(spec.Object))
                .Returns(Task.FromResult(entities.Count()));


            var dbContext = mockDb.Object;
            // Act

            var data = await dbContext.Repository<Advertisement>().CountAsync(spec.Object);
            // Assert

            Assert.Equal(entities.Count(), data);
        }

        [Fact]
        public async void Db_UnitOfWork_ListAll_Returns_All_Ads()
        {
            // Arrange

            var entities = TestHelper.GetListOfAds();

            var mockDb = new Mock<IUnitOfWork>();


            mockDb.Setup(db => db.Repository<Advertisement>().ListAllAsync())
                .Returns(Task.FromResult((IReadOnlyList<Advertisement>) entities));


            var dbContext = mockDb.Object;
            // Act

            var data = await dbContext.Repository<Advertisement>().ListAllAsync();
            // Assert

            Assert.Equal(entities, data);
        }

        [Fact]
        public async void Db_UnitOfWork_GetByIdAsync_Returns_Ad()
        {
            // Arrange
            const int Id = 1;

            var entities = TestHelper.GetListOfAds();

            var mockDb = new Mock<IUnitOfWork>();


            mockDb.Setup(db => db.Repository<Advertisement>().GetByIdAsync(Id))
                .Returns(Task.FromResult(entities.First()));


            var dbContext = mockDb.Object;
            // Act

            var data = await dbContext.Repository<Advertisement>().GetByIdAsync(Id);
            // Assert

            Assert.Equal(entities.First(), data);
        }

        [Fact]
        public void Db_UnitOfWork_Add_Add_Entity_In_A_Db()
        {
            // Arrange

            var entity = TestHelper.GetListOfAds().First();

            var mockDb = new Mock<IUnitOfWork>();


            mockDb.Setup(db => db.Repository<Advertisement>().Add(entity));


            var dbContext = mockDb.Object;
            // Act

            dbContext.Repository<Advertisement>().Add(entity);
            // Assert

            mockDb.Verify(r => r.Repository<Advertisement>().Add(entity), Times.Once);
        }

        [Fact]
        public void Db_UnitOfWork_Update_Updating_Entity_In_A_Db()
        {
            // Arrange

            var entity = TestHelper.GetListOfAds().First();

            var mockDb = new Mock<IUnitOfWork>();


            mockDb.Setup(db => db.Repository<Advertisement>().Update(entity));


            var dbContext = mockDb.Object;
            // Act

            dbContext.Repository<Advertisement>().Update(entity);
            // Assert

            mockDb.Verify(r => r.Repository<Advertisement>().Update(entity), Times.Once);
        }

        [Fact] public void Db_UnitOfWork_Delete_Deleting_Entity_In_A_Db()
        {
            // Arrange

            var entity = TestHelper.GetListOfAds().First();

            var mockDb = new Mock<IUnitOfWork>();


            mockDb.Setup(db => db.Repository<Advertisement>().Delete(entity));


            var dbContext = mockDb.Object;
            // Act

            dbContext.Repository<Advertisement>().Delete(entity);
            // Assert

            mockDb.Verify(r => r.Repository<Advertisement>().Delete(entity), Times.Once);
        }
    }
}
