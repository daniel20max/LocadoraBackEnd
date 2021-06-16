using LocadoraDeFilmes.Data;
using LocadoraDeFilmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Services
{
    public static class SeedData
    {
        internal static void Initialize(IServiceProvider services)
        {
            string[] nomes = new string[] { "O Poderoso Chefão", "O Mágico de Oz", "Cidadão Kane", "Um Sonho de Liberdade", "Pulp Fiction", "Casablanca", "O Poderoso Chefão", "ET", "2001: Uma Odisseia no Espaço", "A Lista de Schindler", "Guerra nas Estrelas", "De Volta Para o Futuro", "Os Caçadores da Arca Perdida", "Forrest Gump", "… E o Vento Levou", "O Sol é Para Todos", "Apocalypse Now", "Noivo Neurótico, Noiva Nervosa", "Os Bons Companheiros", "A Felicidade não se Compra", "Chinatown", "O Silêncio dos Inocentes", "Lawrence da Arábia", "Tubarão", "A Noviça Rebelde", "Cantando na Chuva", "Clube dos Cinco", "A Primeira Noite de um Homem", "Blade Runner — O Caçador de Androides", "Um Estranho no Ninho", "A Princesa Prometida", "Star Wars: Episódio V — O Império Contra-Ataca", "Fargo", "Beleza Americana", "Laranja Mecânica", "Curtindo a Vida Adoidado", "Dr. Fantástico", "Harry & Sally — Feitos um Para o Outro", "O Iluminado", "O Clube da Luta", "Psicose", "Alien", "Toy Story", "Matrix", "Titanic", "O Resgate do Soldado Ryan", "Quanto Mais Quente Melhor", "Os Suspeitos", "Janela Indiscreta", "Jurassic Park", "O Grande Lebowski", "A Malvada", "Gênio Indomável", "Butch Cassidy", "Taxi Driver", "Brilho Eterno de Uma Mente Sem Lembranças", "O Cavaleiro das Trevas", "Crepúsculo dos Deuses", "Thelma & Louise", "O Fabuloso Destino de Amelie Poulain", "Amor, Sublime Amor", "Intriga Internacional", "Feitiço do Tempo", "Mary Poppins", "Touro Indomável", "O Rei Leão", "Avatar", "Monty Python e o Cálice Sagrado", "Gladiador", "Um Corpo que Cai", "Quase Famosos", "O Jovem Frankenstein", "Todos os Homens do Presidente", "Banzé no Oeste", "A Ponte do Rio Kwai", "Brokeback Mountain", "Os Caça-Fantasma", "12 Homens e uma Sentença", "Wall-E", "Sindicato dos Ladrões", "Amadeus", "O Senhor dos Anéis: A Sociedade do Anel", "Duro de Matar", "Inception", "Seven", "A Bela e a Fera", "The Lord of the Rings: The Return of the King", "Quem Quer Ser um Milionário", "Coração Selvagem", "Amnésia", "Rocky: O Lutador", "Up", "Contatos Imediatos do Terceiro Grau", "O Franco Atirador", "Doutor Jivago", "O Labirinto do Fauno", "Apertem os Cintos… O Piloto Sumiu", "Cães de Aluguel", "Bonnie e Clyde", "Os Sete Samurais" };
            string[] generos = new string[] { "Terror", "Acao", "Aventura", "Biografia", "Comendia", "Drama", "Ficcao Cientifica", "Fantasia", "Documentario", "Faroeste", "Musical", "Suspense" };
            int quantidade = 10;

            using (var context = new LocadoraContext(services.GetRequiredService<DbContextOptions<LocadoraContext>>()))
            {
                if (context.filmes.Any())
                {
                    return;
                }
                for (int i = 0; i < quantidade; i++)
                {
                    Random rand = new Random();
                    var inicial = new DateTime(1988, 10, 14);
                    int atual = (DateTime.Today - inicial).Days;
                    context.filmes.Add(
                        new Filme()
                        {
                            nome = $"{nomes[rand.Next(nomes.Length)]}",
                            sinopse = $"{generos[rand.Next(generos.Length)]}",
                            preco = rand.Next(49, 350),
                            lancamento = inicial.AddDays(rand.Next(atual)),
                            duracao = rand.Next(70, 240),
                            link = "https://www.youtube.com/embed/xgbZBVbARpo"
                        }
                    );
                }

                context.SaveChanges();
            }

        }
    }
}
