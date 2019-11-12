using System;
using System.Collections.Generic;

namespace Entities
{
    [Serializable]
    public class DriverContainer
    {
        public List<Driver> Drivers;
    }

    [Serializable]
    public class Driver
    {
        public string Name;
        public string Distance;
        public string Status;
        public Coordinates Coordinates;
    }
}



