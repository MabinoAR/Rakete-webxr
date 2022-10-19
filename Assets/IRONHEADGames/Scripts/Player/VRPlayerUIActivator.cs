using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IronHead.MVRTFusion
{
    public class VRPlayerUIActivator : MonoBehaviour
    {
        [SerializeField] GameObject UI;

        [SerializeField] InputActionProperty _activator;


        private void OnEnable()
        {
            _activator.action.performed += OnActivate;
        }
        private void OnDisable()
        {
            _activator.action.performed -= OnActivate;
        }

        void OnActivate(InputAction.CallbackContext context)
        {
            UI.SetActive(!UI.activeSelf);
        }
    }
}