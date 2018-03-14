using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IUser
    {
        void Send(Message Message);
        Message Recieve();
    }
}
