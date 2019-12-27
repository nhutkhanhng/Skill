using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono : MonoBehaviour
{
    public static SingletonMono Instace;
    public void Awake()
    {
        if (Instace != this)
        {
            Instace = this;
        }
    }
}


namespace KSkill
{
    public static class MathfMyMine
    {
        public static float Distance(ICharacter me, ICharacter target)
        {
            return Vector3.Distance(me.Position, target.Position);
        }


    }

    public class FindCharacter : SingletonMono
    {
        public List<ICharacter> FindAOECharacter(ICharacter caster, float Range = 1)
        {
            return null;
        }

        public ICharacter FindFarthestCharacter(ICharacter me, List<ICharacter> AllCharacter)
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
        public ICharacter FindNearestCharacter(ICharacter me, List<ICharacter> AllCharacter, float range)
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