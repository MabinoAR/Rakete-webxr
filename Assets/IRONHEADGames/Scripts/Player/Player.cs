using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] Transform head;

        [SerializeField] Transform cameraTransform;
        public Hand leftHand;
        public Hand rightHand;


        private NetworkCharacterControllerPrototype _networkCharacterController;


        [Header("Options")]
        [Header("VR")]
        [SerializeField] bool useHeadsetPosition = false;

        void Awake()
        {

            _networkCharacterController = GetComponent<NetworkCharacterControllerPrototype>();


        }




        [SerializeField] Vector2 _lookSpeed = new Vector2(.5f, -.5f);



        public override void FixedUpdateNetwork()
        {

            if (GetInput<InputData>(out var input))
            {


                if (useHeadsetPosition)
                {
                    head.localPosition = input.HeadLocalPosition;
                }

                head.localRotation = input.HeadLocalRotation;


                var rootAngle_y = head.transform.rotation.eulerAngles.y;
                Vector3 lookVectorForward = Quaternion.AngleAxis(rootAngle_y, Vector3.up) * Vector3.forward;
                Vector3 lookVectorRight = Quaternion.AngleAxis(rootAngle_y, Vector3.up) * Vector3.right;


                var moveVelocity = input.Left.Movement.x * lookVectorRight + input.Left.Movement.y * lookVectorForward;

                _networkCharacterController.Move(moveVelocity);

                _networkCharacterController.TeleportToRotation(Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, input.Right.Movement.x, 0)), null, false);




                leftHand.UpdateInput(input.Left);
                rightHand.UpdateInput(input.Right);

            }


            if (GetInput<NetworkPcInput>(out NetworkPcInput inputPc))
            {


                var moveDelta = inputPc.move;

                var rootAngle_y = transform.rotation.eulerAngles.y;
                Vector3 lookVectorForward = Quaternion.AngleAxis(rootAngle_y, Vector3.up) * Vector3.forward;
                Vector3 lookVectorRight = Quaternion.AngleAxis(rootAngle_y, Vector3.up) * Vector3.right;
                var moveVelocity = moveDelta.x * lookVectorRight + moveDelta.y * lookVectorForward;


                _networkCharacterController.Move(moveVelocity);


                var lookDelta = inputPc.look;
                _networkCharacterController.TeleportToRotation(Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, lookDelta.x * _lookSpeed.x, 0f)));
                cameraTransform.Rotate(lookDelta.y * _lookSpeed.y, 0f, 0f, Space.Self);

            }
        }



    }
}