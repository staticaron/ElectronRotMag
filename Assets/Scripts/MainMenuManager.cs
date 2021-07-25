using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private const string MainSceneName = "Main";
    private const string SettingsSceneName = "Settings";
    private const string MainMenuSceneName = "MainMenu";

    [SerializeField] LevelLoadChannelSO levelLoadChannelSO;

    public void Play()
    {
        levelLoadChannelSO.LoadLevel(MainSceneName);
    }

    public void MainMenu(){
        levelLoadChannelSO.LoadLevel(MainMenuSceneName);
    }

    public void Settings()
    {
        levelLoadChannelSO.LoadLevel(SettingsSceneName);
    }

    public void Quit()
    {
        levelLoadChannelSO.Quit();
    }
}
