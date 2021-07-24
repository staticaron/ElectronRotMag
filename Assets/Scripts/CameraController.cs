using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook vCam;

    [SerializeField] Vector2 inputDirection;
    private Vector3 mousePos, prevMousePos;

    [SerializeField, Space] float lookSpeed;

    private const float mobileLookMultiplier = 200;

    private void Start()
    {
        mousePos = prevMousePos = Input.mousePosition;
        vCam = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        SetInputDirection();

        SetCamera();

        prevMousePos = mousePos;
    }

    //Get the input
    private void SetInputDirection()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
        }
        inputDirection = (mousePos - prevMousePos).normalized;
    }

    //Set the camera's values
    private void SetCamera()
    {
        vCam.m_YAxis.Value -= inputDirection.y * lookSpeed * Time.deltaTime;
        vCam.m_XAxis.Value += inputDirection.x * mobileLookMultiplier * lookSpeed * Time.deltaTime;
    }
}
