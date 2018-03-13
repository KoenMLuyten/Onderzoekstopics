using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Web.Mvc;
using SC.UI.Web.MVC.Controllers;
using SC.BL;
using SC.BL.Domain;
using Moq;

namespace UI_MVCTest



{
    [TestFixture]
    class TicketControllerTest
    {
        [OneTimeSetUp]
        public void MockSetup()
        {
            Mock<ITicketManager> mockMgr = new Mock<ITicketManager>();

            //Lijst met testTickets en generiek testTicket aanmaken
            List<Ticket> testTickets = new List<Ticket>();
            Ticket testTicket = new Ticket();        
            for (int i = 0; i < 5; i++)
            {
                testTicket.AccountId = i + 2;
                testTicket.DateOpened = DateTime.Now;
                testTicket.Text = String.Format("Issue #: {0}", i);
                testTicket.TicketNumber = i;
                if (i%2 == 0)
                {
                    testTicket.State = TicketState.Open;
                }
                else { testTicket.State = TicketState.Closed; }
                testTickets.Add(testTicket);
            }

            //Read actions mocken
            mockMgr.Setup(mgr => mgr.GetTickets()).Returns(testTickets);
            mockMgr.Setup(mgr => mgr.GetTicket(It.IsAny<int>())).Callback((int i) => testTicket.TicketNumber = i).Returns(testTicket);

            //Write actions mocken
            mockMgr.Setup(mgr => mgr.AddTicket(It.IsAny<int>(), It.IsAny<string>())).Callback((int i) => testTicket)


            var sut = new TicketController(mockMgr.Object);
        }

    }
}
