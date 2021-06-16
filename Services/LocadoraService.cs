using LocadoraDeFilmes.Data;
using LocadoraDeFilmes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Services
{
    public class LocadoraService : ILocadoraService
    {
        private LocadoraContext _context;
        public LocadoraService(LocadoraContext context)
        {
            _context = context;
        }
        public bool Create(Filme filme)
        {
            try
            {
                _context.Add(filme);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Create(Locacao locacao)
        {
            try
            {
                _context.Add(locacao);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int? id)
        {
            try
            {
                _context.Remove(Get(id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Filme Get(int? id) => _context.filmes.Find(id);
        public List<Filme> GetLocadora(string busca = null) =>
            _context.filmes.Where(p =>
                busca == null ?
                    true :
                    p.nome.ToUpper().Contains(busca.ToUpper()))
            .ToList();

        public Locacao GetLock(int? id) => _context.locacao.Include(l => l.filme).FirstOrDefault(l => l.id == id);
        public List<Locacao> GetLocacao(string busca = null) =>
           _context.locacao.Include(l => l.filme).ToList();

        public bool Update(Filme p)
        {
            try
            {
                _context.Update(p);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
