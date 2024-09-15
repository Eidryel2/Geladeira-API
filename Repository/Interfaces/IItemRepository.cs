using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IItemRepository<T>
    {
        void AddItem(T ItemDomain);
        void UpdateItem(T ItemDomain);
        T GetItem(int id);
        void DeleteItem(int id);
        List<T> GetAllItems();

    }
}
