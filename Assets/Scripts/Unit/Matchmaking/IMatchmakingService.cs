using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public interface IMatchmakingService
    {
        void Matchmake(BaseUnit baseUnit, Action<BaseUnit> onMatchmake);
    }
}