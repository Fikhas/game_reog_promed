using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnPoint : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialSpawnCordinat;

    // [HideInInspector]
    public Vector2 runtimeSpawnCordinat;

    public void OnAfterDeserialize()
    {
        runtimeSpawnCordinat = initialSpawnCordinat;
    }

    public void OnBeforeSerialize() { }

}
