using Domain.Model;
using System.Collections.Generic;

namespace Domain.Infra
{
    public interface ICotacaoRepository
    {
        List<Cotacao> Query();
    }
}
