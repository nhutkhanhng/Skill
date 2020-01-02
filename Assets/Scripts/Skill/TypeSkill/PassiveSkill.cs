using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill {

    [CreateAssetMenu(menuName = "KSkill/Passive")]
    public class PassiveSkill : Ability
    {
        public Transition _Transition;

        public override void EnterState()
        {            
        }

        public override void ExitState()
        {
        }

        public override void Procesing()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            // Call Update trigger
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}