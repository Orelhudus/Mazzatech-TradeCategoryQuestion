using System;
using System.Collections.Generic;
using System.Text;

namespace TradeCategoryQuestion.Interfaces
{
    interface ITrade
    {
        double Value { get; }

        string ClientSector { get; }

        DateTime NextPaymentDate { get; }
    }
}
