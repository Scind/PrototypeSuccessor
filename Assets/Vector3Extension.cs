using System;
using UnityEngine;

namespace Assets.ExtensionMethods
{
    public static class Vector3Extension
    {
        public static void ClampValues(this Vector3 to_clamp, Vector3 min, Vector3 max)
        {
            to_clamp = new Vector3(Mathf.Clamp(to_clamp.x, min.x, max.x), Mathf.Clamp(to_clamp.y, min.y, max.y), Mathf.Clamp(to_clamp.z, min.z, max.z));
        }
    }
}
