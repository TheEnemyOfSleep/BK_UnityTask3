using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAllToggle : CheckToggle
{
    [SerializeField]  private MultipleToggleGroup _multipleGroup;

    public MultipleToggleGroup MultipleGroup
    {
        get => _multipleGroup;
        set => _multipleGroup = value;
    }

    private void Awake()
    {
        Toggle = GetComponent<Toggle>();

        if (MultipleGroup == null)
            _multipleGroup = GetComponentInParent<MultipleToggleGroup>();

        BackgroundActiveColor = BackgroundImage.color;

        Toggle.onValueChanged.AddListener(OnCheck);
        _multipleGroup.onValueChanged.AddListener(OnToggled);
    }

    private void ChangeColor(bool isChecked)
    {
        if (BorderImage != null)
            BorderImage.enabled = isChecked ? true : false;
        BackgroundImage.color = isChecked ? BackgroundActiveColor : BackgroundDefaultColor;
    }

    private void OnToggled(Toggle toggle)
    {
        bool isChecked = _multipleGroup.IsAllTogglesOn() ? true : false;
        Toggle.SetIsOnWithoutNotify(isChecked);
        ChangeColor(isChecked);
    }

    private void OnCheck(bool isChecked)
    {
        ChangeColor(isChecked);
        if (isChecked)
            _multipleGroup.SetAllTogglesOn();
        else
            _multipleGroup.SetAllTogglesOff();
    }

    private void OnDestroy()
    {
        Toggle.onValueChanged.RemoveListener(OnCheck);
        _multipleGroup.onValueChanged.RemoveListener(OnToggled);
    }
}
