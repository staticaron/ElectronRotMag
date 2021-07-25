using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private const string EndTransitionAnimationName = "EndTransition";

    [SerializeField] LevelLoadChannelSO levelLoadChannelSO;

    private string sceneToLoad = default;
    private Animator transitionAnim;

    private void Start(){
        transitionAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        levelLoadChannelSO.ELevelLoad += LoadLevel;
    }

    private void OnDisable()
    {
        levelLoadChannelSO.ELevelLoad -= LoadLevel;
    }

    private void LoadLevel(string levelToLoadName)
    {
        sceneToLoad = levelToLoadName;
        transitionAnim.Play(EndTransitionAnimationName);
    }

    public void TransitionComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}