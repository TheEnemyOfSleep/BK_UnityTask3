using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI//Extensions/Multiple Toggle Group")]
public class MultipleToggleGroup : UIBehaviour
{
    protected List<Toggle> m_Toggles = new List<Toggle>();

    [Serializable]
    /// <summary>
    /// UnityEvent callback for when a custom toggle in toggle group is toggled.
    /// </summary>
    public class MultipleGroupEvent : UnityEvent<Toggle>
    {}

    public MultipleGroupEvent onValueChanged = new MultipleGroupEvent();

    protected MultipleToggleGroup()
    {}

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void ValidateToggleIsInGroup(Toggle toggle)
    {
        if (toggle == null || !m_Toggles.Contains(toggle))
            throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", toggle, this));
    }

    public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
    {
        ValidateToggleIsInGroup(toggle);

        onValueChanged.Invoke(toggle);
    }

    public void UnregisterToggle(Toggle toggle)
    {
        if (m_Toggles.Contains(toggle))
            m_Toggles.Remove(toggle);
    }

    public void RegisterToggle(Toggle toggle)
    {
        if (!m_Toggles.Contains(toggle))
            m_Toggles.Add(toggle);
    }

    public bool AnyTogglesOn()
    {
        return m_Toggles.Find(x => x.isOn) != null;
    }

    public bool IsAllTogglesOn()
    {
        return m_Toggles.Find(x => !x.isOn) == null;
    }

    public IEnumerable<Toggle> ActiveToggles()
    {
        return m_Toggles.Where(x => x.isOn);
    }

    public void SetAllTogglesOff(bool sendCallback = true)
    {
        if (sendCallback)
        {
            for (var i = 0; i < m_Toggles.Count; i++)
                m_Toggles[i].isOn = false;
        }
        else
        {
            for (var i = 0; i < m_Toggles.Count; i++)
                m_Toggles[i].SetIsOnWithoutNotify(false);
        }
    }

    public void SetAllTogglesOn(bool sendCallback = true)
    {
        if (sendCallback)
        {
            for (var i = 0; i < m_Toggles.Count; i++)
                m_Toggles[i].isOn = true;
        }
        else
        {
            for (var i = 0; i < m_Toggles.Count; i++)
                m_Toggles[i].SetIsOnWithoutNotify(true);
        }
    }
}
