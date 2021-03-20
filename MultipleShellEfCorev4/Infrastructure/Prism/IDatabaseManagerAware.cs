using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Prism
{
    public interface IDatabaseManagerAware
    {
        IDatabaseManager DatabaseManager { get; set; }
    }
}
