
using Domain;
using System.ComponentModel;

namespace Domain
{
    public class Geladeira
    {
        public List<Andar> Andares { get; private set; }
        public List<ItemDomain> Itens { get; private set; }

        public Geladeira()
        {
            Andares = new List<Andar>
        {
            new Andar("Hortifrutis"),
            new Andar("Laticínios e Enlatados"),
            new Andar("Charcutaria, Carnes e Ovos")
        };
            Itens = new List<ItemDomain>();
        }

      

        public string Localizacao(ItemDomain item)
        {
            foreach (var andar in Andares)
            {
                foreach (var container in andar.Containers)
                {
                    foreach (var posicao in container.Posicoes)
                    {
                        if (posicao != null && posicao.Item == item)
                        {
                            return $"Item '{item.Nome}' está localizado no andar {Andares.IndexOf(andar)}, container {andar.Containers.IndexOf(container)}, posição {container.Posicoes.IndexOf(posicao)}";
                        }
                    }
                }
            }
            return $"Item '{item.Nome}' não encontrado";
        }

       

        public void AtualizarItem(int id, ItemDomain newItem)
        {
            ItemDomain existingItem = GetItemById(id);
            if (existingItem != null)
            {
               
                existingItem.Nome = newItem.Nome;
                
            }
            else
            {
                Console.WriteLine("Item não encontrado.");
            }
        }

        public bool ItemExists(string nome, string localizacao)
        {
          
            var item = Itens
                .Where(i => i.Nome == nome && Localizacao(i) == localizacao)
                .FirstOrDefault();

            return item != null;
        }

        //post
        public void AdicionarItem(int andarIndex, int containerIndex, int posicaoIndex, ItemDomain item)
        {
       
            if (andarIndex < 0 || andarIndex >= Andares.Count)
                throw new ArgumentException("Andar inválido.");

            var andar = Andares[andarIndex];
            if (containerIndex < 0 || containerIndex >= andar.Containers.Count)
                throw new ArgumentException("Container inválido.");

            var container = andar.Containers[containerIndex];
            if (posicaoIndex < 0 || posicaoIndex >= container.Posicoes.Count)
                throw new ArgumentException("Posição inválida.");

            var posicao = container.Posicoes[posicaoIndex];

           
            if (posicao != null)
            {
                
                Console.WriteLine($"A posição {posicaoIndex} já está ocupada com o item '{posicao.Item.Nome}'.");
                return; 
            }

          
            container.AdicionarItem(posicaoIndex, item);
            Itens.Add(item);
            Console.WriteLine($"Item '{item.Nome}' adicionado ao andar {andarIndex}, container {containerIndex}, posição {posicaoIndex}.");
        }


      
        public ItemDomain GetItemById(int id)
        {
            return Itens.FirstOrDefault(i => i.Id == id);
        }

       
        public void RemoverItem(int andar, int container, int posicao)
        {
            try
            {
                Andares[andar].Containers[container].RemoverItem(posicao);
                Console.WriteLine($"Item removido do andar {andar}, container {container}, posição {posicao}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AdicionarItensAoContainer(int andar, int container, ItemDomain item)
        {
            if (andar > 3)
            {
                Console.WriteLine("Erro: Andar inválido. A geladeira só tem 3 andares.");
                return;
            }
            try
            {
                Andares[andar].Containers[container].AdicionarItens(item);
                Console.WriteLine($"Item '{item.Nome}' adicionado ao container {container} no andar {andar}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



     
        public void RemoverItensDoContainer(int andar, int container)
        {
            try
            {
                Andares[andar].Containers[container].RemoverItens();
                Console.WriteLine($"Todos os itens removidos do container {container} no andar {andar}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


       
        public List<string> ExibirItens()
        {
            var listarItens = new List<string>();

            for (int i = 0; i < Andares.Count; i++)
            {
                var andar = Andares[i];
                listarItens.Add($"Andar {i} ({andar.Tipo}):");

                for (int j = 0; j < andar.Containers.Count; j++)
                {
                    var container = andar.Containers[j];
                    listarItens.Add($"  Container {j}:");

                    for (int k = 0; k < container.Posicoes.Count; k++)
                    {
                        var posicao = container.Posicoes[k];
                        if (posicao != null)
                        {
                            listarItens.Add($"    Posição {k}: {posicao}");
                        }
                        else
                        {
                            listarItens.Add($"    Posição {k}: Vazio");
                        }
                    }
                }
            }

            return listarItens;
        }


    }
}
