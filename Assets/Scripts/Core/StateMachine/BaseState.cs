using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class BaseState
    {
        public BaseStateMachine StateMachine { get; private set; }
        public void InitStateMachine(BaseStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public abstract void Destroy();
    }
}