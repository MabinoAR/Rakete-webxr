using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

namespace IronHead.MVRTFusion
{
    public class VRCameraPostProcess : MonoBehaviour
    {
        Volume _ppVolume;
        Vignette _ppVignette;

        [Header("Vignette")]
        [SerializeField] Vector2 vignette;


        [Header("PlayerInput")]
        [SerializeField] InputActionProperty _look;
        private void Start()
        {
            _ppVolume = GetComponent<Volume>();
            _ppVolume.profile.TryGet(out _ppVignette);

        }
        private void Update()
        {
            Vignette(_look.action.ReadValue<Vector2>().x);
        }
        public void Vignette(float alpha)
        {
            _ppVignette.intensity.value = Mathf.Lerp(vignette.x, vignette.y, alpha);
        }
    }
}