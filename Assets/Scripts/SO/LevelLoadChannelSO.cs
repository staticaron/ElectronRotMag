using UnityEngine;

[CreateAssetMenu(fileName = "LevelLoadChannelSO", menuName = "ElectronRotMag/LevelLoadChannelSO", order = 0)]
public class LevelLoadChannelSO : ScriptableObject
{
    public delegate void LevelLoad(string levelToLoadName);
    public event LevelLoad ELevelLoad;

    public delegate void QuitGame();
    public event QuitGame EQuitGame;

    public void LoadLevel(string levelName)
    {
        if (ELevelLoad != null) ELevelLoad(levelName);
        else
        {
            Debug.LogWarning("Load Level was raised but no one was listening to it");
        }
    }

    public void Quit()
    {
        if (EQuitGame != null) EQuitGame();
        else
        {
            Debug.LogWarning("EQuitGame was raised but no one was listening to it");
        }
    }
}