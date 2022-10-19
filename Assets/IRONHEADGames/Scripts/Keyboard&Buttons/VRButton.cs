using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

namespace IronHead.MVRTFusion
{
    public class VRButton : MonoBehaviour, IClickable
    {
        [Serializable]
        public class KeyboardUpdateEvent : UnityEvent { }

        public KeyboardUpdateEvent DoOnClick = new KeyboardUpdateEvent();

        [SerializeField] bool interactable = true;
        [SerializeField] Image _image;
        [SerializeField] Color activeColor;
        [SerializeField] Color disableColor;

        [ExecuteInEditMode]
        // Start is called before the first frame update
        private void Awake()
        {
            Interactable(interactable);
        }
        void Start()
        {
            _image = GetComponent<Image>();

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void onClick()
        {
            if (interactable)
            {
                DoOnClick.Invoke();
            }

        }
        public void Interactable(bool state)
        {
            interactable = state;
            if (interactable)
            {
                _image.color = activeColor;
            }
            else
            {
                _image.color = disableColor;
            }
        }
    }
}