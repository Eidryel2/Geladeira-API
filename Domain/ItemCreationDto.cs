using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ItemCreationDto
    {
        public string Nome { get; set; }
        public int Andar { get; set; }
        public int Container { get; set; }
        public int Posicao { get; set; }

        public int Id { get; set; }
    }
}
