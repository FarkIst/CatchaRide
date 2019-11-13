using System;
using System.Collections.Generic;

namespace Entities
{
    [Serializable]
    public class ClientContainer
    {
        public List<client> clients;
    }

    [Serializable]
    public class client
    {
        public string id;
        public string clientName;
        public string Status;
        public Coordinates Coordinates;
    }
}

