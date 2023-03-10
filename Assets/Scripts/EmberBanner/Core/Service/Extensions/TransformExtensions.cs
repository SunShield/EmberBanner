using UnityEngine;

namespace EmberBanner.Core.Service.Extensions
{
    public static class TransformExtensions
    {
        public static void LookAt2D(this Transform tran, Vector3 destination, bool ortogonalFlip = false)
        {
            var normalizedDir = (tran.position - destination).normalized;
            var rotation = Mathf.Atan2(normalizedDir.y, normalizedDir.x) * Mathf.Rad2Deg;
            tran.rotation = Quaternion.Euler(0f, 0f, !ortogonalFlip ? rotation - 90 : rotation);
        }
    }
}