using UnityEngine;
using System.Collections;


namespace Spellsystem
{
    public interface ISpellInteraction
    {
        /// <summary>
        /// This function handles the effect input / state change
        /// </summary>
        /// <param name="information">necessary data</param>
        void RecvEffect(SpellInformation information);
        
        /// <summary>
        /// This function handles the damage input
        /// </summary>
        /// <param name="information">necessary data</param>
        void RecvDamage(SpellInformation information);

    }
}