using System;
using System.Collections.Generic;

namespace Entities
{
    [Serializable]
    public class ClientContainer
    {
        public List<Client> Clients;
    }

    [Serializable]
    public class Client
    {
        public string id;
        public string name;
        public string Status;
        public Coordinates Coordinates;
    }
}

