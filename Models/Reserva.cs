namespace ProjetoHotel2025.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            if (Suite.Capacidade >= hospedes.Count)
            {
                Hospedes = hospedes;
            }
            else
            {
                Console.WriteLine("Não foi possível concluir a reserva!\n" +
                    "Quantidade de hospedes maior que a capacidade da suite.\n" +
                    "A quantidade máxima permitda na suite é " + Suite.Capacidade);
            }
        }

        public void CadastrarHospede (Pessoa hospede)
        {
            if (Suite.Capacidade >= Hospedes.Count)
            {
                Hospedes.Add(hospede);
            }
            else
            {
                Console.WriteLine("Capacidade da suíte atingida!");
            }
        }


        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            return Hospedes.Count;
        }

        public decimal CalcularValorDiaria()
        {
            decimal valor = 0;
            valor = Suite.ValorDiaria * DiasReservados;
            // Regra: Caso os dias reservados forem maior ou igual a 10, conceder um desconto de 10%
            if (DiasReservados >= 10)
            {
                decimal desconto = (decimal)0.1 * valor;
                valor -= desconto;
            }

            return valor;
        }
    }
}