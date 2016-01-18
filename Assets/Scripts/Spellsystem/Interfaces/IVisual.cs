using UnityEngine;
using System;


public delegate void ParticleCollisionEventHandler(GameObject other); // I leave out the "sender" to avoid overhead.

public interface IVisual
{
    void KillVisuals(float delay, bool stopBefore);
    void SetParentTransform(Transform transform);
    event ParticleCollisionEventHandler ParticlesCollided;
}

