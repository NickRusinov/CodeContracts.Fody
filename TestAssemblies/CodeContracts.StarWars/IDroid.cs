using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public interface IDroid : IRace
    {
        [Range(0, (byte)100)]
        int Health { get; set; }

        [return: This]
        IDroid FixSelf();
    }
}
