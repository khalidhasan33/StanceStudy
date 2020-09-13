using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float intialValue;

    [HideInInspector]
    public float RunTimeValue;

    public void OnAfterDeserialize() {
        RunTimeValue = intialValue;
    }

    public void OnBeforeSerialize() { }
}
