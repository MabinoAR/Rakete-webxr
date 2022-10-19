using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronHead.MVRTFusion
{
    public class SpecialKeys : MonoBehaviour, IClickable
    {
        public enum SpecialKey { shift, enter, backspace, space, clear, cancel }

        [SerializeField] private SpecialKey currentKey;

        [SerializeField] KeyboardManager keyboardManager;


        Renderer _renderer;
        [SerializeField] Material activeMat;
        [SerializeField] Material pressedMat;

        // Start is called before the first frame update
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
            switch (currentKey)
            {
                case SpecialKey.shift:
                    keyboardManager.OnShift();
                    break;
                case SpecialKey.enter:
                    keyboardManager.OnEnter();
                    break;
                case SpecialKey.backspace:
                    keyboardManager.OnBackspace();
                    break;
                case SpecialKey.space:
                    keyboardManager.AddKey(" ");
                    break;
                case SpecialKey.clear:
                    keyboardManager.OnClear();
                    break;
                case SpecialKey.cancel:
                    break;
                default:
                    break;
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