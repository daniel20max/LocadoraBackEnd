using LocadoraDeFilmes.Models;
using LocadoraDeFilmes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Controllers
{
    public class HomeController : Controller
    {
        private ILocadoraService _service;

        public HomeController(ILocadoraService service)
        {
            _service = service;
        }
        public IActionResult Index(string busca = null)
        {
            var filmes = _service.GetLocadora(); //atribuir uma varial pra conseguir atrair as viewbag dela
            ViewBag.contador = _service.GetLocadora().Count(); // viewbag pra contar a quantidade de filmes na lista
            ViewBag.soma = _service.GetLocadora().Sum(p => p.duracao); // essa coisa feia soma o total de duracao os filmes da lista
            ViewBag.filmenovo = filmes.Find(p => p.lancamento == filmes.Max(p => p.lancamento)).nome; // basicamente procura um lancamente, compara se é o maior lancamente "atual no caso" e passa o nome do lancamento na viewbag
            ViewBag.filmecaro = filmes.Find(p => p.preco == filmes.Max(p => p.preco)).nome;// mesmo do de cima só q procura preço maior
            return View(_service.GetLocadora(busca));// passsa a lista full de filmes, mas pode minizar a lista com uma busca.
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(); // Apenas View Create feita base HTML
        }

        [Authorize]
        [HttpPost] //puxando um method pra upar no banco, tbm da pra usar pra upar um fantasma, vc pode ver na view oq foi criado falso
        [ValidateAntiForgeryToken] //vai obrigar a usar os ta nos requed
        public IActionResult Create(Filme filme)
        {
            if (!ModelState.IsValid) return View(filme); //se preencher esquecer de preencher algo vai aparecer os  erro do requered


            return _service.Create(filme) ? // vai chamar a locacoraservice e o filme passado a lista ADD
                RedirectToAction("Index") : // se add vai simples redirecionar pra index.
                NotFound(); // se nao conseguir add vai apresentar 404
        }
        public IActionResult Visualizar(int? id)
        {
            Filme filme = _service.GetLocadora().Find(filme => filme.id == id); // passei numa variavel pra facilidade uma validaçao estranha
            if (filme == null) // se tentar passar um filme na barra url e nao tiver filme com aquele id  vai retornar um erro tradado
            {
                return View("Invalido"); // todo id passado sem valor, vai apresentar 404, mas tem botao pra voltar ao index
            }
            return View(filme);// como ja tinha feito variavel nao precisava chama _service entao só mandei filme q ja tinha id
        }
        public IActionResult Sucesso() // não lembro oq eu tentei fazer aki
        {

            return View();
        }
        public IActionResult Validar() // não lembro oq eu tentei fazer aki
        {

            return View();
        }
        public IActionResult Invalido() //um notfound q eu fiz com botao de home
        {

            return View();
        }
        [Authorize]
        public IActionResult Update(int? id)
        {
            Filme filme = _service.Get(id); 
            return filme == null ? NotFound() : View(filme);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(Filme filme)
        {
            if (!ModelState.IsValid) // validando os requered , pra ver se pode proceguir
            {
                return View(filme);
            }
            return _service.Update(filme) ? // tentando atualizando o filme com cmd de services
                RedirectToAction("Index") : // se funcionar vai redireciar pro home 
                NotFound();
        }
        [Authorize]
        public IActionResult Delete(int? id) 
        {
            if (_service.Delete(id)) // apos validar o id q deseja deletar volta pra ka
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult ValidaDelete(int id) //recebendo tudo por asp-route-id do q precisa deletar
        {
            ViewBag.id = id;     // enviando o id  por viewbag no asp rout pra nao perde o id na transicão
            return View();
        }
        [Authorize]
        public IActionResult Aluguel()
        {
            var loca = _service.GetLocacao(User.Identity.Name).Where(loca => loca.dataHora < loca.alguel).ToList(); // nao vai visualizar na lista as locacoes expiradas, mas ainda existem só nao da pra ver
            return View(loca); // vai retorna oq eu pedi acima
        }
        [Authorize]
        public IActionResult Criar()
        {
            var filmes = _service.GetLocadora();
            ViewBag.listfilmes = new SelectList(filmes, "id", "nome");
            
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Locacao locacao)
        {
            if (!ModelState.IsValid) //pedindo validacao para requered
            {
                var filmes = _service.GetLocacao(User.Identity.Name); // parametro dos requered
                ViewBag.listfilmes = new SelectList(filmes, "id", "nome"); // parametro dos requered, mas especial pq recebe os valores do FK
                return View(locacao);
            }
            locacao.dataHora = DateTime.Now; //quando for adicionar a locacao a locacao dos paramentro acima pegar esse aki tbm e por no banco

            return _service.Create(locacao, User.Identity.Name) ? // pegando os dados acima add no banco
                RedirectToAction("Index") :
                NotFound();
        }

        public IActionResult Pipoca(int? id)
        {

            Locacao locacao = _service.GetLock(id); // chamei uma viavel pra me ajudar
            var filme = _service.Get(locacao.filmeId); // o id q vai ser pego pelo filme, q é o mesmo do filme.id
            ViewBag.link = filme.link; // quando ele encontrar o filmeid = filme.link vai retorna pra minha viewbag o link pra assistir

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
