using System;
using System.Collections.Generic;
using System.Text;

namespace Ubuntu_Intelli_Compass.Services.Interfaces
{
    public interface IPinHasher
    {
        string Hash(string pin);

        bool Verify(string pin, string passwordHash);
    }
}
