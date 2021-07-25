using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private const string EndTransitionAnimationName = "EndTransition";

    [SerializeField] LevelLoadChannelSO levelLoadChannelSO;

    private string sceneToLoad = default;
    private Animator transitionAnim;

    private void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        levelLoadChannelSO.ELevelLoad += LoadLevel;
        levelLoadChannelSO.EQuitGame += Quit;
    }

    private void OnDisable()
    {
        levelLoadChannelSO.ELevelLoad -= LoadLevel;
        levelLoadChannelSO.EQuitGame -= Quit;
    }

    private void LoadLevel(string levelToLoadName)
    {
        sceneToLoad = levelToLoadName;
        transitionAnim.Play(EndTransitionAnimationName);
    }

    private void Quit()
    {
        transitionAnim.Play(EndTransitionAnimationName);
        sceneToLoad = "";
    }

    public void TransitionComplete()
    {
        if(string.IsNullOrEmpty(sceneToLoad))Application.Quit();
        else SceneManager.LoadScene(sceneToLoad);
    }
}