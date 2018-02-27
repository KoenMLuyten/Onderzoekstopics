﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SC.BL;
using SC.BL.Domain;
using SC.DAL.EF;

namespace SC.UI.Web.MVC.Controllers.Api
{
    public class TicketController : ApiController
    {
        private static TicketRepository repository = new TicketRepository();
        private ITicketManager mgr = new TicketManager(repository);

        [HttpPut]
        [Route("api/Ticket/{id}/State/Closed")]
        public IHttpActionResult PutTicketStateToClosed(int id)
        {
            mgr.ChangeTicketStateToClosed(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
