using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject{
    public Transform prefab;
    [FormerlySerializedAs("Sprite")] public Sprite sprite;
    public string objectName;
}