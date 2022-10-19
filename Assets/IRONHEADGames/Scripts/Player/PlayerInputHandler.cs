using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class PlayerInputHandler : MonoBehaviour
    {
        InputData data;
        [SerializeField] bool onlySendInputWhenFocused;
        [SerializeField] Transform relativeTo;
        [SerializeField] Transform head;

        public LocalController LeftController;
        public LocalController RightController;

        [SerializeField] InputActionAsset _actionMap;

        protected void OnEnable()
        {
            _actionMap.Enable();
        }

        protected void OnDisable()
        {
            _actionMap.Disable();
        }

        private void Awake()
        {
            if (relativeTo == null)
            {
                relativeTo = transform.parent;
            }
        }
        private void Start()
        {
            var networkedParent = GetComponentInParent<NetworkObject>();

            var events = NetworkManager.Instance.Runner.GetComponent<NetworkEvents>();
            events.OnInput.AddListener(OnInput);

            var player = networkedParent.GetComponent<Player>();
            if (player != null)
            {
                player.leftHand.SetLocalController(LeftController);
                player.rightHand.SetLocalController(RightController);

            }
        }

        void OnInput(NetworkRunner runner, NetworkInput inputContainer)
        {

            if (onlySendInputWhenFocused && Application.isFocused == false)
            {
                return;
            }
            data.HeadLocalPosition = relativeTo.InverseTransformPoint(head.position);
            data.HeadLocalRotation = Quaternion.Inverse(relativeTo.rotation) * head.rotation;


            if (LeftController != null && RightController != null)
            {
                LeftController.UpdateInputFixed(ref data.Left);
                RightController.UpdateInputFixed(ref data.Right);
            }



            inputContainer.Set(data);
        }
    }
}