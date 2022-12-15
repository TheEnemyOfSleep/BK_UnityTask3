using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OpacityToggleChange : MonoBehaviour
{
    [SerializeField] private MultipleToggleGroup _multipleToggleGroup;
    [SerializeField] private ToggleGroup _toggleGroup;

    public MultipleToggleGroup MultipleToggleGroup
    {
        get => _multipleToggleGroup;
        set => _multipleToggleGroup = value;
    }

    public ToggleGroup ToggleGroup
    {
        get => _toggleGroup;
        set => _toggleGroup = value;
    }

    private void Awake()
    {
        ToggleGroup = GetComponent<ToggleGroup>();
    }

    public void OnToggleChanged()
    {
        OpacityToggleValues toggleValues = ToggleGroup.ActiveToggles().FirstOrDefault().GetComponent<OpacityToggleValues>();
        SetObjectOpacity(toggleValues.ObjectOpacity);
    }

    private void SetObjectOpacity(float opacity)
    {
        List<Toggle> activeToggles = MultipleToggleGroup.ActiveToggles().ToList();
        foreach (Toggle toggle in activeToggles)
        {
            toggle.GetComponentInParent<ObjectLogic>().SetOpacityToText(opacity);
        }
    }
}
