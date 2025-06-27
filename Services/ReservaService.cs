using System;
using ProjetoHotel2025.Models;

namespace ProjetoHotel2025.Services
{
    public class ReservaService
    {
        private readonly List<Reserva> _reservas = new();
        private readonly SuiteService _suiteService;
        private readonly HospedeService _hospedeService;

        public ReservaService (HospedeService hospedeService, SuiteService suiteService)
        {
            _hospedeService = hospedeService;
            _suiteService = suiteService;
        }

        public void CadastrarReserva()
        {
            Console.Clear();
            Console.WriteLine("__Cadastro de Reserva__");

            // 1. Verifica a disponibilidade das suítes
            var suitesDisponiveis = _suiteService.ObterSuitesDisponiveis();
            if (suitesDisponiveis.Count == 0)
            {
                Console.WriteLine("Nenhuma suíte disponível no momento.");
                return;
            }

            // 2. Quantidade de dias à reservar
            Console.Write("Quantos dias de reserva? ");
            if (!int.TryParse(Console.ReadLine(), out int diasReservados))
            {
                Console.WriteLine("Valor inválido. Reserva cancelada.");
                return;
            }

            // 3. Exibe suítes disponíveis
            Console.Clear();
            Console.WriteLine("Suítes disponíveis:");
            for (int i = 0; i < suitesDisponiveis.Count; i++)
            {
                var suite = suitesDisponiveis[i];
                Console.WriteLine($"[{i+1}] {suite.TipoSuite} | Capacidade: {suite.Capacidade} | Valor da diária: {suite.ValorDiaria:C}");
            }

            Console.Write("\nEscolha o número da suíte: ");
            if (!int.TryParse(Console.ReadLine(), out int indiceSuite) || indiceSuite < 1 || indiceSuite > suitesDisponiveis.Count)
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            var suiteSelecionada = suitesDisponiveis[indiceSuite - 1];
            
            // 4. Escolher hospedes
            var todosHospedes = _hospedeService.ObterTodosHospedes();
            if (todosHospedes.Count == 0)
            {
                Console.WriteLine("Nenhum hóspede cadastrado. Cadastre ao menos um hóspede.");
                _hospedeService.cadastroHospede();
                todosHospedes = _hospedeService.ObterTodosHospedes();
            }

            List<Pessoa> hospedesDaReserva = new();
            bool adicionando = true;

            while (adicionando && hospedesDaReserva.Count < suiteSelecionada.Capacidade)
            {
                Console.Clear();
                Console.WriteLine("Escolha um hóspede para adicionar à reserva:");

                for (int i = 0; i < todosHospedes.Count; i++)
                {
                    Console.WriteLine($"[{i+1}] {todosHospedes[i].NomeCompleto}");
                }

                Console.WriteLine("Número do Hóspede: ");
                if (int.TryParse(Console.ReadLine(), out int indiceHospede) && indiceHospede >= 1 && indiceHospede <= todosHospedes.Count)
                {
                    indiceHospede--;
                    hospedesDaReserva.Add(todosHospedes[indiceHospede]);
                }
                else
                {
                    Console.WriteLine("Hóspede inválido.");
                }

                if (hospedesDaReserva.Count > suiteSelecionada.Capacidade)
                {
                    Console.WriteLine("Capacidade da suíte atingida.");
                    break;
                }

                Console.WriteLine("Deseja adicionar mais hóspedes? 1 - SIM / 2 - NÃO :");
                if (int.TryParse(Console.ReadLine(), out int confirmacao) && confirmacao == 2)
                {
                    adicionando = false;
                }
            }
            // 4. Cria a reserva
            try
            {
                var reserva = new Reserva(diasReservados);
                reserva.CadastrarSuite(suiteSelecionada);
                reserva.CadastrarHospedes(hospedesDaReserva);

                _reservas.Add(reserva);
                _suiteService.MarcarSuiteComoIndisponivel(suiteSelecionada);

                Console.Clear();
                Console.WriteLine("Reserva concluída com sucesso!");
                Console.WriteLine($"Total de hóspedes: {reserva.ObterQuantidadeHospedes()}");
                Console.WriteLine($"Valor total da reserva: {reserva.CalcularValorDiaria():C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Criar reserva: " + ex.Message);
            }
        }

        public void ListarReservas ()
        {
            Console.Clear();
            if (_reservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva registrada.");
                return;
            }

            for (int i = 0; i < _reservas.Count; i++)
            {
                var r = _reservas[i];
                Console.WriteLine($"Reserva: #{i + 1}:");
                Console.WriteLine($"- Suite: {r.Suite.TipoSuite}");
                Console.WriteLine($"- Hóspedes: {r.ObterQuantidadeHospedes()}");
                Console.WriteLine($"- Total: {r.CalcularValorDiaria():C}");
                Console.WriteLine("----------");
            }
        }

    }
}