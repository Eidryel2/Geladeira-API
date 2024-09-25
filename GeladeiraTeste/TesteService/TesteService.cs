using Domain;
using Moq;
using Repository.Interfaces;
using Service.ServicesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Atividade feita pelas alunas
//Adrielly Ribeiro da Silva
// Caroline de Lima Santos
//Aline dos Santos Araújo
namespace GeladeiraTeste.TesteService
{
    public  class TesteService
    {
        private readonly Mock<IItemRepository<ItemDomain>> _itemRepositoryMock;
        private readonly ItemServices _serviceMock;

        public TesteService()
        {
            _itemRepositoryMock = new Mock<IItemRepository<ItemDomain>>();
            _serviceMock = new ItemServices( _itemRepositoryMock.Object);
        }



        [Fact]
        public void AddItem_Sucess()
        {
            // Arrange
            var newItem = new ItemDomain
            {
                Id = 1,
                Nome = "Item 1",
                Andar = 1,
                Container = 1,
                Posicao = 2
            };

            // Act
            _serviceMock.AddItem(newItem);

            // Assert
            _itemRepositoryMock.Verify(m => m.AddItem(It.Is<ItemDomain>(i => i.Id == 1 && i.Nome == "Item 1")), Times.Once);
        }

        [Fact]
        public void GetItem_Sucess()
        {
            // Arrange
            var existingItem = new ItemDomain
            {
                Id = 1,
                Nome = "Item 1",
                Andar = 1,
                Container = 1,
                Posicao = 1
            };

            _itemRepositoryMock.Setup(m => m.GetItem(1)).Returns(existingItem);

            // Act
            var result = _serviceMock.GetItem(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Item 1", result.Nome);
            _itemRepositoryMock.Verify(m => m.GetItem(1), Times.Once);
        }

     
        [Fact]
        public void UpdateItem_Sucess()
        {
            // Arrange
            var existingItem = new ItemDomain
            {
                Id = 1,
                Nome = "Item Existente",
                Andar = 1,
                Container = 2,
                Posicao = 3
            };

            _itemRepositoryMock.Setup(m => m.GetItem(1)).Returns(existingItem);

            var updatedItem = new ItemDomain
            {
                Id = 1,
                Nome = "Item Atualizado"
            };

            // Act
            _serviceMock.UpdateItem(updatedItem);

            // Assert
            _serviceMock.UpdateItem(updatedItem);
            var updatedItemFromRepo = _itemRepositoryMock.Object.GetItem(1);
            Assert.NotNull(updatedItemFromRepo);
            Assert.Equal("Item Atualizado", updatedItemFromRepo.Nome);
        }

     

        [Fact]
        public void DeleteItem_Sucess()
        {
            // Arrange
            var existingItem = new ItemDomain
            {
                Id = 1,
                Nome = "Item Existente"
            };

            _itemRepositoryMock.Setup(m => m.GetItem(1)).Returns(existingItem);

            // Act
            _serviceMock.DeleteItem(1);

            // Assert
            _itemRepositoryMock.Verify(m => m.DeleteItem(1), Times.Once);
        }

    

    }
}
