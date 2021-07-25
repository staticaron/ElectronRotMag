using UnityEngine;
using System.Collections.Generic;

public class CameraRandomRotator : MonoBehaviour
{
    private Quaternion toRotation;

    [SerializeField] float rotationSpeed;

    [SerializeField] float waitTime;
    [SerializeField] float angleThreshold;

    private float WaitTime
    {
        get { return WaitTime; }
        set
        {
            if (value == waitTime) return;

            UpdateWaiter(value);
            waitTime = value;
        }
    }

    private WaitForSeconds waiter;

    private void Start()
    {
        toRotation = Quaternion.Euler(45, 45, 45);
        waiter = new WaitForSeconds(waitTime);
        StartCoroutine(SetRandomAngle());
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
    }

    private void UpdateWaiter(float newTime)
    {
        waiter = new WaitForSeconds(newTime);
    }

    IEnumerator<YieldInstruction> SetRandomAngle()
    {
        while (true)
        {
            yield return waiter;

            float randomXangle = UnityEngine.Random.Range(-180, 180);
            float randomYangle = UnityEngine.Random.Range(-180, 180);
            float randomZangle = UnityEngine.Random.Range(-180, 180);

            //Prevent angle from falling too low.
            randomXangle = (randomXangle / Mathf.Abs(randomXangle)) * Mathf.Clamp(Mathf.Abs(randomXangle), angleThreshold, 180);
            randomYangle = (randomYangle / Mathf.Abs(randomYangle)) * Mathf.Clamp(Mathf.Abs(randomYangle), angleThreshold, 180);
            randomZangle = (randomZangle / Mathf.Abs(randomZangle)) * Mathf.Clamp(Mathf.Abs(randomZangle), angleThreshold, 180);

            toRotation = Quaternion.Euler(randomXangle, randomYangle, randomZangle);
        }
    }
}
