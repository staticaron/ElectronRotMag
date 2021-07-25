using UnityEngine;

[CreateAssetMenu(fileName = "SettingsDataSO", menuName = "ElectronRotMag/SettingsDataSO", order = 0)]
public class SettingsDataSO : ScriptableObject 
{
    public int cameraSensitivity;
    public bool postProcessing;    
}