using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SC.DAL;
using SC.BL.Domain;
using System.Collections.Generic;
using SC.BL;

namespace BLTest
{
    [TestClass]
    public class TicketManagerTest
    {
        Mock<ITicketRepository> mockRepo = new Mock<ITicketRepository>();
        TicketManager sut;


        [TestInitialize]
        public void Setup()
        {
            mockRepo.Setup(repo => repo.CreateTicket(It.IsAny<Ticket>())).Returns(((Ticket t) => t));
            mockRepo.Setup(repo => repo.CreateTicketResponse(It.IsAny<TicketResponse>())).Returns((TicketResponse t) => t);
            mockRepo.Setup(repo => repo.ReadTicketResponsesOfTicket(It.IsAny<int>())).Returns((List<TicketResponse> t) => t);
            ITicketRepository testRepo = mockRepo.Object;
            sut = new TicketManager(testRepo);
        }

        [TestMethod]
        public void AddTicketTest()
        {

            int accountid = 1;
            string question = "ik heb een probleem";
            Ticket newTicket = new Ticket()
            {
                AccountId = accountid,
                Text = question
            };
        
            Ticket returnedTicket = sut.AddTicket(accountid, question);

            mockRepo.Verify(repo => repo.CreateTicket(newTicket), Times.Exactly(1));
            Assert.AreEqual(accountid, returnedTicket.AccountId);
            Assert.AreEqual(question, returnedTicket.Text);

        }
    }
}
