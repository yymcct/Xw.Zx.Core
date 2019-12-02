using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbMeeting.Controllers.Manager
{ 
    public class ProductMeetingMDto
    {
        public int Id { get; set; }

        public int MeetingId { get; set; }

        public string MeetingName { get; set; }

        public int MeetingAreaId { get; set; }

        public string MeetingAreaName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public bool Check { get; set; } = true;

        public DateTime AddTime { get; set; }

        public string AddUserName { get; set; }
    }
}
