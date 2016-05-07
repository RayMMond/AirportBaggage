using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage.Model
{
    public interface INext
    {
        void Next();
    }

    public interface IChangedFlight
    {
        void ChangedFlight(string oldFlight, string newFlight);
    }
}
