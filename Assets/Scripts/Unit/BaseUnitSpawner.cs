using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class BaseUnitSpawner : MonoBehaviour 
    {
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private float radius;
        [SerializeField] private float spawnDelay;
        private int namesCounter;

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
                var baseUnit = Instantiate(unitPrefab, transform.position + Random.insideUnitSphere * radius, Quaternion.identity).GetComponent<BaseUnit>();
                baseUnit.gameObject.name = namesCounter.ToString();
                namesCounter++;
                BaseUnitMatchmaker.AddSearchingUnit(baseUnit);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}