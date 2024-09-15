namespace Domain

{
    public class Andar
    {
        public List<Container> Containers { get; set; }

        public string Tipo { get; set; }
        // representa o tipo do andar

        public Andar(string tipo)
        {
            Tipo = tipo;
            Containers = new List<Container>
            {
                //cada andar vai começar com 2 containers
                new Container(),
                new Container()
            };
        }
    }
}
