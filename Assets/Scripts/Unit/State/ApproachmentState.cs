using Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unit {
    public class ApproachmentState : BaseState
    {
        private const float rotationSpeed = 0.3f;
        private const float jumpForce = 3f;
        private const float jumpDelayMin = 0.25f;
        private const float jumpDelayMax = 1f;
        private BaseUnit self;
        private BaseUnit target;

        public ApproachmentState(BaseUnit self, BaseUnit target)
        {
            this.self = self;
            this.target = target;
        }

        public override void Enter()
        {
            self.StartCoroutine(Jumping());
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            self.transform.rotation = Quaternion.Slerp(self.transform.rotation,Quaternion.LookRotation(self.transform.position - target.transform.position), rotationSpeed);
            
        }

        public IEnumerator Jumping()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(jumpDelayMin, jumpDelayMax));
                var floor = Physics.RaycastAll(self.transform.position, Vector3.down, 1)
                    .Where(rh => rh.collider.gameObject != self)
                    .ToList();
                if (floor.Count > 0)
                {
                    var jumpDirection = ((target.transform.position - self.transform.position).normalized + Vector3.up).normalized;
                    self.GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }
}