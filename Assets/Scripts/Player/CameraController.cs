using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyBindingsSO _keys;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeedHor = 750;
    [SerializeField] private float _rotationSpeedVer = 750;
    [SerializeField] private float _rotationMinVer = -25;
    [SerializeField] private float _rotationMaxVer = 75;

    [Header("Camera Positions")]
    [SerializeField] private Vector3 _cameraPosFirstPerson;
    [SerializeField] private Vector3 _cameraPosThirdPerson;

    [Header("Camera Rotations")]
    [SerializeField] private Vector3 _cameraRotationFirstPerson;
    [SerializeField] private Vector3 _cameraRotationThirdPerson;

    [Header("Camera")]
    [SerializeField] private GameObject _camera;

    [Header("Camera Movement")]
    [SerializeField] private float _maxTime;
    private float _time = 0f;
    private float _lerp;

    private IEnumerator _corroutineCameraMovement;

    private void FixedUpdate()
    {
        ChangePOV();
        Rotation();
    }

    private IEnumerator MovingCamera()
    {
        if (_camera.transform.position == _cameraPosFirstPerson)
        {
            while (_time < _maxTime)
            {
                _time += Time.deltaTime;
                _lerp = _time / _maxTime;
                _camera.transform.position = Vector3.Lerp(_cameraPosFirstPerson, _cameraPosThirdPerson, _lerp);
                yield return null;
            }
            _camera.transform.localEulerAngles = _cameraRotationThirdPerson;
        }
        else
        {
            while (_time < _maxTime)
            {
                _time += Time.deltaTime;
                _lerp = _time / _maxTime;
                _camera.transform.position = Vector3.Lerp(_cameraPosThirdPerson, _cameraPosFirstPerson, _lerp);
                yield return null;
            }
            _camera.transform.localEulerAngles = _cameraRotationFirstPerson;
        }
        _time = 0f;
        _corroutineCameraMovement = null;
        yield return null;
    }

    private void ChangePOV()
    {
        if (Input.GetKeyUp(_keys.changePOV))
        {
            if (_corroutineCameraMovement != null) { }
            else
            {
                _corroutineCameraMovement = MovingCamera();
                StartCoroutine(_corroutineCameraMovement);
            }
        }
    }

    private void Rotation()
    {
        Vector3 rotation = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);

        rotation.x *= _rotationSpeedVer;
        rotation.y *= _rotationSpeedHor;

        if (-rotation.x <= _rotationMinVer)
        {
            rotation.x = -_rotationMinVer;
        }

        if (-rotation.x >= _rotationMaxVer)
        {
            rotation.x = -_rotationMaxVer;
        }

        rotation *= Time.fixedDeltaTime;

        transform.localEulerAngles += rotation;
    }
}
