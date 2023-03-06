using UnityEngine;

namespace EmberBanner.Unity.Service
{
    public class EBMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;
        public Transform Tran => _transform ??= GetComponent<Transform>();
    }
}