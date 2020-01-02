using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace KSkill
{
    [System.Serializable]
    public class AttributeToChange
    {
        [Header("Properties of class | DOnt use private variables")]
        public string PropertiesName;
        [Header("Attribute use float as a wrapper")]
        public float NewValue;
    }

    [CreateAssetMenu(menuName = "KSkill/Action/ChangeAttribute")]
    public class ModifierAttribute : Action
    {
        public List<AttributeToChange> PropertiesChanged = new List<AttributeToChange>();

        public override void Act(ICharacter controller, List<ICharacter> targetBehaviour)
        {
            if (targetBehaviour.Available())
            {
                for (int i = 0; i < targetBehaviour.Count; i++)
                {                    
                    foreach (var attribute in PropertiesChanged)
                    {
                        ChangedProperties(targetBehaviour[i], attribute.PropertiesName, attribute.NewValue);
                    }
                }
            }
        }

        // Start is called before the first frame update
        protected void ChangedProperties(ICharacter target, string propertiesName, object newValue)
        {
            Type myType = typeof(ICharacter);

            PropertyInfo myFieldInfo = myType.GetProperty(propertiesName
                /*,BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public*/);

            // Change the field value using the SetValue method. 
            myFieldInfo.SetValue(target, Convert.ChangeType(newValue, myFieldInfo.PropertyType.GetTypeInfo().AsType()));

        }
    }
}