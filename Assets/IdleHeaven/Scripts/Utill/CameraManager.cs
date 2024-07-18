using Cinemachine;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase _currentCam;
    [SerializeField] private CinemachineVirtualCameraBase _mainCam;
    [SerializeField] private CinemachineVirtualCameraBase _zoomedCam;


    public CinemachineVirtualCameraBase CurrentCam
    {
        get => _currentCam;
        set
        {
            _currentCam.Priority = 50;
            _currentCam = value;
            _currentCam.Priority = 100;
        }
    }

    public void ToggleZoom()
    {
        CurrentCam = (CurrentCam == _mainCam) ? _zoomedCam : _mainCam;
    }
}
