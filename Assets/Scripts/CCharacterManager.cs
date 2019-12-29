using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

using KSkill;
public class CCharacterManager : MonoSingleton<CCharacterManager>
{
    public List<KSkill.ICharacter> AllCharacter;

    public override void Init()
    {
        AllCharacter = new List<KSkill.ICharacter>();
        //AllCharacter.CollectionChanged += HandleChange;
    }

    private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
    {
        foreach (var x in e.NewItems)
        {
            // do something
        }

        if (e.Action == NotifyCollectionChangedAction.Move)
        {
            //do something
        }
    }
}
