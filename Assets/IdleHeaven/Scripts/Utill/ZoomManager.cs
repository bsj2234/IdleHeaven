using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera _originalCam;
    [SerializeField] private CinemachineVirtualCamera _zoomCamera;
    [SerializeField] private float _zoomInSize;

    private bool zoomed = false;

    public void ZoomTrigger()
    {
        zoomed = !zoomed;

        if(zoomed)
        {
            _cinemachineBrain.
        }
    }


}
