using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Prism
{
    public interface IDatabaseManager
    {
        IDatabaseManager CreateDatabaseManager();
        string DatabaseName { get; set; }
    }
}
