using System;
using ProjetoHotel2025.Models;

namespace ProjetoHotel2025.Services
{
    public class HospedeService
    {
        private readonly List<Pessoa> _hospedes = new();

        public void cadastroHospede()
        {
            Console.Clear();
            Console.WriteLine("__Cadastro de Hóspede__");
            Console.Write("Nome:");
            string nome = Console.ReadLine()?.Trim() ?? "";
            Console.Clear();
            Console.Write("Sobrenome:");
            string sobrenome = Console.ReadLine()?.Trim() ?? "";
            string nomeCompleto = $"{nome} {sobrenome}";
            Console.Clear();
            Console.WriteLine($"Confirma o cadastro do Hóspede {nomeCompleto}?");
            Console.WriteLine("1 - SIM\n2 - NÃO");
            Console.WriteLine("Digite uma das opções acima: ");

            if (int.TryParse(Console.ReadLine(), out int opcao) && opcao == 1)
            {
                Pessoa p = new (nome, sobrenome);
                _hospedes.Add(p);
                Console.WriteLine($"\nHóspede {p.NomeCompleto} cadastrado com Sucesso!");
            }
            else
            {
                Console.WriteLine("\nCadastro cancelado.");
            }
        }

        public void ListarHospedes()
        {
            Console.Clear();
            Console.WriteLine("__Lista de Hóspedes Cadastrados__");
            if (_hospedes.Count == 0)
            {
                Console.WriteLine("Nenhum hóspede cadastrado.");
                return;
            }
            for (int i = 0; i < _hospedes.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_hospedes[i].NomeCompleto}");
            }
        }

        public List<Pessoa> ObterTodosHospedes() => _hospedes;

        public int QuantidadeHospedes => _hospedes.Count;

    }
}
