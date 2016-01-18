using UnityEngine;
using System.Collections;
using System;
using Spellsystem;

[RequireComponent(typeof(ParticleSystem))]
public class FireSprayAnchor : MonoBehaviour, IVisual {

    ParticleSystem ps;
    SpellInformation spellInformation;
    bool authorized = false;

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
        if(authorized)
        {
            ApplyAll(other);
            authorized = false;
        }
    }

    public void AuthorizeCollisionDetection(ref SpellInformation spellInformation)
    {
        this.spellInformation = spellInformation;
        InvokeRepeating("setAuthorized", 0, 1);
    }

    public void ApplyAll(GameObject gobject)
    {
        ISpellInteraction effectReceiver = gobject.GetComponent(typeof(ISpellInteraction)) as ISpellInteraction; 
        if (effectReceiver != null)
        {
            effectReceiver.RecvEffect(spellInformation);
            effectReceiver.RecvDamage(spellInformation);
        }
    }

    public void setAuthorized()
    {
        authorized = true;
    }
}
