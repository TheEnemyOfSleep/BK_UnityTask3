using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Extensions/Custom Toggle")]
    [RequireComponent(typeof(RectTransform))]
    public class CustomToggle : Toggle
    {
        [SerializeField]
        private MultipleToggleGroup m_multipleGroup;

        /// <summary>
        /// Group the toggle belongs to.
        /// </summary>
        public MultipleToggleGroup multipleGroup
        {
            get { return m_multipleGroup; }
            set
            {
                SetMultipleToggleGroup(value, true);
                PlayEffect(true);
            }
        }

        public new bool isOn
        {
            get { return base.isOn; }

            set
            {
                Set(value);
            }
        }

        protected CustomToggle()
        {}

        protected override void OnEnable()
        {
            base.OnEnable();
            SetMultipleToggleGroup(m_multipleGroup, false);
            PlayEffect(true);
        }

        protected override void OnDisable()
        {
            SetMultipleToggleGroup(null, false);
            base.OnDisable();
        }

        private void SetMultipleToggleGroup(MultipleToggleGroup newGroup, bool setMemberValue)
        {
            // Sometimes IsActive returns false in OnDisable so don't check for it.
            // Rather remove the toggle too often than too little.
            if (m_multipleGroup != null)
                m_multipleGroup.UnregisterToggle(this);

            // At runtime the group variable should be set but not when calling this method from OnEnable or OnDisable.
            // That's why we use the setMemberValue parameter.
            if (setMemberValue)
                m_multipleGroup = newGroup;

            // Only register to the new group if this Toggle is active.
            if (newGroup != null && IsActive())
                newGroup.RegisterToggle(this);

            // If we are in a new group, and this toggle is on, notify group.
            // Note: Don't refer to m_Group here as it's not guaranteed to have been set.
            if (newGroup != null && isOn && IsActive())
                newGroup.NotifyToggleOn(this);
        }

        void Set(bool value, bool sendCallback = true)
        {
            if (isOn == value)
                return;

            // if we are in a group and set to true, do group logic
            base.isOn = value;
            if (group != null && group.isActiveAndEnabled && IsActive())
            {
                if (isOn || (!group.AnyTogglesOn() && !group.allowSwitchOff))
                {
                    base.isOn = true;
                    group.NotifyToggleOn(this, sendCallback);
                }
            }

            // if we are in a multiple group and set value, do multiple group logic
            if (m_multipleGroup != null && m_multipleGroup.isActiveAndEnabled && IsActive())
            {
                base.isOn = value;
                m_multipleGroup.NotifyToggleOn(this, sendCallback);
            }

            // Always send event when toggle is clicked, even if value didn't change
            // due to already active toggle in a toggle group being clicked.
            // Controls like Dropdown rely on this.
            // It's up to the user to ignore a selection being set to the same value it already was, if desired.
            PlayEffect(toggleTransition == ToggleTransition.None);
            if (sendCallback)
            {
                UISystemProfilerApi.AddMarker("Toggle.value", this);
                onValueChanged.Invoke(isOn);
            }
        }

        private void PlayEffect(bool instant)
        {
            if (graphic == null)
                return;

#if UNITY_EDITOR
            if (!Application.isPlaying)
                graphic.canvasRenderer.SetAlpha(isOn ? 1f : 0f);
            else
#endif
            graphic.CrossFadeAlpha(isOn ? 1f : 0f, instant ? 0f : 0.1f, true);
        }

        private void InternalToggle()
        {
            if (!IsActive() || !IsInteractable())
                return;

            isOn = !isOn;
        }

        /// <summary>
        /// React to clicks.
        /// </summary>
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            InternalToggle();
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            InternalToggle();
        }
    }
}
