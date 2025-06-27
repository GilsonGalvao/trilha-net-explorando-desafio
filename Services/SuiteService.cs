using System;
using ProjetoHotel2025.Models;

namespace ProjetoHotel2025.Services
{
    public class SuiteService
    {
        private readonly List<Suite> _suites = new();

        public void CriarSuite()
        {
            string tipoSuite = "";
            bool tipoValido = false;
            Console.Clear();
            Console.WriteLine("__Cadastro de Suíte__");
            while (!tipoValido)
            {
                Console.WriteLine("Escolha o tipo da suíte:");
                Console.WriteLine("1 - Básico");
                Console.WriteLine("2 - Premium");
                Console.Write("Opção: ");

                string entrada = Console.ReadLine()?.Trim() ?? "";
                switch (entrada)
                {
                    case "1":
                        tipoSuite = "Básico";
                        tipoValido = true;
                        break;
                    case "2":
                        tipoSuite = "Premium";
                        tipoValido = true;
                        break;
                    default:
                        Console.WriteLine("Opção Inválida. Tente Novamente.\n");
                        break;
                }
            }

            Console.Clear();
            Console.Write("Capacidade da Suíte (quantidade de hóspedes): ");
            if (!int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.WriteLine("Capacidade inválida. Cadastro cancelado.");
                return;
            }

            Console.Clear();
            Console.Write("Valor da diária (€): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valorDiaria))
            {
                Console.WriteLine("Valor inválido. Cadastro cancelado.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Confirma o cadastro da Suíte?");
            Console.WriteLine($"Tipo: {tipoSuite}");
            Console.WriteLine($"Capacidade: {capacidade}");
            Console.WriteLine($"Valor da Diária: {valorDiaria:C}");
            Console.Write("1 - SIM / 2 - NÃO: ");

            if (int.TryParse(Console.ReadLine(), out int confirmacao) && confirmacao == 1)
            {
                Suite suite = new(tipoSuite, capacidade, valorDiaria, true);
                _suites.Add(suite);
                Console.WriteLine("\nSuíte cadastrada com sucesso!");
            }
            else //aqui eu poderia botar um else if para se confirmacao == 2 e emm baixo um else com opção inválida
            {
                Console.WriteLine("\nCadastro cancelado.");
            }
        }

        public List<Suite> ObterSuitesDisponiveis()
        {
            return _suites.Where(x => x.Disponivel).ToList();
        }

        public void MarcarSuiteComoIndisponivel(Suite suite)
        {
            suite.Disponivel = false;
        }

        public int QuantidadeSuites => _suites.Count;

        public Suite? ObterSuitePorIndice(int indice)
        {
            if (indice < 0 || indice >= _suites.Count)
            {
                return null;
            }
            else
            {
                return _suites[indice];
            }
        }
    }
}

