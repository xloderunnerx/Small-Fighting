using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit {
    public class UnitHealth : IDamagable
    {
        [SerializeField] private IDamagable damagable;

        public UnitHealth(IDamagable health) => this.damagable = health;

        public void TakeDamage(int damage) => damagable.TakeDamage(damage); // Could be with some damage correction logic.
    }
}