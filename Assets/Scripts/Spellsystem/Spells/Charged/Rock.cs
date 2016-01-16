using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Spellsystem
{
    [RequireComponent(typeof(SpellMovement))]
    public sealed class Rock : SpellLogic, VFXHandler
    {
        public override SpellType Spelltype
        {
            get
            {
                return SpellType.Charged;
            }
        }

        public override SpellForm Spellform
        {
            get
            {
                return SpellForm.Rock;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            SpellInformation.Spellform = SpellForm.Rock;
            SpellInformation.Spelltype = SpellType.Charged;
            StartVisuals();
            GetComponent<SpellMovement>().StartMovement(SpellInformation.ChargeTime);            
        }

        public override void Kill()
        {
            RaiseSpellExpired();
            Destroy(this.gameObject);
        }

        public void DeathVisuals()
        {
            throw new NotImplementedException();
        }

        public void StartVisuals()
        {
        }
    }
}
