using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;
using System;

namespace IronHead.MVRTFusion
{
    public class LocalController : MonoBehaviour
    {

        public Transform relativeTo;

        [SerializeField] InputActionProperty _thumbstick;

        private void Awake()
        {
            if (relativeTo == null)
            {
                relativeTo = transform.parent;
            }

        }
        protected void OnEnable()
        {

            _thumbstick.action.Enable();
        }

        protected void OnDisable()
        {

            _thumbstick.action.Disable();
        }


        public Vector3 GetLocalPosition()
        {
            return relativeTo.InverseTransformPoint(transform.position);

        }
        public Quaternion GetLocalRotation()
        {
            return Quaternion.Inverse(relativeTo.rotation) * transform.rotation;
        }

        public void UpdateInputFixed(ref InputDataController container)
        {
            container.LocalPosition = GetLocalPosition();
            container.LocalRotation = GetLocalRotation();

            container.Movement = _thumbstick.action.ReadValue<Vector2>();
            container.Move = _thumbstick.action.ReadValue<Vector2>();


            container.Rotate = _thumbstick.action.ReadValue<Vector2>() * new Vector2(0, 1);



        }

    }
}