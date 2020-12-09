using Microsoft.AspNetCore.Mvc;
using Oppgave3.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oppgave3.DAL
{
    public interface IFAQRepository
    {
        Task<List<Kategori>> HentKategorier();

        Task<List<Skjema>> HentStilteSpørsmål();

        Task<bool> EndreLikesFAQ([FromBody] FAQDTO enFAQ);

        Task<int> LagreSkjema([FromBody] SkjemaDTO etSkjema);
    }
}