using System.Text;
using ProjetoHotel2025.Models;
using ProjetoHotel2025.Services;

Console.OutputEncoding = Encoding.UTF8;

var hospedeService = new HospedeService();
var suiteService = new SuiteService();
var reservaService = new ReservaService(hospedeService, suiteService);

int opcao = 0;
do
{
    Console.Clear();
    Console.WriteLine("\n" +
        "Menu - Sistema de Hospedagem\n" +
        "1 - Cadastrar Hóspede\n" +
        "2 - Criar Suíte\n" +
        "3 - Cadastrar Reserva\n" +
        "4 - Listar Reservas\n" +
        "5 - Listar Hóspedes Cadastrados\n" +
        "6 - Sair do Menu");
    Console.WriteLine("Escolha uma opção: ");

    if (!int.TryParse(Console.ReadLine(), out opcao))
    {
        Console.WriteLine("Entrada Inválida. Digite um número.");
        Console.ReadKey();
        continue;
    }
    switch (opcao)
    {
        case 1:
            hospedeService.cadastroHospede();
            break;
        case 2:
            suiteService.CriarSuite();
            break;
        case 3:
            reservaService.CadastrarReserva();
            break;
        case 4:
            reservaService.ListarReservas();
            break;
        case 5:
            hospedeService.ListarHospedes();
            break;
        case 6:
            Console.WriteLine("Encerrando o sistema...");
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break; 
    }
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
} while (opcao != 6);