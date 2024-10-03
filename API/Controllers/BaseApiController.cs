using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.UoW;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IUnitOfWork _uow;
        
        public BaseApiController(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}