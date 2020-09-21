using System;
using System.Collections.Generic;
using System.Text;

namespace BetterUI
{
    static class Utils
    {
        public static float LuckCalc(float chance, float luck)
        {
            if (luck == 0)
            {
                return chance;
            }
            else if (luck < 0)
            {
                return (float)Math.Pow(chance % 100 / 100, Math.Abs(luck) + 1) * 100;
            }
            else
            {
                return (float)(1 - Math.Pow(1 - (chance % 100 / 100), Math.Abs(luck) + 1)) * 100;
            }
        }
    }
}
