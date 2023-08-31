using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageRef : ScriptableObject, ISerializationCallbackReceiver
{
    public GameObject stage;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize() { }

}