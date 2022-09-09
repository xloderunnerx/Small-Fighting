using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

namespace Unit
{
    public class UnitMatchmaker : MonoBehaviour // Static isn't good technicuqe and should be replaced with DI container, i understand that.
    {
        [SerializeField] private static List<BaseUnit> searchingUnits;
        [SerializeField] private static List<BaseUnit> busyUnits;
        public static event Action<BaseUnit> OnSearchingUnitAdded;

        private void Awake()
        {
            searchingUnits = new List<BaseUnit>();
            busyUnits = new List<BaseUnit>();
        }

        public static BaseUnit GetClosestUnit(BaseUnit unit)
        {
            var validatedList = searchingUnits
                .Where(bu => bu != unit)
                .ToList();
            if (validatedList.Count == 0)
                return null;
            var minDistance = validatedList
                .Select(bu => Vector3.Distance(bu.transform.position, unit.transform.position))
                .Min();
            var closestUnit = validatedList.Where(bu => Vector3.Distance(bu.transform.position, unit.transform.position) == minDistance).FirstOrDefault();
            return closestUnit;
        }

        public static void AddSearchingUnit(BaseUnit unit)
        {
            searchingUnits.Add(unit);
            OnSearchingUnitAdded?.Invoke(unit);
        }

        public static void SetUnitBusy(BaseUnit unit)
        {
            searchingUnits.Remove(unit);
            busyUnits.Add(unit);
        }

        public static void SetUnitSearching(BaseUnit unit)
        {
            busyUnits.Remove(unit);
            searchingUnits.Add(unit);
            OnSearchingUnitAdded?.Invoke(unit);
        }

        public static bool IsUnitBusy(BaseUnit unit)
        {
            if (busyUnits.Contains(unit))
                return true;
            return false;
        }
    }
}