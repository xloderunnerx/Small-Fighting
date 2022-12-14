using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private BaseUnit unitPrefab;
        [SerializeField] private float radius;
        [SerializeField] private float spawnDelay;
        private int namesCounter;
        private IUnitFactory unitFactory;

        [Inject]
        public void Construct(IUnitFactory unitFactory)
        {
            this.unitFactory = unitFactory;
        }

        private void Start()
        {
            StartSpawning();
        }

        public void StartSpawning() => StartCoroutine(Spawning());
        public void StopSpawning() => StopAllCoroutines();
        private IEnumerator Spawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                var baseUnit = unitFactory.Create(unitPrefab);
                baseUnit.gameObject.name = namesCounter.ToString();
                baseUnit.transform.position = transform.position + Random.insideUnitSphere * radius;
                namesCounter++;
            }
        }

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, radius);
    }
}