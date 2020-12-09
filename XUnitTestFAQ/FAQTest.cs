using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Oppgave3.Controllers;
using Oppgave3.DAL;
using Oppgave3.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestFAQ
{
    public class FAQTest
    {
        private readonly Mock<IFAQRepository> mockAdmin = new Mock<IFAQRepository>();
        private readonly Mock<ILogger<FAQController>> mockAdminLogg = new Mock<ILogger<FAQController>>();

        /****************************
        **Enhetstester for Kategori**
        ****************************/

        [Fact]
        public async Task HentKategorierOK()
        {
            //Arrange
            var faq1 = new FAQ
            {
                spørsmål = "Hvor lang tid tar Oslo-Bergen?",
                svar = "Ca 5 timer",
                like = 1,
                dislike = 2
            };

            var ruteinfoListe = new List<FAQ>();
            ruteinfoListe.Add(faq1);

            var kategori1 = new Kategori
            {
                id = 1,
                kategori = "Ruteinformasjon",
                faqs = ruteinfoListe
            };

            var kategoriListe = new List<Kategori>();
            kategoriListe.Add(kategori1);

            mockAdmin.Setup(k => k.HentKategorier()).ReturnsAsync(kategoriListe);

            var faqController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            //Act
            var resultat = await faqController.HentKategorier() as OkObjectResult;

            //Assert
            Assert.Equal<List<Kategori>>((List<Kategori>)resultat.Value, kategoriListe);
        }

        /**************************
        **Enhetstester for Skjema**
        **************************/

        [Fact]
        public async Task HentSkjemaOK()
        {
            //Arrange
            var skjema1 = new Skjema
            {
                id = 1,
                navn = "Nordman",
                mail = "ola@hotmail.no",
                spørsmål = "Hei, kunne dere senket prisene i Corona tiden?"
            };

            var skjemaListe = new List<Skjema>();
            skjemaListe.Add(skjema1);

            mockAdmin.Setup(k => k.HentStilteSpørsmål()).ReturnsAsync(skjemaListe);

            var faqController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            //Act
            var resultat = await faqController.HentStilteSpørsmål() as OkObjectResult;

            //Assert
            Assert.Equal<List<Skjema>>((List<Skjema>)resultat.Value, skjemaListe);
        }

        [Fact]
        public async Task LagreSkjemaOK()
        {
            // Arrange

            mockAdmin.Setup(k => k.LagreSkjema(It.IsAny<SkjemaDTO>())).ReturnsAsync(1);

            var fAQController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            // Act
            var resultat = await fAQController.LagreSkjema(It.IsAny<SkjemaDTO>()) as OkObjectResult;

            // Assert
            Assert.Equal(1, resultat.Value);
        }

        [Fact]
        public async Task LagreSkjemaIkkeOK()
        {
            // Arrange

            mockAdmin.Setup(k => k.LagreSkjema(It.IsAny<SkjemaDTO>())).ReturnsAsync(-1);

            var fAQController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            // Act
            var resultat = await fAQController.LagreSkjema(It.IsAny<SkjemaDTO>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Skjema kunne ikke lagres!", resultat.Value);
        }

        [Fact]
        public async Task LagreSkjemaFeilModel()
        {
            // Arrange
            // SkjemaDTO er indikert feil på navn, er ikke lov med tall.
            // det har ikke noe å si, det er introduksjonen med ModelError under som tvinger frem feilen
            // kunne også her brukt It.IsAny<BilettType>
            var skjema1 = new SkjemaDTO
            {
                navn = "Nordman123",
                mail = "ola@hotmail.no",
                spørsmål = "Hei, kunne dere senket prisene i Corona tiden?"
            };

            mockAdmin.Setup(k => k.LagreSkjema(skjema1)).ReturnsAsync(1);

            var fAQController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            fAQController.ModelState.AddModelError("Navn", "Feil i inputvalidering på server");

            // Act
            var resultat = await fAQController.LagreSkjema(skjema1) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        /***********************
        **Enhetstester for FAQ**
        ***********************/

        [Fact]
        public async Task EndreFAQOK()
        {
            // Arrange

            mockAdmin.Setup(k => k.EndreLikesFAQ(It.IsAny<FAQDTO>())).ReturnsAsync(true);

            var adminController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            // Act
            var resultat = await adminController.EndreLikesFAQ(It.IsAny<FAQDTO>()) as OkObjectResult;

            // Assert

            Assert.Equal("FAQ endret", resultat.Value);
        }

        [Fact]
        public async Task EndreFAQIkkeOK()
        {
            // Arrange

            mockAdmin.Setup(k => k.EndreLikesFAQ(It.IsAny<FAQDTO>())).ReturnsAsync(false);

            var faqController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            // Act
            var resultat = await faqController.EndreLikesFAQ(It.IsAny<FAQDTO>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal("FAQ ble ikke endret", resultat.Value);
        }

        [Fact]
        public async Task EndreFAQFeilModel()
        {
            // Arrange
            // FAQDTO mangler en required attributt, "dislikes".
            // det har ikke noe å si, det er introduksjonen med ModelError under som tvinger frem feilen
            // kunne også her brukt It.IsAny<FAQDTO>
            var faq1 = new FAQDTO
            {
                id = 1,
                likes = 2
            };

            mockAdmin.Setup(k => k.EndreLikesFAQ(faq1)).ReturnsAsync(true);

            var fAQController = new FAQController(mockAdmin.Object, mockAdminLogg.Object);

            fAQController.ModelState.AddModelError("dislikes", "Feil i inputvalidering på server");

            // Act
            var resultat = await fAQController.EndreLikesFAQ(faq1) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }
    }
}