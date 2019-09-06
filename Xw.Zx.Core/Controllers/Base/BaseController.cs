using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly XwZxContext _context;
        public readonly IMapper _mapper;
        public readonly ISieveProcessor _sieveProcessor;
        public BaseController(XwZxContext context)
        {
            _context = context;
        }
        public BaseController(XwZxContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        private Member _member = null;

        public Member Member
        {
            get
            {
                if (_member != null)
                {
                    return _member;
                }
                else
                {
                    var id = int.Parse(User.Claims.Where(l => l.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                                           .FirstOrDefault().Value);
                    _member = _context.Members.First(c => c.Id == id);
                    return _member;
                }
            }
        }
    }

}
