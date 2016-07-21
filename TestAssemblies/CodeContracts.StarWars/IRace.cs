using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public interface IRace
    {
        [Enum]
        Sentience Sentience { get; }

        [Range(0L, long.MaxValue)]
        long Population { get; set; }
    }
}
