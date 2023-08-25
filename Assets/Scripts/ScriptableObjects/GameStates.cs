using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStates : ScriptableObject, ISerializationCallbackReceiver
{
    public Signal enemyDeathSignal;
    public StageRef currenStage;
    public StageRef initialStage;

    public void OnAfterDeserialize()
    {
        enemyDeathSignal = null;
        currenStage = initialStage;
    }


    public void OnBeforeSerialize() { }
}
