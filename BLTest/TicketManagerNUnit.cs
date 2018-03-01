using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Moq;
using SC.DAL;
using SC.BL;
using SC.BL.Domain;

namespace BLTest
{
    [TestFixture]
    class TicketManagerNUnit
    {
        Mock<ITicketRepository> mockRepo = new Mock<ITicketRepository>();
        TicketManager sut;

        [OneTimeSetUp]
        public void MockSetup()
        {
            //Create operations simply return te given object
            mockRepo.Setup(repo => repo.CreateTicket(It.IsAny<Ticket>())).Returns(((Ticket t) => t));
            mockRepo.Setup(repo => repo.CreateTicketResponse(It.IsAny<TicketResponse>())).Returns((TicketResponse t) => t);

            //List read operations return a list counting five of the requested object
            //Ticketresponses include a Ticket with the requeste TicketID
            List<TicketResponse> testResponses = new List<TicketResponse>();
            Ticket testTicket = new Ticket();
            for (int i = 0; i < 5; i++)
            {
                TicketResponse tr = new TicketResponse();
                tr.Ticket = testTicket;
                testResponses.Add(tr);
            }
            mockRepo.Setup(repo => repo.ReadTicketResponsesOfTicket(It.Is<int>(i => i != 0))).Callback((int i) => testResponses.ForEach(tr => tr.Ticket.TicketNumber = i)).Returns(testResponses);
            mockRepo.Setup(repo => repo.ReadTicketResponsesOfTicket(It.Is<int>(i => i == 0))).Returns((new List<TicketResponse>()));


            List<Ticket> testTickets = new List<Ticket>();
            for (int i = 0; i < 5; i++)
            {
                testResponses.Add(new Ticket());
            }
            mockRepo.Setup(repo => repo.ReadTickets()).Returns(testTickets);

            //Returns a ticket with the requested ID
            mockRepo.Setup(repo => repo.ReadTicket(It.IsAny<int>())).Callback((int i) => testTicket.TicketNumber = i).Returns(testTicket);


            ITicketRepository testRepo = mockRepo.Object;
            sut = new TicketManager(testRepo);
        }


    }
}
