using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronHead.MVRTFusion
{
    public class Key : MonoBehaviour, IClickable
    {
        public string content;
        public string shiftContent;
        [SerializeField] KeyboardManager keyboardManager;

        Renderer _renderer;
        [SerializeField] Material activeMat;
        [SerializeField] Material pressedMat;


        void Start()
        {
            _renderer = GetComponent<Renderer>();
        }


        // Update is called once per frame
        void Update()
        {

        }

        public void onClick()
        {
            if (keyboardManager.Shift)
            {
                keyboardManager.AddKey(shiftContent);
            }
            else
            {
                keyboardManager.AddKey(content);
            }
            StartCoroutine(nameof(DoOnClick));
        }
        private IEnumerator DoOnClick()
        {
            _renderer.material = pressedMat;
            yield return new WaitForSeconds(0.125f);
            _renderer.material = activeMat;
        }
    }
}