using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject{
    public Transform prefab;
    public Sprite Sprite;
    public string objectName;
}