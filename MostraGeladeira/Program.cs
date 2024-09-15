
using Domain;


class Program
{
    static void Main(string[] args)
    {
        Geladeira minhaGeladeira = new Geladeira();
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("\nMenu de Operações da Geladeira:");
            Console.WriteLine("1. Adicionar item em uma posição específica");
            Console.WriteLine("2. Remover item de uma posição específica");
            Console.WriteLine("3. Adicionar item a um container");
            Console.WriteLine("4. Remover todos os itens de um container");
            Console.WriteLine("5. Exibir todos os itens da geladeira");
            Console.WriteLine("6. Alterar posição de um item");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarAdd = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerAdd = int.Parse(Console.ReadLine());
                    Console.Write("Digite a posição (0 a 3): ");
                    int posicaoAdd = int.Parse(Console.ReadLine());
                    Console.Write("Digite o nome do item: ");
                    string nomeItemAdd = Console.ReadLine();
                    Console.Write("Digite o ID do item: ");
                    int idItemAdd = int.Parse(Console.ReadLine());
                    minhaGeladeira.AdicionarItem(andarAdd, containerAdd, posicaoAdd, new Item(nomeItemAdd, idItemAdd));
                    break;

                case "2":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarRem = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerRem = int.Parse(Console.ReadLine());
                    Console.Write("Digite a posição (0 a 3): ");
                    int posicaoRem = int.Parse(Console.ReadLine());
                    minhaGeladeira.RemoverItem(andarRem, containerRem, posicaoRem);
                    break;

                case "3":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarAddCont = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerAddCont = int.Parse(Console.ReadLine());
                    Console.Write("Digite o nome do item: ");
                    string nomeItemAddCont = Console.ReadLine();
                    minhaGeladeira.AdicionarItensAoContainer(andarAddCont, containerAddCont, new Item(nomeItemAddCont, 0));
                    break;

                case "4":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarRemCont = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerRemCont = int.Parse(Console.ReadLine());
                    minhaGeladeira.RemoverItensDoContainer(andarRemCont, containerRemCont);
                    break;

                case "5":
                    foreach (string item in minhaGeladeira.ExibirItens())
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case "6":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }
        }
    }
}