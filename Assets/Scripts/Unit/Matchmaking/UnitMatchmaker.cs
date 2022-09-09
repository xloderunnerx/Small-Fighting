using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unit
{
    public class UnitMatchmaker : IMatchmakingService
    {
        private Dictionary<BaseUnit, Action<BaseUnit>> matchmakingUnits;

        public UnitMatchmaker()
        {
            this.matchmakingUnits = new Dictionary<BaseUnit, Action<BaseUnit>>();
        }

        public void Matchmake(BaseUnit reuquester, Action<BaseUnit> onMatchmake)
        {
            matchmakingUnits.Add(reuquester, onMatchmake);
            TryMatchmakeWithClosest(reuquester);
        }

        private void TryMatchmakeWithClosest(BaseUnit requester)
        {
            var validatedDictionary = matchmakingUnits.Where(mu => mu.Key != requester)
                .ToDictionary(k => k.Key, v => v.Value);
            if (validatedDictionary.Count == 0)
                return;
            var minDistance = validatedDictionary.Select(vmu => Vector3.Distance(vmu.Key.transform.position, requester.transform.position))
                .Min();
            var closestUnit = validatedDictionary.Where(vmu => Vector3.Distance(vmu.Key.transform.position, requester.transform.position) == minDistance)
                .FirstOrDefault();
            var requesterKeyPair = matchmakingUnits.Where(mu => mu.Key == requester)
                .FirstOrDefault();
            closestUnit.Value?.Invoke(requester);
            requesterKeyPair.Value?.Invoke(closestUnit.Key);
            matchmakingUnits.Remove(closestUnit.Key);
            matchmakingUnits.Remove(requester);
            DebugColors(closestUnit.Key, requester);
        }

        private void DebugColors(BaseUnit a, BaseUnit b)
        {
            var rndColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            a.GetComponent<MeshRenderer>().material.color = rndColor;
            b.GetComponent<MeshRenderer>().material.color = rndColor;
        }        
    }
}