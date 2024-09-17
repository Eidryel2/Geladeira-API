using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ItemDomain
    {
        // Propriedades
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100)] 
        public string Nome { get; set; }

        public int Andar { get; set; }
        public int Container { get; set; }
        public int Posicao { get; set; }
        // Construtor padrão
        public ItemDomain() { }

        // Construtor com parâmetros
        public ItemDomain(string nome, int id)
        {
            Nome = nome;
            Id = id;
        }

        // Sobrescreve o método ToString
        public override string ToString()
        {
            return $"{Nome} (ID: {Id})";
        }
        public class ItemUpdateDomain
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
    }
}
