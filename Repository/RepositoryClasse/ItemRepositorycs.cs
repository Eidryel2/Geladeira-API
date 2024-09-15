using System.Collections.Generic;
using System.Linq;
using Repository.Models;
using Repository.Interfaces;
using Domain;

public class ItemRepository : IItemRepository<ItemDomain>
{
    private readonly ApiGeladeiraContext _context;

    public ItemRepository(ApiGeladeiraContext context)
    {
        _context = context;
    }

    public void AddItem(ItemDomain itemDomain)
    {
        _context.Items.Add(itemDomain);
        _context.SaveChanges();
    }

    public void UpdateItem(ItemDomain itemDomain)
    {
        var existingItem = _context.Items.Find(itemDomain.Id);
        if (existingItem != null)
        {
            _context.Entry(existingItem).CurrentValues.SetValues(itemDomain);
            _context.SaveChanges();
        }
    }

    public ItemDomain GetItem(int id)
    {
        var item = _context.Items.Find(id);
        if (item == null)
        {
            throw new KeyNotFoundException($"Item com o id {id} não encontrado.");
        }
        return item;
    }

    public void DeleteItem(int id)
    {
        var itemDomain = _context.Items.Find(id);
        if (itemDomain != null)
        {
            _context.Items.Remove(itemDomain);
            _context.SaveChanges();
        }
    }

    public List<ItemDomain> GetAllItems()
    {
        return _context.Items.ToList();
    }
}
