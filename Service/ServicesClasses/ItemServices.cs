using Domain;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Service.Interfaces;
using Repository.Models;

namespace Service.ServicesClasses
{
    public class ItemServices : IServices<ItemDomain>
    {
        private readonly IItemRepository<ItemDomain> _repository;

        public ItemServices(IItemRepository<ItemDomain> repository)
        {
            _repository = repository;
        }

        public void AddItem(ItemDomain itemDomain)
        {
            var item = new ItemDomain
            {
                Nome = itemDomain.Nome,
                Id = itemDomain.Id,
                Andar = itemDomain.Andar,
                Container = itemDomain.Container,
                Posicao = itemDomain.Posicao,
            };
            _repository.AddItem(item);
        }

        public void UpdateItem(ItemDomain itemDomain)
        {
            var existingItem = _repository.GetItem(itemDomain.Id);
            if (existingItem != null)
            {
                existingItem.Nome = itemDomain.Nome;
                // Atualizar outras propriedades conforme necessário
                _repository.UpdateItem(existingItem);
            }
            else
            {
                throw new KeyNotFoundException($"Item com o id {itemDomain.Id} não encontrado.");
            }
        }

        public ItemDomain GetItem(int id)
        {
            var item = _repository.GetItem(id);
            if (item != null)
            {
                return new ItemDomain
                {
                    Nome = item.Nome,
                    Id = item.Id
                    // Mapear outras propriedades conforme necessário
                };
            }
            return null;
        }

        public void DeleteItem(int id)
        {
            var item = _repository.GetItem(id);
            if (item != null)
            {
                _repository.DeleteItem(id);
            }
            else
            {
                throw new KeyNotFoundException($"Item com o id {id} não encontrado.");
            }
        }

        public List<ItemDomain> GetAllItems()
        {
            return _repository.GetAllItems()
                .Select(i => new ItemDomain
                {
                    Nome = i.Nome,
                    Id = i.Id
                    // Mapear outras propriedades conforme necessário
                })
                .ToList();
        }
    }
}
