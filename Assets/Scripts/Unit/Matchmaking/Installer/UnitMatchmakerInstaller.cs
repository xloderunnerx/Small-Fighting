using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitMatchmakerInstaller : MonoInstaller
    {
        [SerializeField] private UnitMatchmaker baseUnitMatchmaker;
        public override void InstallBindings()
        {
            Container.Bind<UnitMatchmaker>().FromInstance(baseUnitMatchmaker).AsSingle();
        }
    }
}