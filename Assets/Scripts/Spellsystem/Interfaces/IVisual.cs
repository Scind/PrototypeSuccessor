using UnityEngine;

public interface IVisual
{
    void KillVisuals(float delay, bool stopBefore);
    void SetParentTransform(Transform transform);
}

