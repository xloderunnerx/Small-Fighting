using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class SearchingState : BaseState
    {
        private BaseUnit self;
        private BaseUnit target;
        private IMatchmakingService matchmakingService;
        private Action<BaseUnit> onTargetFound;

        public SearchingState(BaseUnit unit, IMatchmakingService matchmakingService, Action<BaseUnit> onTargetFound)
        {
            this.self = unit;
            this.matchmakingService = matchmakingService;
            this.onTargetFound = onTargetFound;
        }

        public override void Enter()
        {
            matchmakingService.Matchmake(self, MatchmakeWith);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }

        public void MatchmakeWith(BaseUnit target)
        {
            this.target = target;
            onTargetFound?.Invoke(target);
        }

        public override void Destroy()
        {
            
        }
    }
}