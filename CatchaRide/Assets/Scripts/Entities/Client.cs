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
        public string Name;
        public string Status;
        public Coordinates Coordinates;
    }
}

