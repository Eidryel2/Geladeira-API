using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IServices<T>
    {
        void AddItem(T ItemDomain);
        void UpdateItem(T ItemDomain);
        T GetItem(int id);
        void DeleteItem(int id);
        List<T> GetAllItems();
        List<T> LocalizarItems(string localizacao);
    }
}
