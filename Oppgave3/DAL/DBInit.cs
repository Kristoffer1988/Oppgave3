using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Oppgave3.Model;
using System.Collections.Generic;
using System.Linq;

namespace Oppgave3.DAL
{
    public static class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FAQContext>();

                if (context.faqs.Count() == 0)
                {
                    var faq1 = new FAQ
                    {
                        spørsmål = "Hvor lang tid tar Oslo-Bergen?",
                        svar = "Ca 5 timer",
                        like = 8,
                        dislike = 2
                    };

                    var faq2 = new FAQ
                    {
                        spørsmål = "Kan dere lage en stasjon fra Lommedalen til Kolsås?",
                        svar = "Vi vurderer kontinuerlig nye ruter, hvis vi får over 10 likes på denne posten vil vi se at kundegrunnlaget er sterkt nok!",
                        like = 33,
                        dislike = 4
                    };

                    var faq3 = new FAQ
                    {
                        spørsmål = "Hvordan kan man få refundert en billett?",
                        svar = "Alle kjøp er endelige og man kan ikke få refundert billetter!",
                        like = 5,
                        dislike = 113
                    };

                    var faq4 = new FAQ
                    {
                        spørsmål = "Hei, kunne dere senket prisene i Corona tiden?",
                        svar = "Beklager, vi har ikke klart å frigjøre noen middler.",
                        like = 10,
                        dislike = 9
                    };

                    var faq5 = new FAQ
                    {
                        spørsmål = "Må man bruke belte i bussen?",
                        svar = "For egen og andres sikkerhet, er det påbudt å bruke sikkerhetsbelte ombord på bussen!",
                        like = 50,
                        dislike = 5
                    };

                    var faq6 = new FAQ
                    {
                        spørsmål = "Finnes det barneseter på bussen?",
                        svar = "Bussene er utstyrt med ett barnesete.",
                        like = 23,
                        dislike = 8
                    };

                    var faq7 = new FAQ
                    {
                        spørsmål = "Får honnør rabatt?",
                        svar = "Ja, dette gis til alle over 67 år.",
                        like = 39,
                        dislike = 5
                    };

                    var faq8 = new FAQ
                    {
                        spørsmål = "Får studenter rabatt?",
                        svar = "Alle studenter under 30 år og som kan vise gyldig studentbevis får rabatt.",
                        like = 23,
                        dislike = 3
                    };

                    var faq9 = new FAQ
                    {
                        spørsmål = "Får ledsagere rabatt?",
                        svar = "Ledsagere som følger døvblinde reiser gratis.",
                        like = 12,
                        dislike = 1
                    };

                    var aktueltListe = new List<FAQ>();
                    aktueltListe.Add(faq4);
                    aktueltListe.Add(faq2);

                    var betalingListe = new List<FAQ>();
                    betalingListe.Add(faq3);

                    var ruteinfoListe = new List<FAQ>();
                    ruteinfoListe.Add(faq1);

                    var sikkerhetListe = new List<FAQ>();
                    sikkerhetListe.Add(faq5);
                    sikkerhetListe.Add(faq6);

                    var rabatterListe = new List<FAQ>();
                    rabatterListe.Add(faq7);
                    rabatterListe.Add(faq8);
                    rabatterListe.Add(faq9);

                    var kategori1 = new Kategori
                    {
                        id = 1,
                        kategori = "Aktuelt nå",
                        faqs = aktueltListe
                    };

                    var kategori2 = new Kategori
                    {
                        id = 2,
                        kategori = "Betaling",
                        faqs = betalingListe
                    };

                    var kategori3 = new Kategori
                    {
                        id = 3,
                        kategori = "Ruteinformasjon",
                        faqs = ruteinfoListe
                    };

                    var kategori4 = new Kategori
                    {
                        id = 4,
                        kategori = "Sikkerhet",
                        faqs = sikkerhetListe
                    };

                    var kategori5 = new Kategori
                    {
                        id = 5,
                        kategori = "Rabatter",
                        faqs = rabatterListe
                    };

                    context.kategorier.Add(kategori1);
                    context.kategorier.Add(kategori2);
                    context.kategorier.Add(kategori3);
                    context.kategorier.Add(kategori4);
                    context.kategorier.Add(kategori5);

                    context.SaveChanges();
                }

                if (context.skjemaer.Count() == 0)
                {
                    var skjema1 = new Skjema
                    {
                        id = 1,
                        navn = "Nordman",
                        mail = "ola@hotmail.no",
                        spørsmål = "Hei, kunne dere senket prisene i Corona tiden?"
                    };

                    var skjema2 = new Skjema
                    {
                        id = 2,
                        navn = "Ola",
                        mail = "nordman@norge.no",
                        spørsmål = "Hei, kunne dere satt opp avganger mellom Oslo og Drammen i rushen? Må stå i bussen hver dag!"
                    };

                    context.skjemaer.Add(skjema1);
                    context.skjemaer.Add(skjema2);

                    context.SaveChanges();
                }
            }
        }
    }
}