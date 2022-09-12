using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unit {
    public class ApproachmentState : BaseState
    {
        private const float rotationSpeed = 0.3f;
        private const float jumpForce = 3f;
        private const float jumpDelayMin = 0.25f;
        private const float jumpDelayMax = 1f;
        private const float minimumTargetDistance = 4.5f;
        private BaseUnit self;
        private BaseUnit target;
        private Coroutine jumping;
        private Action<BaseUnit> onApproach;
        private Action onTargetLost;

        public ApproachmentState(BaseUnit self, BaseUnit target, Action<BaseUnit> onApproach, Action onTargetLost)
        {
            this.self = self;
            this.target = target;
            this.onApproach = onApproach;
            this.onTargetLost = onTargetLost;
        }

        public override void Enter()
        {
            jumping = self.StartCoroutine(Jumping());
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            if (!ValidateTarget())
                return;
            self.transform.rotation = Quaternion.Slerp(self.transform.rotation,Quaternion.LookRotation(target.transform.position - self.transform.position), rotationSpeed);
        }

        public bool ValidateTarget()
        {
            if (target != null)
                return true;
            onTargetLost?.Invoke();
            return false;
        }

        public IEnumerator Jumping()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(jumpDelayMin, jumpDelayMax));
                if (target == null)
                    yield break;
                if (Vector3.Distance(self.transform.position, target.transform.position) <= minimumTargetDistance)
                {
                    self.StopCoroutine(jumping);
                    onApproach?.Invoke(target);
                }
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

        public override void Destroy()
        {
            self.StopCoroutine(jumping);
        }
    }
}