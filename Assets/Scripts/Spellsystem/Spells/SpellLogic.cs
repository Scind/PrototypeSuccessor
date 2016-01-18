using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Spellsystem
{

    public enum SpellType { Charged, Continuous, Burst }
    public enum SpellForm
    {
        Spray,
        Rock
    }

    /// <summary>
    /// Base class for every Spell in game. 
    /// To create a new Spell inherit from this. This should only contains the logic. Movement etc. should be handled in component class. 
    /// </summary>
    /// 

    
    public abstract class SpellLogic : MonoBehaviour
    {

        public delegate void SpellExpiredHandler(object sender, EventArgs e);
        public event SpellExpiredHandler SpellExpired;

        protected SpellInformation SpellInformation;
       
        /// <summary>
        /// Actual Lifetime of a spell. Controls how long the spell can be seen on screen. Editor
        /// </summary>
        public int LifetimeInSeconds;

        /// <summary>
        /// This should be the bone/socket of the staff for example.
        /// </summary>
        public Transform StaffTransform { get; set; }


        public abstract SpellType Spelltype { get; }
        public abstract SpellForm Spellform { get; }

        protected virtual void Awake()
        {
            Invoke("Kill", LifetimeInSeconds);
            transform.position = StaffTransform.position;
            transform.rotation = StaffTransform.rotation; 

        }

        public void SetDamage(float charge, int ramp)
        {
            SpellInformation.ChargeTime = charge;
            SpellInformation.Ramp = ramp;
        }

        /// <summary>
        /// Use this to apply the damage. To apply it, test if the gobject implements IDamageAble; It true, call RecvDmg by passing the Dmg specified above;
        /// The object "knows" how to handle that damage. E.g. The player has warded !ESS and you want to apply Pure death damage, the spell doesn't need to know that the player has warded !ESS.
        /// But the player will internally handle this damage input and turn it down to zero.
        /// You guys know what i mean :D
        /// 
        /// Use SendMessage OR GetComponents<typeof(IDamageAble)>() on gobject to get the interface. 
        /// </summary>
        /// <param name="gobject">The gameobject that should recv the Dmg.</param>

        public void ApplyDamage(GameObject gobject)
        {
            ISpellInteraction damageReceiver = gobject.GetComponent(typeof(ISpellInteraction)) as ISpellInteraction;
            if (damageReceiver != null)
            {
                damageReceiver.RecvDamage(SpellInformation);
            }
        }

        protected void RaiseSpellExpired()
        {
            if (SpellExpired != null)
            {
                SpellExpired(this, new EventArgs());
            }
        }

        public void SetElements(List<Element> elements)
        {
            SpellInformation.Elements = elements;
        }

        /// <summary>
        /// Use this to apply an specific effect 
        /// Create your own Interface (e.g. IFlamable) and check if the GameObject implements it. If true, just call your methods defined in your Interface. 
        /// </summary>
        /// <param name="gobject"> The object that should recv the effect</param>
        public void ApplyEffect(GameObject gobject)
        {
            ISpellInteraction effectReceiver = gobject.GetComponent(typeof(ISpellInteraction)) as ISpellInteraction;
            if(effectReceiver != null) effectReceiver.RecvEffect(SpellInformation);
        }



        /// <summary>
        /// Implement this to handle everything before destroying this. E.g. make something fade out etc.
        /// </summary>
        public abstract void Kill();
        
    }
}