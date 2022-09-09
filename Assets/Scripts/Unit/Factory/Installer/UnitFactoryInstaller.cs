using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUnitFactory>()
                .To<UnitFactory>()
                .AsSingle();
        }
    }
}