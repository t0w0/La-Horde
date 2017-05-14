using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

    public class cameraController : MonoBehaviour
    {
        
        [SerializeField] private MouseLook m_MouseLook;

        private Camera m_Camera;
	private Vector3 m_OriginalCameraPosition;

        // Use this for initialization
        private void Start()
        {
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
			m_MouseLook.Init(transform , m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();   
        }

        private void FixedUpdate()
        {

            m_MouseLook.UpdateCursorLock();
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }
    }
