using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public static class MathfMyMine
    {
        public static float Distance(ICharacter me, ICharacter target)
        {
            return Vector3.Distance(me.Position, target.Position);
        }


    }

    public static class FindCharacter
    {
        public static List<ICharacter> FindAOECharacter(ICharacter caster, float Range)
        {
            return FindAOECharacter(caster, CCharacterManager.Instance.AllCharacter, Range);
        }


        public static List<ICharacter> FindAOECharacter(ICharacter caster, List<ICharacter> AllCharacter, float Range = 1)
        {
            List<ICharacter> result = new List<ICharacter>();

            Debug.LogError(AllCharacter.Count);
            for (int i = 0; i < AllCharacter.Count; i++)
            {
                var Target = AllCharacter[i];
                Debug.LogError(Target.IdTeam != caster.IdTeam);

                if (Target.IdTeam != caster.IdTeam && Target.Equals(caster) == false)
                {
                    if (MathfMyMine.Distance(caster, Target) <= Range)
                    {
                        result.Add(Target);
                    }
                }
            }

            if (result.Count == 0)
                return null;

            return result;
        }

        public static ICharacter FindFarthestCharacter(ICharacter me, List<ICharacter> AllCharacter)
        {
            ICharacter Character = null;
            float Max = 0;

            foreach (var character in AllCharacter)
            {
                if (Max < MathfMyMine.Distance(me, character))
                {
                    Max = MathfMyMine.Distance(me, character);
                    Character = character;
                }
            }

            return Character;
        }
        public static ICharacter FindNearestCharacter(ICharacter me, List<ICharacter> AllCharacter, float range)
        {
            ICharacter CharacterNearest = null;
            float Min = 0;

            foreach (var character in AllCharacter)
            {
                if (Min > MathfMyMine.Distance(me, character))
                {
                    Min = MathfMyMine.Distance(me, character);
                    CharacterNearest = character;
                }
            }

            return CharacterNearest;
        }
    }
}