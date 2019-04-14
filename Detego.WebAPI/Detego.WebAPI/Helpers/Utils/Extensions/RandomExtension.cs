using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Helpers.Utils
{
    public static class RandomExtension
    {

        /// <summary>
        /// Generates decimal number between 0 and 1
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static decimal GenerateDecimal(this Random rng)
        {

            return (decimal) rng.NextDouble();

        }
    }
}
