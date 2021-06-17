using LocadoraDeFilmes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Services
{
    public interface ILocadoraService
    {
        List<Filme> GetLocadora(string busca = null);
        List<Locacao> GetLocacao(string loggin, string busca = null);
        Locacao GetLock(int? id);
        Filme Get(int? id);

        bool Update(Filme filme);

        bool Delete(int? id);

        bool Create(Filme filme);
        bool Create(Locacao locacao, string loggin);
    }
}
