using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oppgave3.DAL;
using Oppgave3.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave3.Controllers
{
    [Route("[controller]/[action]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQRepository _db;
        private readonly ILogger<FAQController> _log;

        public FAQController(IFAQRepository db, ILogger<FAQController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentKategorier()
        {
            List<Kategori> kategorier = await _db.HentKategorier();

            return Ok(kategorier);
        }

        [HttpGet]
        public async Task<ActionResult> HentStilteSpørsmål()
        {
            List<Skjema> skjemaListe = await _db.HentStilteSpørsmål();

            return Ok(skjemaListe);
        }

        [HttpPost]
        public async Task<ActionResult> EndreLikesFAQ([FromBody] FAQDTO faqDTO)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreLikesFAQ(faqDTO);
                if (!returOK)
                {
                    _log.LogInformation("FAQ ble ikke endret");
                    return NotFound("FAQ ble ikke endret");
                }
                _log.LogInformation("FAQ endret");
                return Ok("FAQ endret");
            }
            _log.LogInformation("Feil i inputvalidering på server");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPost]
        public async Task<ActionResult> LagreSkjema([FromBody] SkjemaDTO etSkjema)
        {
            if (ModelState.IsValid)
            {
                int id = await _db.LagreSkjema(etSkjema);

                if (id == -1)
                {
                    _log.LogInformation("Skjema kunne ikke lagres!");
                    return BadRequest("Skjema kunne ikke lagres!");
                }
                _log.LogInformation("Skjema ble lagret!");
                return Ok(id);
            }
            _log.LogInformation("Feil i inputvalidering på server");
            return BadRequest("Feil i inputvalidering på server");
        }
    }
}