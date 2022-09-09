using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "SO/Core/Value/RandomIntVariable")]
    public class RandomIntVariable : Variable<int>
    {
        public int minHealth;
        public int maxHealth;
        public override int Value
        {
            get => Random.Range(minHealth, maxHealth);
        }
    }
}