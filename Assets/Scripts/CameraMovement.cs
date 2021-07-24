using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;

    [SerializeField, Space] Transform target;
    [SerializeField] Vector3 inputDirection;
    [SerializeField] float camSpeed;

    [SerializeField, Space] float minYAngle;
    [SerializeField] float maxYAngle;

    [SerializeField, Space] bool isMobile;

    private Vector3 currentMousePos, prevMousePos;

    private void Awake()
    {
        currentMousePos = prevMousePos = transform.position;

        #region Get Platform Data
#if UNITY_ANDROID
        isMobile = true;
#endif
#if UNITY_EDITOR_WIN
        isMobile = false;
#endif
        #endregion
    }

    private void Update()
    {
        inputDirection = GetInput();

        SetTransform(inputDirection);

        prevMousePos = currentMousePos;
    }

    Vector3 GetInput()
    {
        Vector3 mouseDirection = Vector3.zero;

        //Get the touch/click direction
        if (isMobile)
        {
            //Get Touch Input
        }
        else
        {
            currentMousePos = Input.mousePosition;

            mouseDirection = (currentMousePos - prevMousePos).normalized;
        }

        return mouseDirection;
    }

    void SetTransform(Vector3 direction)
    {
        //Position
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}