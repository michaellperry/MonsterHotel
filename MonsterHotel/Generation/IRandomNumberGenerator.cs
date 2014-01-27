using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Generation
{
    public interface IRandomNumberGenerator
    {
        int GetNumber(int maxNumber);
    }
}
