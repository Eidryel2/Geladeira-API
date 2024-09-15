using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Posicao
    {

        public ItemDomain Item { get; set; }


        public Posicao()
        {
            Item = null;
        }

        //metodod
        public bool EstaVazia()
        {
            return Item == null;
        }

        public void AdicionarItem(ItemDomain item)
        {
            if (EstaVazia())
            {
                Item = item;
            }
            else
            {
                throw new InvalidOperationException("A posição já está ocupada.");
            }
        }

        public void RemoverItem()
        {
            if (!EstaVazia())
            {
                Item = null;
            }
            else
            {
                throw new InvalidOperationException("A posição já está vazia.");
            }
        }

        public override string ToString()
        {
            return Item != null ? Item.ToString() : "Vazio";
        }
    }
    public class Container
    {
        public List<Posicao> Posicoes { get; private set; }
        public List<ItemDomain> Itens { get; set; }

        public Container()
        {
            Posicoes = new List<Posicao>();
            //inicia com 4 posiçoes 
            for (int i = 0; i < 4; i++)
            {
                Posicoes.Add(new Posicao());
            }
        }

        //metodos

        public bool EstaCheio()
        {
            return Posicoes.TrueForAll(p => !p.EstaVazia());
        }

        public bool EstaVazio()
        {
            return Posicoes.TrueForAll(p => p.EstaVazia());
        }

        public void AdicionarItem(int posicao, ItemDomain item)
        {
            if (EstaCheio())
            {
                throw new InvalidOperationException("O container está cheio.");
            }
            Posicoes[posicao].AdicionarItem(item);
        }

        public void RemoverItem(int posicao)
        {
            Posicoes[posicao].RemoverItem();
        }

        public void AdicionarItens(ItemDomain item)
        {
            foreach (var pos in Posicoes)
            {
                if (pos.EstaVazia())
                {
                    pos.AdicionarItem(item);
                    return;
                }
            }
        }

        public void RemoverItens()
        {
            if (EstaVazio())
            {
                throw new InvalidOperationException("O container já está vazio.");
            }

            foreach (var pos in Posicoes)
            {
                pos.RemoverItem();
            }
        }
    }

}
