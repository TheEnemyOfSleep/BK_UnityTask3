using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineButton : MonoBehaviour
{
    [SerializeField] Toggle _button;
    [SerializeField] Canvas _outliner;

    public Toggle Button
    {
        get => _button;
        set => _button = value;
    }

    public Canvas Outliner
    {
        get => _outliner;
        set => _outliner = value;
    }

    private void Awake()
    {
        Button = GetComponent<Toggle>();

        Button.onValueChanged.AddListener(OnChanged);
    }

    private void OnChanged(bool isChecked)
    {
        Outliner.enabled = isChecked;
    }
}
