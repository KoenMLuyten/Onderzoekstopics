using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SC.DAL;
using System.ComponentModel.DataAnnotations;
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

      mockRepo.Verify(repo => repo.CreateTicket(It.Is<Ticket>(t => t.Text == question && t.AccountId == accountid)), Times.Exactly(1));
      Assert.AreEqual(accountid, returnedTicket.AccountId);
      Assert.AreEqual(question, returnedTicket.Text);

    }
    [TestMethod]
    public void Validatetest()
    {
      Ticket ticket = new Ticket
      {
        AccountId = 2,
        Text = "Ik heb een probleem",
        DateOpened = DateTime.Now
      };
      try
      {
        sut.Validate(ticket);
      }
      catch
      {
        Assert.Fail("Validation Failed");
      }
    }
    [TestMethod]
    public void Validatetest2()
    {
    Ticket ticket2 = new Ticket();
    ticket2.AccountId = 2;
    ticket2.Text = "nog steeds een probleem maar ditmaal met veel langere vraag, langer dan 100 tekens. nog steeds een probleem maar ditmaal met veel langere vraag, langer dan 100 tekens.";
    ticket2.DateOpened = DateTime.Now;
    ticket2.State = TicketState.Open;
    //Ticket ticket = sut.AddTicket(accountid, question);
    Assert.ThrowsException<ValidationException>(() => sut.Validate(ticket2));
    }
    [TestMethod] //gebruikt reflection om de methode te benaderen
    public void Validatetestprivate()
    {
      ITicketRepository testRepo = mockRepo.Object;
      PrivateObject privatehelper = new PrivateObject(typeof(TicketManager), 
        new[] {typeof(ITicketRepository)}, new[] {testRepo});
      Ticket ticket = new Ticket
      {
        AccountId = 2,
        Text = "Ik heb een probleem",
        DateOpened = DateTime.Now
      };
      try
      {
        privatehelper.Invoke("Validate", ticket);
      }
      catch
      {
        Assert.Fail("Validation Failed");
      }
    }
}
}
