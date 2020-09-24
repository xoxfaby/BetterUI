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
                return (float) ((int) chance + Math.Pow(chance % 1, Math.Abs(luck) + 1));
            }
            else
            {
                return (float) ((int)chance + (1 - Math.Pow(1 - (chance % 1), Math.Abs(luck) + 1)));
            }
        }
    }
}
