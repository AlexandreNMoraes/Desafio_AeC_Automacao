using System.Text;
using System.Threading;
using Desafio_AeC_Automacao.BancoDados.DTO;
using Desafio_AeC_Automacao.BancoDados.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace Desafio_AeC_Automacao.Application.Services;
public class Selenium
{
    private IWebDriver driver;
    private StringBuilder verificationErrors;
    private string baseURL;

    public void ExecucaoSelenium()
    {
        Console.WriteLine("Processando dados...");
        StartChrome();
        Pesquisar("RPA");
        AcessaPrimeiroCurso();
        ClsCursos Obj = CapturaDados();
        Console.WriteLine("Inserindo dados...");
        new DTOCursos().InsertCursos(Obj);
        
    }
    public void StartChrome()
    {
        var option = new ChromeOptions()
        {
            BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
        };
        option.AddArgument("--headless"); // Executar o Chrome em modo headless
        option.AddArgument("--disable-gpu"); // Desativar aceleração de hardware

        // Desativar logs do ChromeDriver
        var driverService = ChromeDriverService.CreateDefaultService();
        driverService.HideCommandPromptWindow = true;


        driver = new ChromeDriver(driverService, option);
        baseURL = "https://www.google.com/";
        verificationErrors = new StringBuilder();

    }

    public void Pesquisar(string Titulo)
    {
        driver.Navigate().GoToUrl("https://www.alura.com.br/");
        driver.FindElement(By.Id("header-barraBusca-form-campoBusca")).Click();
        driver.FindElement(By.Id("header-barraBusca-form-campoBusca")).Clear();
        driver.FindElement(By.Id("header-barraBusca-form-campoBusca")).SendKeys(Titulo);
        driver.FindElement(By.CssSelector(".header__nav--busca-form")).Submit();
    }

    public void AcessaPrimeiroCurso()
    {
        Thread.Sleep(2500);
        // Realizar o scraping dos dados dos cursos retornados
        IWebElement listaCursos = driver.FindElement(By.Id("busca-resultados"));
        IWebElement paginacao = listaCursos.FindElement(By.CssSelector("ul.paginacao-pagina"));

        IList<IWebElement> elementosLi = paginacao.FindElements(By.TagName("li"));
        elementosLi[0].Click();
    }

    public ClsCursos CapturaDados()
    {
        By Element;
        // Capturação do Titulo pelo Header
        IWebElement Header = driver.FindElement(By.CssSelector("section.course-header__wrapper div.container div.course-icon-title-flex"));

        string Titulo = Header.FindElement(By.CssSelector("h1.curso-banner-course-title")).Text + Header.FindElement(By.CssSelector("p.course--banner-text-category")).Text;
        // CargaHoraria
        Element = By.CssSelector("section.course-header__wrapper div.container div.course-container-flex--desktop div.course-container__co-branded_icon div.course-container--icon div.couse-container--spacing div p.courseInfo-card-wrapper-infos");
        string CargaHoraria = FormataHorario(driver.FindElement(Element).Text.ToUpper());

        // Professor + Descricao
        IWebElement Conteudo = driver.FindElement(By.CssSelector("section.container div.course-container--instructor"));
        //--> Descricao
        string Descricao = "";
        IWebElement Lista = Conteudo.FindElement(By.CssSelector("div.container-list--width ul.course-list"));
        IList<IWebElement> elementosLI = Lista.FindElements(By.TagName("li"));

        elementosLI.ToList().ForEach(Item => Descricao += Item.Text + "\n");
        //--> Professor
        IWebElement Section = Conteudo.FindElement(By.TagName("section"));
        IWebElement div = Section.FindElement(By.TagName("div"));
        IWebElement div2 = div.FindElement(By.TagName("div"));
        IWebElement div3 = div2.FindElement(By.TagName("div"));
        var Professor = div3.FindElement(By.TagName("h3")).Text;

        return new ClsCursos {Titulo = Titulo, CargaHoraria = CargaHoraria, Descricao = Descricao, Professor = Professor};
    }
    private string FormataHorario(string Hora)
    {
        // Verifica se a string possui o formato esperado
        if (Hora.EndsWith("H") && int.TryParse(Hora.TrimEnd('H'), out int horas))
        {
            // Formata a hora e retorna no formato "HH:mm"
            string horaFormatada = horas.ToString("D2") + ":00";
            return horaFormatada;
        }
        else
        {
            // Caso a string não esteja no formato esperado, retorna vazio ou um valor padrão
            return string.Empty; // ou "00:00" ou outra valor padrão
        }
    }


}

