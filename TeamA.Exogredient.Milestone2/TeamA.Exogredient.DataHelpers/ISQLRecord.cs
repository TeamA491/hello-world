﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeamA.Exogredient.DataHelpers
{
    public interface ISQLRecord
    {
        IDictionary<string, object> GetData();
    }
}
