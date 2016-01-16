using UnityEngine;
using System.Collections;
using Spellsystem;
using System;

namespace Player
{

    /// <summary>
    /// Handles all types of spells. Continuous, Charged, Burst. 
    /// </summary>
    
    [RequireComponent(typeof(ElementInput))]
    public class SpellHandler : MonoBehaviour
    {

        #region Editor
        public int MaxChargeTime;
        public int MaxRamp;

        [Tooltip("Defines how much ramp should be added")]
        public int RampStep;

        [Tooltip("After each interval the RampStep is applied")]
        public int RampInterval;
        #endregion

        float ChargeTime;
        bool charge = false;
        int Ramp;

        public event EventHandler SpellChargingStarting;
        public event EventHandler SpellChargingEnd;
        public event EventHandler SpellContinuousStarting;
        public event EventHandler SpellContinuousEnd;

        SpellLogic currentSpell;

        bool continuous;

        /// <summary>
        /// Gets called when a new Spell is generated
        /// </summary>
        /// <param name="sl">the generated spell</param>
        /// 

        public void OnSpellGenerated(SpellLogic sl)
        {
            currentSpell = sl;
            if (sl.Spelltype == SpellType.Charged)
            {
                // Set IsCharging() 
                charge = true;

                if (SpellChargingStarting != null)
                {
                    SpellChargingStarting(this, new EventArgs());
                }

                StartCoroutine("Charge");
            }

            else if (sl.Spelltype == SpellType.Continuous)
            {
                // Set Animation based on the Spellform like Spray. If the number of animations grows, we can add a Dictionary that contains Delegates, so it also has constant time.
                continuous = true;
                sl.gameObject.SetActive(true);

                if (SpellContinuousStarting != null)
                {
                    SpellContinuousStarting(this, new EventArgs());
                }

                StartCoroutine("Continuous");
            }
           
        }


        /// <summary>
        /// Starts the charging process
        /// </summary>
        /// <returns></returns>
        IEnumerator Charge()
        {
            while (ChargeTime < MaxChargeTime)
            {
                yield return new WaitForSeconds(0.5f);
                ChargeTime += 0.5f;
            }

            ChargeEnd();
        }

        private void ChargeEnd()
        {
            currentSpell.SetDamage(ChargeTime, 1); // Ramp = 1;
            currentSpell.gameObject.SetActive(true); // Turn it on.

            //Reset ChargeTime
            ChargeTime = 0;
            charge = false;
        }

        /// <summary>
        /// Starts the Continuous process; Damage gets handled here.
        /// </summary>
        /// <returns></returns>
        IEnumerator Continuous()
        {
            while (Ramp < MaxRamp)
            {
                yield return new WaitForSeconds(RampInterval);
                Ramp += RampStep;
                currentSpell.SetDamage(1, Ramp);
            }

            ContinuousEnd();
        }

        private void ContinuousEnd()
        {
            currentSpell.Kill();

            //Reset Ramp
            Ramp = 0;
            continuous = false;
        }

        /// <summary>
        /// Gets called when a button is released during the same frame.
        /// </summary>
        public void OnButtonUp()
        {
            if(charge)
            {
                StopAllCoroutines();
                
                if(SpellChargingEnd != null)
                {
                    SpellChargingEnd(this, new EventArgs());
                }

                ChargeEnd();
            }

            else if(continuous)
            {
                StopAllCoroutines();
                if (SpellContinuousEnd != null)
                {
                    SpellContinuousEnd(this, new EventArgs());
                }
                ContinuousEnd();
            }
        }

    }
}
