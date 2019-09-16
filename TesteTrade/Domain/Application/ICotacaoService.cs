using Domain.Model;
using System;
using System.Collections.Generic;

namespace Domain.Application
{
    public interface ICotacaoService
    {
        Trade CalculaCompra(int qtdAcoes, string symbol);
        Trade CalculaVenda(int qtdAcoes, string symbol);
        List<Cotacao> ListaCotacaoTotal();
        double CalculaValorMaximoPorDia(DateTime dia, string symbol);
        double CalculaValorMinimoPorDia(DateTime dia, string symbol);
        double CalculaValorMedioPorDia(DateTime dia, string symbol);
        double CalculaValorMaximoPorMes(DateTime dia, string symbol);
        double CalculaValorMinimoPorMes(DateTime dia, string symbol);
        double CalculaValorMedioPorMes(DateTime dia, string symbol);
        double CalculaValorMaximoPorAno(DateTime dia, string symbol);
        double CalculaValorMinimoPorAno(DateTime dia, string symbol);
        double CalculaValorMedioPorAno(DateTime dia, string symbol);
    }
}
