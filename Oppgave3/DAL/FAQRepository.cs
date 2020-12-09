using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Oppgave3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave3.DAL
{
    public class FAQRepository : IFAQRepository
    {
        private readonly FAQContext _db;
        private readonly ILogger<FAQRepository> _log;

        public FAQRepository(FAQContext db, ILogger<FAQRepository> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<List<Kategori>> HentKategorier()
        {
            try
            {
                var kategoriListe = await _db.kategorier.Include(f => f.faqs).ToListAsync();

                foreach (Kategori kat in kategoriListe)
                {
                    kat.faqs.Sort();
                }

                return kategoriListe;
            }
            catch (Exception exp)
            {
                _log.LogInformation("HentFAQ feilet: " + exp.Message);
                return null;
            }
        }

        [HttpGet]
        public async Task<List<Skjema>> HentStilteSpørsmål()
        {
            try
            {
                var skjemaListe = await _db.skjemaer.ToListAsync();
                return skjemaListe;
            }
            catch (Exception exp)
            {
                _log.LogInformation("HentStilteSpørsmål feilet: " + exp.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> EndreLikesFAQ([FromBody] FAQDTO endreFAQ)
        {
            try
            {
                FAQ FAQ = await _db.faqs
                    .FirstOrDefaultAsync(x => x.id == endreFAQ.id);

                _log.LogInformation("Endrer likes: " + FAQ.like + " til " + endreFAQ.likes + "\n"
                    + "Endrer dislikes: " + FAQ.dislike + " til " + endreFAQ.dislikes);

                if (FAQ != null)
                {
                    FAQ.like = endreFAQ.likes;
                    FAQ.dislike = endreFAQ.dislikes;
                }
                else return false;

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception exp)
            {
                _log.LogInformation("EndreLikes feilet: " + exp.Message);
                return false;
            }
        }

        public async Task<int> LagreSkjema([FromBody] SkjemaDTO etSkjema)
        {
            EntityEntry<Skjema> _skjema;
            try
            {
                _skjema = _db.skjemaer.Add(new Skjema
                {
                    navn = etSkjema.navn,
                    mail = etSkjema.mail,
                    spørsmål = etSkjema.spørsmål
                });

                _log.LogInformation("Lagrer nytt spørreskjema: \n" + etSkjema.navn + "\n" + etSkjema.mail + "\n" + etSkjema.spørsmål);

                await _db.SaveChangesAsync();
                return _skjema.Entity.id;
            }
            catch (Exception exp)
            {
                _log.LogInformation("LagreSkjema feilet: " + exp.Message);
                return -1;
            }
        }
    }
}