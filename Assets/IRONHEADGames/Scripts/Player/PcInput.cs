using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class PcInput : MonoBehaviour
    {
        [SerializeField] InputActionProperty _move;
        [SerializeField] InputActionProperty _look;

        [SerializeField] Transform _rootTransform;
        [SerializeField] Transform _cameraTransform;

        NetworkPcInput _data;



        private void Start()
        {
            var events = NetworkManager.Instance.Runner.GetComponent<NetworkEvents>();
            events.OnInput.AddListener(OnInput);

            Cursor.lockState = CursorLockMode.Locked;

        }

        private void OnDestroy()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        void OnInput(NetworkRunner runner, NetworkInput input)
        {
            _data.move = _move.action.ReadValue<Vector2>();
            _data.look = _look.action.ReadValue<Vector2>();
            input.Set(_data);
        }
    }
}