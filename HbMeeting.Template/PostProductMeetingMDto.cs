using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbMeeting.Controllers.Manager
{
    public class PostProductMeetingMDto
    {
        public int Id { get; set; }

        public string MeetingName { get; set; }

        public string MeetingAreaName { get; set; }

        public int CompanyId { get; set; }

        public int ProductId { get; set; }

        public bool Check { get; set; } = true;
    }
}
