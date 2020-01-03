using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{

    [CreateAssetMenu(menuName = "KSkill/Passive")]
    public class AuraSkill : Ability
    {
        public Transition<ICharacter, Ability> _Transition;

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExecuteEnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void Procesing()
        {
            throw new System.NotImplementedException();
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