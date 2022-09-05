using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearIdleState : BaseState
{
    private GameObject weapon;
    private float handlingSpeed;

    public SpearIdleState(GameObject weapon, float handlingSpeed)
    {
        this.weapon = weapon;
        this.handlingSpeed = handlingSpeed;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, Quaternion.LookRotation(Vector3.up), handlingSpeed);
    }
}
