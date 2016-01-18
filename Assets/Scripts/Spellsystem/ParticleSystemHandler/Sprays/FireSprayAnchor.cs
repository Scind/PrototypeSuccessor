using UnityEngine;
using System.Collections;
using System;


[RequireComponent(typeof(ParticleSystem))]
public class FireSprayAnchor : MonoBehaviour, IVisual {

    ParticleSystem ps;

    public event ParticleCollisionEventHandler ParticlesCollided;

    public void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void KillVisuals(float delay, bool stopBefore)
    {
        if (stopBefore) ps.Stop(true);
        Destroy(this.gameObject, delay);
    }

    public void SetParentTransform(Transform transform)
    {
        this.transform.SetParent(transform);
    }

    public void OnParticleCollision(GameObject other)
    {
        if(ParticlesCollided != null)
        {
            ParticlesCollided(other);
        }
    }
}
