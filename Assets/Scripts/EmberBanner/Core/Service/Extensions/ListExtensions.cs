﻿using System;
using System.Collections.Generic;

namespace EmberBanner.Core.Service.Extensions
{
    public static class ListExtensions
    {
        private static Random rng = new ();
        
        public static void Shuffle<T>(this IList<T> list)  
        {  
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = rng.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
    }
}