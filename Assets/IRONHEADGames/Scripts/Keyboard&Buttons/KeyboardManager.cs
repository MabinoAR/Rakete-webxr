using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace IronHead.MVRTFusion
{
    public class KeyboardManager : MonoBehaviour
    {
        [SerializeField] bool isActive;
        [SerializeField] GameObject keys;

        public string Text;

        [Serializable] public class KeyboardUpdateEvent : UnityEvent<string> { }
        [Serializable] public class KeyboardSubmitEvent : UnityEvent<string> { }


        public KeyboardUpdateEvent OnUpdate = new KeyboardUpdateEvent();

        public KeyboardSubmitEvent OnSubmit = new KeyboardSubmitEvent();

        public bool Shift { get; private set; }

        private void Start()
        {
            SetKeyboarsActive(isActive);
        }
        public void AddKey(string key)
        {
            Text += key;
            OnUpdate.Invoke(Text);

        }
        public void OnEnter()
        {
            OnSubmit.Invoke(Text);
        }
        public void OnBackspace()
        {
            if (Text.Length > 0)
            {
                Text = Text.Substring(0, Text.Length - 1);
            }
            OnUpdate.Invoke(Text);
        }
        public void OnClear()
        {
            Text = null;
            OnUpdate.Invoke(Text);
        }
        public void OnShift()
        {
            Shift = !Shift;
        }

        public void SetKeyboarsActive(bool state)
        {
            isActive = state;
            keys.SetActive(isActive);
        }

    }
}