using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BaseStateMachine
    {
        public BaseState CurrentState { get; private set; }

        public void InitNewState(BaseState newState)
        {
            CurrentState = newState;
            CurrentState.InitStateMachine(this);
            CurrentState.Enter();
        }

        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();
            InitNewState(newState);
        }

        public void Update()
        {
            if (CurrentState == null)
                return;
            CurrentState.Update();
        }

        public void Destroy()
        {
            CurrentState.Destroy();
        }
    }
}