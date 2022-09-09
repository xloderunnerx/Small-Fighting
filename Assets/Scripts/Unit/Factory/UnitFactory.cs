using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitFactory : IUnitFactory
    {
        private readonly DiContainer diContainer;

        public UnitFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public BaseUnit Create(BaseUnit baseUnit) => diContainer.InstantiatePrefab(baseUnit.gameObject).GetComponent<BaseUnit>();
    }
}