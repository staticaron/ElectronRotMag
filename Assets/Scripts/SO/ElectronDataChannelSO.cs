using UnityEngine;

[CreateAssetMenu(fileName = "ElectronDataChannelSO", menuName = "ElectronRotMag/ElectronDataChannelSO")]
public class ElectronDataChannelSO : ScriptableObject
{
    public float angle;
    public float speed;
    public float magneticFieldStrength;
    public float charge;

    public delegate void ValuesUpdated();
    public event ValuesUpdated EValuesUpdated;

    public void UpdateValues(float angle, float speed, float magneticFieldStrength, float charge)
    {
        this.angle = angle;
        this.speed = speed;
        this.charge = charge;
        this.magneticFieldStrength = magneticFieldStrength;

        if (EValuesUpdated != null) EValuesUpdated();
        else Debug.LogWarning("Values were updated were no one was looking for those updated values");
    }
}
