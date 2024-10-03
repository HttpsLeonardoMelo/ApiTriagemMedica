using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data.Interfaces.Util
{
    public interface IHashSenha
    {
        string Hash(string raw);
    }
}
