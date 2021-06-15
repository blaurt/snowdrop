using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Snowdrop.DAL.Repositories;
using Snowdrop.DAL.Tests.Core;
using Snowdrop.Data.Entities.Core;
using Xunit;

namespace Snowdrop.DAL.Tests.Repositories
{
    // [TestClass]
    public abstract class BaseRepositoryTest<TEntity> : BaseTest where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> Repository = default;

        protected BaseRepositoryTest()
        {
            Repository = Services.GetService<IRepository<TEntity>>();
        }
        
        [Fact]
        public void GetService_Repository_NotNull()
        {
            Assert.NotNull(Repository);
        }

        [Fact]
        public async Task Insert_NullEntity_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => Repository.Insert(null));
        }

        [Fact]
        public async Task Update_NullEntity_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => Repository.Update(null));
        }

        [Fact]
        public async Task Delete_NullEntity_ArgumentNullException()
        {
            //Arrange
            var id = -1;
            //Act

            var entity = await Repository.GetSingle(id);

            //Assert
            Assert.Null(entity);
            await Assert.ThrowsAsync<ArgumentNullException>(() => Repository.Delete(id));
        }


    }
}