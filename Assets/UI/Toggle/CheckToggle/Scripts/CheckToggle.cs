using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _backgroundDefaultColor;
    [SerializeField] private Image _borderImage;
    private Color _backgroundActiveColor;


    public Toggle Toggle
    {
        get => _toggle;
        set => _toggle = value;
    }
    public Image BackgroundImage
    {
        get => _backgroundImage;
        set => _backgroundImage = value;
    }
    public Color BackgroundDefaultColor
    {
        get => _backgroundDefaultColor;
        set => _backgroundDefaultColor = value;
    }
    public Image BorderImage
    {
        get => _borderImage;
        set => _borderImage = value;
    }
    public Color BackgroundActiveColor
    {
        get => _backgroundActiveColor;
        set => _backgroundActiveColor = value;
    }

    private void Awake()
    {
        Toggle = GetComponent<Toggle>();
        BackgroundActiveColor = BackgroundImage.color;

        Toggle.onValueChanged.AddListener(OnCheck);
    }

    private void OnCheck(bool isChecked)
    {
        if (BorderImage != null)
            BorderImage.enabled = isChecked ? true : false;
        BackgroundImage.color = isChecked ? BackgroundActiveColor : BackgroundDefaultColor;
    }

    private void OnDestroy()
    {
        Toggle.onValueChanged.RemoveListener(OnCheck);
    }
}
