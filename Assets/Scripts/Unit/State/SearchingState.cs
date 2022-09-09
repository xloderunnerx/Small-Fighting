using Core;
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

        public SearchingState(BaseUnit unit, IMatchmakingService matchmakingService)
        {
            this.self = unit;
            this.matchmakingService = matchmakingService;
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
            self.target = target;
        }
    }
}