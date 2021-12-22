﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Star_Demo.Tasks
{
    public abstract class AGVTask
    {
        public long TaskStartTimeStamp { get; }

        public AGVTask()
        {
            TaskStartTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
