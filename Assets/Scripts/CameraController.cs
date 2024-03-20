using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Set an object of spectating")] private GameObject _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _sensetivity;
    
    [Space]
    [SerializeField] private float _clampLow;
    [SerializeField] private float _clampHigh;
    [SerializeField] private InputReader _reader;
    
    private float inputX;
    private float inputY;

    private enum View
    {
        thirdPerson,
        firstPerson
    }

    private View viewMode;

    void Start()
    {
        viewMode = View.thirdPerson;
        Cursor.lockState = CursorLockMode.Locked;
        _reader.LookEvent += HandleLook;
        _reader.ToggleViewEvent += HandleToggleView;
        
    }

    void Update()
    {
        inputY = Mathf.Clamp(inputY, _clampLow, _clampHigh);
    }

    private void LateUpdate()
    {
        ThirdPersonCam();
        FirstPersonCam();
    }

    private void ThirdPersonCam()
    {
        if (viewMode == View.thirdPerson)
        {
            Vector3 camPosY = _player.transform.position + new Vector3(0, _offset.y, 0);
            Quaternion rotation = Quaternion.Euler(inputY, inputX, 0);
            Vector3 positionOffset = rotation * new Vector3(0, 0, -_offset.z) + camPosY;

            transform.position = Vector3.Lerp(transform.position, positionOffset, 0.5f);
            transform.LookAt(camPosY);
        }
    }

    private void FirstPersonCam()
    {
        if (viewMode == View.firstPerson)
        {
            var camY = _player.transform.position + new Vector3(0, _offset.y, 0);
            transform.position = camY;
            transform.rotation = Quaternion.Euler(-inputY, inputX + 180, 0);
        }
    }

    private void HandleLook(Vector2 dir)
    {
        inputX += dir.x * _sensetivity * Time.deltaTime;
        inputY += dir.y * _sensetivity * Time.deltaTime;
    }

    private void HandleToggleView()
    {
        if (viewMode == View.thirdPerson)
        {
            viewMode = View.firstPerson;
        }
        else
        {
            viewMode = View.thirdPerson;
        }
    }
}