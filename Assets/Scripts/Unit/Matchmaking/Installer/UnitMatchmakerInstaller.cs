using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitMatchmakerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMatchmakingService>()
                .To<UnitMatchmaker>()
                .AsSingle();
        }
    }
}