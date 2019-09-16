using Domain.Application;
using Domain.Infra;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class CotacaoService : ICotacaoService
    {
        private readonly ICotacaoRepository _cotacaoRepository;

        public CotacaoService(ICotacaoRepository repository)
        {
            _cotacaoRepository = repository;
        }

        public Trade CalculaCompra(int qtdAcoes, string symbol)
        {
            return _cotacaoRepository.Query().OrderByDescending(p => p.Timestamp).Where(p => p.Symbol.ToLower() == symbol.ToLower()).Select(p => new Trade { Action = "BUY", NoOfShares = qtdAcoes, Symbol =symbol, Price = p.Rate * qtdAcoes}).FirstOrDefault();
        }

        public Trade CalculaVenda(int qtdAcoes, string symbol)
        {
            return _cotacaoRepository.Query().OrderByDescending(p => p.Timestamp).Where(p => p.Symbol.ToLower() == symbol.ToLower()).Select(p => new Trade { Action = "SELL", NoOfShares = qtdAcoes, Symbol = symbol, Price = p.Rate * qtdAcoes }).FirstOrDefault();
        }

        public List<Cotacao> ListaCotacaoTotal()
        {
            return _cotacaoRepository.Query().OrderBy(p => p.Symbol).ThenByDescending(p => p.Timestamp).GroupBy(c => c.Symbol).Select(c => new Cotacao
            {
                Id = c.ToList().First().Id,
                Timestamp = c.ToList().First().Timestamp,
                Rate = c.ToList().First().Rate,
                Symbol = c.Key,
            }).ToList();
        }

        public double CalculaValorMaximoPorDia(DateTime data,string symbol)
        {
            return  _cotacaoRepository.Query().Where(p => p.Timestamp.DayOfYear == data.DayOfYear && p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Max(p => p.Rate);
        }

        public double CalculaValorMinimoPorDia(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.DayOfYear == data.DayOfYear && p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Min(p => p.Rate);
        }

        public double CalculaValorMedioPorDia(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.DayOfYear == data.DayOfYear && p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Average(p => p.Rate);
        }

        public double CalculaValorMaximoPorMes(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.Month == data.Month && p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Max(p => p.Rate);
        }

        public double CalculaValorMinimoPorMes(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.Month == data.Month && p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Min(p => p.Rate);
        }

        public double CalculaValorMedioPorMes(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.Month == data.Month && p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Average(p => p.Rate);
        }

        public double CalculaValorMaximoPorAno(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Max(p => p.Rate);
        }

        public double CalculaValorMinimoPorAno(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Min(p => p.Rate);
        }

        public double CalculaValorMedioPorAno(DateTime data, string symbol)
        {
            return _cotacaoRepository.Query().Where(p => p.Timestamp.Year == data.Year && p.Symbol.ToLower() == symbol.ToLower()).Average(p => p.Rate);
        }
    }
}
