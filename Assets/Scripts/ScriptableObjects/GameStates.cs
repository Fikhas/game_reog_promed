using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStates : ScriptableObject, ISerializationCallbackReceiver
{
    public Signal enemyDeathSignal;

    public void OnAfterDeserialize()
    {
        enemyDeathSignal = null;
    }


    public void OnBeforeSerialize() { }
}
