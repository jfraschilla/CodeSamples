using System;
using System.Collections.Generic;
using System.Text;

namespace MultipleShellEfCore.Infrastructure
{
    public interface IShellService
    {
        void ShowShell(string uri);
        void ShowShell(string uri, string filename);

    }
}
