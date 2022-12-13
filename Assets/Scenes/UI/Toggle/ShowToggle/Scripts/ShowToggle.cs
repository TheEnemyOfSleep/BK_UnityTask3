using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _checkmarkImage;
    [SerializeField] private Color _checkmarkDefaultColor;
    private Color _checkmarkActiveColor;


    public Toggle Toggle
    {
        get => _toggle;
        set => _toggle = value;
    }
    public Image CheckmarkImage
    {
        get => _checkmarkImage;
        set => _checkmarkImage = value;
    }
    public Color CheckmarkDefaultColor
    {
        get => _checkmarkDefaultColor;
        set => _checkmarkDefaultColor = value;
    }
    public Color CheckmarkActiveColor
    {
        get => _checkmarkActiveColor;
        set => _checkmarkActiveColor = value;
    }

    private void Awake()
    {
        Toggle = GetComponent <Toggle> ();
        CheckmarkActiveColor = CheckmarkImage.color;

        Toggle.onValueChanged.AddListener(OnCheck);
    }

    private void OnCheck(bool isChecked)
    {
        CheckmarkImage.color = isChecked ? CheckmarkActiveColor : CheckmarkDefaultColor;
    }

    private void OnDestroy()
    {
        Toggle.onValueChanged.RemoveListener(OnCheck);
    }
}
