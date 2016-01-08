using UnityEngine;
using System.Collections.Generic;
using System;

namespace Spellsystem
{
    public class VFXPool : MonoBehaviour
    {
        public static VFXPool Instance { get; private set; }

        
        public Dictionary<string, ParticleSystem> Sprays = new Dictionary<string, ParticleSystem>();

        [Tooltip("Specify the identifications of the spray particle system")]
        public List<string> SprayName;

        [Tooltip("Specify the particle systems of the identifications in SprayName")]
        public List<ParticleSystem> SprayPS;

        public List<string> RockName;
        public List<ParticleSystem> RockPS;

        public Dictionary<string, ParticleSystem> Rock = new Dictionary<string, ParticleSystem>();

        public List<string> AOEName;
        public List<ParticleSystem> AOEPS;

        public Dictionary<string, ParticleSystem> AOE = new Dictionary<string, ParticleSystem>();
        

        void Awake()
        {
            if(Instance != null && Instance != this )
            {
                Destroy(gameObject);
            }

            Instance = this;

            addRange(SprayName, SprayPS, Sprays);
            SprayName = null;
            SprayPS = null;

        }


        private void addRange(List<string> ident, List<ParticleSystem> ps, Dictionary<string, ParticleSystem> dict)
        {
            for(int i = 0; i<ident.Count;i++)
            {
                dict.Add(ident[i], ps[i]);
            }
        }
    }
}