using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class Hand : NetworkBehaviour
    {
        public Hand OtherHand;
        [SerializeField] Transform _visuals;
        public LocalController LocalController { get; set; }

        public void SetLocalController(LocalController other)
        {
            LocalController = other;

            if (LocalController != null)
            {
                var nt = GetComponent<NetworkTransform>();
                nt.InterpolationDataSource = InterpolationDataSources.NoInterpolation;
            }
        }
        public void UpdateInput(InputDataController input)
        {
            UpdatePose(input.LocalPosition, input.LocalRotation);
            //UpdateLocalPose( input.LocalPosition, input.LocalRotation );

        }

        void UpdatePose(Vector3 localPosition, Quaternion localRotation)
        {

            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
        }
        public void UpdateLocalPose(Vector3 localPosition, Quaternion localRotation)
        {

            _visuals.position = transform.parent.TransformPoint(localPosition);
            _visuals.rotation = transform.parent.rotation * localRotation;
        }
        public override void Render()
        {
            if (LocalController != null)
            {
                UpdateLocalPose(LocalController.GetLocalPosition(), LocalController.GetLocalRotation());
            }
        }
    }
}