using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbMeeting.Models
{
    public class ProductMeeting
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int ProductId  { get; set; }

        public int MeetingId { get; set; }

        public int MeetingAreaId { get; set; }

        public bool Check { get; set; } = true;

        public DateTime AddTime { get; set; } = DateTime.Now;

        public int AddUserId { get; set; }
    }
}
