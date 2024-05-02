using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game
{
    public interface ICameraController
    {
        void SetFollowTarget(Transform target);
    }


    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraController : MonoBehaviour, ICameraController
    {
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }


        public void SetFollowTarget(Transform target)
        {
            _virtualCamera.Follow = target;
        }
    }
}