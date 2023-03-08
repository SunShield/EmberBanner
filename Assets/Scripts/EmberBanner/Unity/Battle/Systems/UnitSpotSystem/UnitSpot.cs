﻿using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.UnitSpotSystem
{
    public class UnitSpot : EBMonoBehaviour
    {
        public BattleUnitView Unit { get; private set; }
        public bool IsFree => Unit == null;

        public void AddUnit(BattleUnitView unit)
        {
            Unit = unit;
            unit.Tran.parent = Tran;
            unit.Tran.localPosition = Vector3.zero;
        }

        public void RemoveUnit()
        {
            Unit = null;
        }
    }
}