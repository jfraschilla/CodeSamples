using System;
using System.Collections.Generic;
using System.Text;

namespace MultiShellEfCore.Infrastructure.Prism
{
    public interface IDatabaseBehavior
    {
        //
        // Summary:
        //     The region that this behavior is extending.
        IRegion Region
        {
            get;
            set;
        }

        //
        // Summary:
        //     Attaches the behavior to the specified region.
        void Attach();

    }
}
