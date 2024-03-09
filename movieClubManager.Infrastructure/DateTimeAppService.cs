using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieClubManager.Contracts.Interfaces;

namespace MovieClubManager.Infrastructure
{
    public class DateTimeAppService : DateTimeService
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}
