using System;

namespace EmberBanner.Core.Service.Classes.Fundamental
{
    /// <summary>
    /// This class holds information about growth rate of something:
    ///
    /// For each 'Span' of difference value grows for 'Growth'
    /// </summary>
    [Serializable]
    public class IntGrowthRate
    {
        public int Span;
        public int Growth;
    }
}