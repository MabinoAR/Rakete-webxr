using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace IronHead.MVRTFusion
{
    public class LineRendererHandler : MonoBehaviour
    {
        LineRenderer lineRenderer;
        Vector3[] points;

        [SerializeField] private LayerMask layerMask;
        [SerializeField] InputActionProperty _interact;
        // Start is called before the first frame update
        void Start()
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();

            points = new Vector3[2];


            points[0] = transform.position;
            points[1] = transform.position + transform.forward * 20;

            lineRenderer.SetPositions(points);
            lineRenderer.enabled = true;
        }
        // Update is called once per frame
        void Update()
        {
            AlignLineRenderer(lineRenderer);

        }

        public void AlignLineRenderer(LineRenderer lineRenderer)
        {
            points[0] = transform.position;

            Ray ray;
            ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 20f, layerMask))
            {
                points[1] = hit.point;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;



                if (_interact.action.triggered)
                {

                    hit.transform.GetComponent<IClickable>().onClick();
                }
            }
            else
            {
                points[1] = transform.position + transform.forward * 20;

                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            lineRenderer.SetPositions(points);
            lineRenderer.material.color = this.lineRenderer.startColor;


        }

    }
}