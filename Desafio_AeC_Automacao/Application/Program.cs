using Desafio_AeC_Automacao.Application.Services;
using Desafio_AeC_Automacao.BancoDados.DTO;
using Desafio_AeC_Automacao.BancoDados.Models;

Console.WriteLine("Iniciando a Aplicação!");

new Selenium().ExecucaoSelenium();
List<ClsCursos> Lista = new DTOCursos().GetCursos();
Console.WriteLine($"[{Lista.Count}] Registros encontrados!");
Console.WriteLine("Apresentando os dados...");

Lista.ForEach(Item => {
    Console.WriteLine("---------------------------------");
    Console.WriteLine($"Curso ==  {Item.Titulo}");
    Console.WriteLine($"Professor ==  {Item.Professor}");
    Console.WriteLine($"Carga Horaria ==  {Item.CargaHoraria}");
    Console.WriteLine($"Descrição  ==  {Item.Descricao}");
    Console.WriteLine("---------------------------------");
});

Console.WriteLine("Pressione alguma tecla para finalização da aplicação");
Console.ReadKey();


