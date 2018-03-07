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
            testTicket.Text = "Dit is het testTicket van de Mock";
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
                testTickets.Add(new Ticket());
            }
            mockRepo.Setup(repo => repo.ReadTickets()).Returns(testTickets);

            //Returns a ticket with the requested ID
            mockRepo.Setup(repo => repo.ReadTicket(It.IsAny<int>())).Callback((int i) => testTicket.TicketNumber = i).Returns(testTicket);


            ITicketRepository testRepo = mockRepo.Object;
            sut = new TicketManager(testRepo);
        }


        //Read Action Tests
        [Test]
        public void GetTicketsTest()
        {
            //Arrange
            List<Ticket> resultList;

            //Act
            resultList = sut.GetTickets().ToList();

            //Assert
            mockRepo.Verify(repo => repo.ReadTickets(), Times.Exactly(1));
            Assert.AreEqual(5, resultList.Count());
        }

        [Test]
        public void GetTicketTest()
        {
            //Arrange
            Ticket resultTicket;
            int ticketNumber = 5;

            //Act
            resultTicket = sut.GetTicket(ticketNumber);

            //Assert
            mockRepo.Verify(repo => repo.ReadTicket(ticketNumber), Times.Exactly(1));
            Assert.AreEqual(ticketNumber, resultTicket.TicketNumber);
        }

        [Test]
        public void GetTicketResponsesTest()
        {
            //Arrange
            int ticketnumber = 1;

            //Act
            List<TicketResponse> resultList = sut.GetTicketResponses(ticketnumber).ToList();

            //Assert
            mockRepo.Verify(repo => repo.ReadTicketResponsesOfTicket(ticketnumber));
            foreach (TicketResponse res in resultList)
            {
                Assert.AreEqual(ticketnumber, res.Ticket.TicketNumber);
            }
        }


        //Create Action Tests
        [Test]
        public void AddTicketTest()
        {
            //Arrange
            Ticket testTicket = new Ticket()
            {
                TicketNumber = 1,
                AccountId = 1,
                Text = "Ik heb een probleem",
                DateOpened = DateTime.Now,
                State = TicketState.Open
            };

            //Act 
            //(AddTicket public zetten, later workaround)
            Ticket resultTicket = sut.AddTicket(testTicket);

            //Assert
            Assert.AreSame(testTicket, resultTicket);
            mockRepo.Verify(repo => repo.CreateTicket(testTicket));

        }

        [Test]
        public void AddTicketSimpleTest()
        {
            //Arrange
            int accountId = 3;
            string question = "Ik heb een vraag";


            //Act
            Ticket resultTicket = sut.AddTicket(accountId, question);

            //Assert
            Assert.AreEqual(accountId, resultTicket.AccountId);
            Assert.AreEqual(question, resultTicket.Text);
            mockRepo.Verify(repo => repo.CreateTicket(It.Is<Ticket>(t =>    t.Text == question && 
                                                                            t.AccountId == accountId)));

        }

        [Test]
        public void AddTicketDeviceTest()
        {
            //Arrange
            int accountId = 3;
            string question = "Ik heb een vraag";
            string device = "PC-1";



            //Act
            HardwareTicket resultTicket = (HardwareTicket) sut.AddTicket(accountId, device, question);

            //Assert
            Assert.AreEqual(accountId, resultTicket.AccountId);
            Assert.AreEqual(question, resultTicket.Text);
            Assert.AreEqual(device, resultTicket.DeviceName);
            mockRepo.Verify(repo => repo.CreateTicket(It.Is<HardwareTicket>(t =>    t.Text == question &&
                                                                                    t.AccountId == accountId && 
                                                                                    t.DeviceName == device)));

        }

        //Update Action Tests
        [Test]
        public void ChangeTicketTest()
        {
            //Arrange
            Ticket testTicket = new Ticket()
            {
                TicketNumber = 1,
                AccountId = 1,
                Text = "Ik heb een probleem",
                DateOpened = DateTime.Now,
                State = TicketState.Open
            };

            //Act
            sut.ChangeTicket(testTicket);

            //Assert
            mockRepo.Verify(repo => repo.UpdateTicket(testTicket));

        }

        [Test]
        public void AddTicketResponseTest()
        {
            //Arrange
            int ticketNumber = 1;
            string text = "Ik heb een probleem";
            bool isClientResponse = true;

            //Act
            TicketResponse result = sut.AddTicketResponse(ticketNumber, text, isClientResponse);

            //Assert
            mockRepo.Verify(repo => repo.ReadTicket(ticketNumber));
            mockRepo.Verify(repo => repo.ReadTicketResponsesOfTicket(ticketNumber));
            mockRepo.Verify(repo => repo.CreateTicketResponse(It.Is<TicketResponse>(res => res.Ticket.TicketNumber == ticketNumber && res.Text == text && res.IsClientResponse == isClientResponse)));
            Assert.AreEqual(ticketNumber, result.Ticket.TicketNumber);
            Assert.AreEqual(text, result.Text);
            mockRepo.Verify(repo => repo.UpdateTicket(It.Is<Ticket>(tic => tic.Responses.Contains(result))));



        }

        //Delete Action Tests
        [Test]
        public void RemoveTicketTest()
        {
            //Arrange
            int ticketNumber = 1;

            //Act
            sut.RemoveTicket(ticketNumber);

            //Assert
            mockRepo.Verify(repo => repo.DeleteTicket(ticketNumber));
        }
        



    }
}
