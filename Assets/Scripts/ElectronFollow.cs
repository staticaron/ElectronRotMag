using UnityEngine;

public class ElectronFollow : MonoBehaviour
{
    [SerializeField] GameObject targetElectron;
    [SerializeField] Electron electronMovement;

    private void LateUpdate()
    {
        float r = GetRadius(electron: electronMovement);
        SetPosition(r);
    }

    //Get the radii
    private float GetRadius(Electron electron)
    {
        //r = mv/qBsin0
        float m = targetElectron.GetComponent<Rigidbody>().mass;
        float v = electronMovement.velocity;
        float B = electronMovement.magneticFieldStrength;
        float q = electronMovement.charge;
        float angle = electronMovement.angle;

        float r = (m * v) / (q * B * Mathf.Sin(angle * Mathf.Deg2Rad));

        return r;
    }

    //Follow the electron with that radii
    private void SetPosition(float radius)
    {
        Vector3 targetPos = targetElectron.transform.position;
        targetPos = new Vector3(targetPos.x, 0, -radius);

        transform.position = targetPos;
    }
}
