using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class OpacityToggleValues : MonoBehaviour
{
    [SerializeField] private float _ObjectOpacity;

    public float ObjectOpacity
    {
        get => _ObjectOpacity;
        set => _ObjectOpacity = value;
    }
}
