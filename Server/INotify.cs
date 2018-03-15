using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface INotify
    {
        void JoinChat(string userName);
        void LeaveChat(string userName);

    }
}
