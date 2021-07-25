using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraController : MonoBehaviour
{
    [SerializeField] bool isMobile = false;

    [SerializeField] CinemachineFreeLook vCam;

    [SerializeField, Space] Vector2 inputDirection;
    private Vector3 mousePos, prevMousePos;

    [SerializeField, Space] float lookSpeed;

    private bool isFingerDown = false;

    private const float mobileLookMultiplier = 200;

    private void Start()
    {
        #region Get the platform details
#if UNITY_ANDROID
        isMobile = true;
#endif
        #endregion

        mousePos = prevMousePos = Vector3.zero;
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
        if (isMobile == false)
        {
            if (Input.GetMouseButton(0))
            {
                mousePos = Input.mousePosition;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);

                //Get touch down details
                if (t.phase == TouchPhase.Began)
                {
                    isFingerDown = true;
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    isFingerDown = false;
                }

                if (isFingerDown == true)
                {
                    mousePos = t.position;
                }
            }
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
