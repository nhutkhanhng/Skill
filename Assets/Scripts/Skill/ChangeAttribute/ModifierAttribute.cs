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

        public override void Act(ICharacter controller, TargetBehaviour targetBehaviour)
        {
            foreach(var attribute in PropertiesChanged)
            {
                ChangedProperties(controller, attribute.PropertiesName, attribute.NewValue);
            }
        }

        // Start is called before the first frame update
        protected void ChangedProperties(ICharacter controller, string propertiesName, object newValue)
        {
            Type myType = typeof(ICharacter);

            PropertyInfo myFieldInfo = myType.GetProperty("IdTeam"
                /*,BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public*/);

            // Change the field value using the SetValue method. 
            myFieldInfo.SetValue(controller, Convert.ChangeType(newValue, myFieldInfo.PropertyType.GetTypeInfo().AsType()));

        }
    }
}