using System;
using UnityEngine;

namespace Spellsystem
{

    /// <summary>
    /// Creates a Spray.
    /// This is the final version for all types of Sprays like a Fire Spray.
    /// If you want to create a specific spell like a Fire Spray you have to do this via the editor only. pls see the README.txt
    /// </summary>
    /// 


    public sealed class Spray : SpellLogic, VFXHandler
    {
        public float KillVisualsAfter;

        IVisual particleSystemAnchor;
        public override SpellForm Spellform
        {
            get
            {
                return SpellForm.Spray;
            }
        }

        public override SpellType Spelltype
        {
            get
            {
                return SpellType.Continuous;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            SpellInformation.Spelltype = SpellType.Continuous;
            SpellInformation.Spellform = SpellForm.Spray;
            StartVisuals();
        }

        public override void Kill()
        {
            // Wanna add some visual effects here? Call DeathVisuals(). Be sure to implement it first.
            RaiseSpellExpired();
            particleSystemAnchor.KillVisuals(KillVisualsAfter, true);
            Destroy(this.gameObject);
        }

        public void DeathVisuals()
        {
            // No death visuals here.
        }


        public void StartVisuals()
        { 
            ParticleSystem ps = Instantiate(VFXPool.Instance.Sprays[SpellInformation.Elements[0].ToString()], StaffTransform.position, StaffTransform.rotation) as ParticleSystem;
            ps.transform.SetParent(StaffTransform);
            particleSystemAnchor = ps.GetComponent<IVisual>();
        }
    }
}