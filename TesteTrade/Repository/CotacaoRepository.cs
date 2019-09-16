using Domain.Infra;
using Domain.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class CotacaoRepository : ICotacaoRepository
    {
        public List<Cotacao> Query()
        {
            var json = File.ReadAllText(@"../Repository/Mock/rate.json", Encoding.GetEncoding("iso-8859-1"));

            var cotacao = JsonConvert.DeserializeObject<List<Cotacao>>(json);

            return cotacao;
        }
    }
}
