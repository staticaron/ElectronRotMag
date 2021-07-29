using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraController : MonoBehaviour
{
    private const string SensitivityPrefsName = "Sensitivity";
    private const int defaultSensitivity = 150;

    [SerializeField] bool isMobile = false;

    [SerializeField] CinemachineFreeLook vCam;

    [SerializeField, Space] Vector2 inputDirection;
    private Vector3 mousePos, prevMousePos;

    [SerializeField, Space] float lookSpeed;

    private bool isFingerDown = false;

    private int mobileLookMultiplier = 150;

    private void Start()
    {
        #region Get the platform details
#if UNITY_ANDROID
        isMobile = true;
#endif
        #endregion

        LoadSavedData();

        mousePos = prevMousePos = Vector3.zero;
        vCam = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        SetInputDirection();

        SetCamera();

        prevMousePos = mousePos;
    }

    private void LoadSavedData()
    {
        int sensitivity = PlayerPrefs.GetInt(SensitivityPrefsName);

        //In case sensitivity is not set, initialise it with the default value
        if(sensitivity == 0)
        {
            sensitivity = defaultSensitivity;
            PlayerPrefs.SetInt(SensitivityPrefsName, defaultSensitivity);
        }

        mobileLookMultiplier = sensitivity == 0 ? defaultSensitivity : sensitivity;
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
