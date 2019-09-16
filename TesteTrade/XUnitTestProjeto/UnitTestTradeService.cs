using Domain.Application;
using Domain.Infra;
using Domain.Model;
using Moq;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProjeto
{
    public class UnitTestTradeService
    {
        private readonly ICotacaoService _cotacaoService;
        private readonly Mock<ICotacaoRepository> _cotacaoRepository = new Mock<ICotacaoRepository>();

        public UnitTestTradeService()
        {
            _cotacaoService = new CotacaoService(_cotacaoRepository.Object);

        }
        #region Mock da lista de famílias
        public List<Cotacao> FuncaoRetornaCotacao()
        {
            var lista = new List<Cotacao>();
            lista.Add(new Cotacao
            {
                Id = 97,
                Symbol = "ABC",
                Rate = 1,
                Timestamp = new DateTime(2018, 2, 1, 10, 22, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 98,
                Symbol = "ABC",
                Rate = 100,
                Timestamp = new DateTime(2018, 2, 1, 10, 22, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 99,
                Symbol = "ABC",
                Rate = 5,
                Timestamp = new DateTime(2018, 8, 1, 10, 22, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 0,
                Symbol = "ABC",
                Rate = 40,
                Timestamp = new DateTime(2018, 8, 1, 10, 22, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 1,
                Symbol = "ABC",
                Rate = 15,
                Timestamp = new DateTime(2018, 8, 13, 10, 22, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 2,
                Symbol = "ABC",
                Rate = 20,
                Timestamp = new DateTime(2018, 8, 13, 23, 59, 00)
            });
            lista.Add(new Cotacao
            {
                Id = 3,
                Symbol = "ABC",
                Rate = 25,
                Timestamp = new DateTime(2018, 8, 13, 9, 25, 22)

            });
            lista.Add(new Cotacao
            {
                Id = 4,
                Symbol = "CBI",
                Rate = 20,
                Timestamp = new DateTime(2018, 8, 13, 9, 6, 22)

            });
            lista.Add(new Cotacao
            {
                Id = 5,
                Symbol = "CBI",
                Rate = 11,
                Timestamp = new DateTime(2018, 8, 13, 10, 25, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 6,
                Symbol = "CBI",
                Rate = 15,
                Timestamp = new DateTime(2018, 8, 13, 23, 40, 00)

            });
            lista.Add(new Cotacao
            {
                Id = 7,
                Symbol = "LST",
                Rate = 25,
                Timestamp = new DateTime(2018, 8, 13, 22, 6, 22)

            });
            lista.Add(new Cotacao
            {
                Id = 8,
                Symbol = "LST",
                Rate = 20,
                Timestamp = new DateTime(2018, 8, 13, 10, 35, 30)

            });
            lista.Add(new Cotacao
            {
                Id = 9,
                Symbol = "LST",
                Rate = 55,
                Timestamp = new DateTime(2018, 8, 13, 8, 40, 00)

            });
            lista.Add(new Cotacao
            {
                Id = 10,
                Symbol = "LST",
                Rate = 23,
                Timestamp = new DateTime(2018, 8, 13, 8, 40, 00)

            });
            lista.Add(new Cotacao
            {
                Id = 11,
                Symbol = "LST",
                Rate = 2,
                Timestamp = new DateTime(2018, 8, 1, 8, 40, 00)

            });
            lista.Add(new Cotacao
            {
                Id = 12,
                Symbol = "LST",
                Rate = 61,
                Timestamp = new DateTime(2018, 8, 1, 8, 40, 00)

            });
            lista.Add(new Cotacao
            {
                Id = 12,
                Symbol = "LST",
                Rate = 66,
                Timestamp = new DateTime(2018, 6, 1, 8, 40, 00)

            });
            lista.Add(new Cotacao
            {
                Id = 12,
                Symbol = "LST",
                Rate = 1,
                Timestamp = new DateTime(2018, 5, 1, 8, 40, 00)

            });
            return lista;
        }

        public List<Trade> FuncaoRetornaTrade()
        {
            var lista = new List<Trade>();

            lista.Add(new Trade
            {
                Id = 1,
                NoOfShares = 5,
                Action = "BUY",
                Price = 100,
                Symbol = "ABC",
                PortfolioId = 1
            });
            lista.Add(new Trade
            {
                Id = 2,
                NoOfShares = 20,
                Action = "BUY",
                Price = 300,
                Symbol = "CBI",
                PortfolioId = 1
            });
            lista.Add(new Trade
            {
                Id = 3,
                NoOfShares = 13,
                Action = "SELL",
                Price = 286,
                Symbol = "LST",
                PortfolioId = 1
            });
            lista.Add(new Trade
            {
                Id = 4,
                NoOfShares = 10,
                Action = "SELL",
                Price = 200,
                Symbol = "ABC",
                PortfolioId = 1
            });

            return lista;
        }

        #endregion


        [Theory]
        [InlineData(5, "CBI", 75)]
        [InlineData(20, "CBI", 300)]
        public void TestCompraAcoes(int qtdAcoes, string symbol, double precoEsperado)
        {
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var trade = _cotacaoService.CalculaCompra(qtdAcoes, symbol);

            //Assert
            Assert.Equal(trade.Price, precoEsperado);
        }

        [Theory]
        [InlineData(13, "LST", 325)]
        [InlineData(10, "ABC", 200)]
        public void TestVendaAcoes(int qtdAcoes, string symbol, double precoEsperado)
        {
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var trade = _cotacaoService.CalculaVenda(qtdAcoes, symbol);

            //Assert
            Assert.Equal(trade.Price, precoEsperado);
        }

        [Fact]
        public void TestListagemCotacaoTotal()
        {
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var listaOrdenada = _cotacaoService.ListaCotacaoTotal();

            //Assert
            var cotacaoABC = listaOrdenada.Where(p => p.Symbol.ToLower() == "abc").First();
            var cotacaoLST = listaOrdenada.Where(p => p.Symbol.ToLower() == "lst").First();
            var cotacaoCBI = listaOrdenada.Where(p => p.Symbol.ToLower() == "cbi").First();
            Assert.Equal(new DateTime(2018, 8, 13, 23, 59, 00), cotacaoABC.Timestamp);
            Assert.Equal(20, cotacaoABC.Rate);
            Assert.Equal(2, cotacaoABC.Id);
            Assert.Equal(new DateTime(2018, 8, 13, 22, 6, 22), cotacaoLST.Timestamp);
            Assert.Equal(25, cotacaoLST.Rate);
            Assert.Equal(7, cotacaoLST.Id);
            Assert.Equal(new DateTime(2018, 8, 13, 23, 40, 00), cotacaoCBI.Timestamp);
            Assert.Equal(15, cotacaoCBI.Rate);
            Assert.Equal(6, cotacaoCBI.Id);
        }
        public class TestCase
        {
            public static readonly List<object[]> DataParametros = new List<object[]>
            {
                new object[]{ new DateTime(2018, 8, 13, 10, 22, 30)},
                new object[]{ new DateTime(2018, 8, 13, 10, 22, 30)}
            };
        }

        [Theory]
        [InlineData(0, "ABC", 25)]
        [InlineData(1, "LST", 55)]
        public void TestValorMaximoNoDia(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMaximoNoDia = _cotacaoService.CalculaValorMaximoPorDia(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMaximoNoDia);
        }

        [Theory]
        [InlineData(0, "ABC", 15)]
        [InlineData(1, "LST", 20)]
        public void TestValorMinimoNoDia(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMinimoNoDia = _cotacaoService.CalculaValorMinimoPorDia(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMinimoNoDia);
        }

        [Theory]
        [InlineData(0, "ABC", 20)]
        [InlineData(1, "LST", 30.75)]
        public void TestValorMedioNoDia(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMedioNoDia = _cotacaoService.CalculaValorMedioPorDia(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMedioNoDia);
        }


        [Theory]
        [InlineData(0, "ABC", 40)]
        [InlineData(1, "LST", 61)]
        public void TestValorMaximoNoMes(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMaximoNoMes = _cotacaoService.CalculaValorMaximoPorMes(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMaximoNoMes);
        }

        [Theory]
        [InlineData(0, "ABC", 5)]
        [InlineData(1, "LST", 2)]
        public void TestValorMinimoNoMes(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMinimoNoMes = _cotacaoService.CalculaValorMinimoPorMes(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMinimoNoMes);
        }

        [Theory]
        [InlineData(0, "ABC", 21)]
        [InlineData(1, "LST", 31)]
        public void TestValorMedioNoMes(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMedioNoMes = _cotacaoService.CalculaValorMedioPorMes(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMedioNoMes);
        }

        [Theory]
        [InlineData(0, "ABC", 100)]
        [InlineData(1, "LST", 66)]
        public void TestValorMaximoNoAno(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMaximoNoAno = _cotacaoService.CalculaValorMaximoPorAno(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMaximoNoAno);
        }

        [Theory]
        [InlineData(0, "ABC", 1)]
        [InlineData(1, "LST", 1)]
        public void TestValorMinimoNoAno(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMinimoNoAno = _cotacaoService.CalculaValorMinimoPorAno(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, valorMinimoNoAno);
        }

        [Theory]
        [InlineData(0, "ABC", 29.4)]
        [InlineData(1, "LST", 31.6)]
        public void TestValorMedioNoAno(int i, string symbol, double valorEsperado)
        {
            var input = TestCase.DataParametros[i];
            var data = (DateTime)input[0];
            //Arrange
            _cotacaoRepository.Setup(p => p.Query()).Returns(FuncaoRetornaCotacao());
            //Act
            var valorMedioNoAno = _cotacaoService.CalculaValorMedioPorAno(data, symbol);

            //Assert
            Assert.Equal(valorEsperado, Math.Round(valorMedioNoAno,1));
        }
    }
}
