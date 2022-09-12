using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class DeathState : BaseState
    {
        private BaseUnit self;
        public DeathState(BaseUnit self)
        {
            this.self = self;
        }

        public override void Destroy()
        {
            
        }

        public override void Enter()
        {
            GameObject.Destroy(self.gameObject);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}