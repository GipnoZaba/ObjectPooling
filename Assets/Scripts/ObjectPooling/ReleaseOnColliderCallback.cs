using UnityEngine;

namespace ObjectPooling
{
    public class ReleaseOnColliderCallback : ReleaseCallback
    {

        public bool isTrigger;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (isTrigger) return;

            Release();
        }
        
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (!isTrigger) return;
            
            Release();
        }
    }
}