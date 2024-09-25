using Domain;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace GeladeiraTeste.TestesDomain
{
    public class GeladeiraTeste
    {

        private Geladeira _geladeira;

        public GeladeiraTeste()
        {
            _geladeira = new Geladeira();


        }



        //Funcionando
        [Fact]
        public void GetItemById_ComSucesso()
        {
            var item = new ItemDomain { Id = 1, Nome = "Maçã" };
            _geladeira.Itens.Add(item);

            var result = _geladeira.GetItemById(1);

            Assert.Equal(item, result);
        }

        [Fact]
        public void GetItemById_NaoEncontrado()
        {
            var result = _geladeira.GetItemById(1);
            Assert.Null(result);
        }


        [Fact]
        public void AdicionarItem_AndarInvalido()
        {


            var item = new ItemDomain { Nome = "Maçã" };


            Action act = () => _geladeira.AdicionarItem(-1, 1, 1, item);

            Assert.Throws<ArgumentException>(act);
        }


        [Fact]
        public void AdicionarItem_ComSucesso()
        {
            // Arrange
            var item = new ItemDomain { Nome = "Maçã" };

            // Act
            _geladeira.AdicionarItem(0, 0, 0, item);

            // Assert
            var resultado = _geladeira.Localizacao(item);
            var expectedMessage = $"Item '{item.Nome}' está localizado no andar 0, container 0, posição 0";
            Assert.Contains(expectedMessage, resultado);
        }




        [Fact]
        public void AdicionarItem_ContainerInvalido()


        {


            var item = new ItemDomain { Nome = "Maçã" };


            Action act = () => _geladeira.AdicionarItem(1, -1, 1, item);


            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void AdicionarItem_PosicaoInvalida()
        {


            var item = new ItemDomain { Nome = "Maçã" };


            Action act = () => _geladeira.AdicionarItem(1, 1, -1, item);

            Assert.Throws<ArgumentException>(act);
        }


        [Fact]
        public void RemoverItem_ComSucesso()
        {

            var geladeira = new Geladeira();
            var item = new ItemDomain { Id = 1, Nome = "Maçã" };


            geladeira.AdicionarItem(0, 0, 0, item);


            geladeira.RemoverItem(item.Id);

            Assert.DoesNotContain(item, geladeira.Itens);

            var container = geladeira.Andares[0].Containers[0];
            Assert.Null(container.Posicoes[0]);


            Console.WriteLine($"Item '{item.Nome}' removido.");
        }



        [Fact]
        public void RemoverItem_ComFalha()
        {
            var geladeira = new Geladeira();
            var item = new ItemDomain { Id = 1, Nome = "Maçã" };


            geladeira.AdicionarItem(0, 0, 0, item);


            geladeira.RemoverItem(2);


            Assert.Contains(item, geladeira.Itens);

            var container = geladeira.Andares[0].Containers[0];
            Assert.NotNull(container.Posicoes[0]);
            Console.WriteLine($"Item '{item.Nome}' removido.");


        }

        [Fact]
        public void ExibirItens_ComSucesso()
        {
            // Arrange
            var geladeira = new Geladeira();
            var item1 = new ItemDomain { Id = 1, Nome = "Maçã" };
            var item2 = new ItemDomain { Id = 2, Nome = "Banana" };

            // Act
            geladeira.AdicionarItem(0, 0, 0, item1);
            geladeira.AdicionarItem(0, 0, 1, item2);

            // Assert
            var itens = geladeira.ExibirItens();
            Assert.True(itens.Any(s => s.Contains("Maçã")));
            Assert.True(itens.Any(s => s.Contains("Banana")));
        }

        [Fact]
        public void ExibirItens_Item_falha()
        {

            var geladeira = new Geladeira();
            var item1 = new ItemDomain { Id = 1, Nome = "Maçã" };


            geladeira.AdicionarItem(0, 0, 0, item1);
            var itens = geladeira.ExibirItens();


            Assert.DoesNotContain("Maçã", itens);
        }

        [Fact]
        public void AtualizarItem_ItemExists()
        {
            // Arrange
            var geladeira = new Geladeira();
            var item1 = new ItemDomain { Id = 1, Nome = "Maçã" };
            var newItem = new ItemDomain { Id = 1, Nome = "Nova Maçã" };

            // Act
            geladeira.AdicionarItem(0, 0, 0, item1);
            geladeira.AtualizarItem(1, newItem);

            // Assert
            Assert.Contains(newItem, geladeira.Itens);
            Assert.DoesNotContain(item1, geladeira.Itens);
        }

        [Fact]
        public void AtualizarItem_Item_Falha()
        {
            // Arrange
            var geladeira = new Geladeira();
            var newItem = new ItemDomain { Id = 1, Nome = "Nova Maçã" };
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            geladeira.AtualizarItem(1, newItem);

            // Assert
            Assert.Equal("Item não encontrado." + Environment.NewLine, output.ToString());
        }
    }
}
