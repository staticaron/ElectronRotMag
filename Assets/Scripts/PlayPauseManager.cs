using UnityEngine;

public enum PlayState
{
    Play,
    Pause
}

public class PlayPauseManager : MonoBehaviour
{
    public static PlayState currentPlayState;

    [SerializeField, Space] PlayStateChannelSO playStateChannelSO;

    private void Awake()
    {
        currentPlayState = PlayState.Pause;
    }

    private void OnEnable()
    {
        playStateChannelSO.ESetState += SetTheState;
    }

    private void OnDisable()
    {
        playStateChannelSO.ESetState -= SetTheState;
    }

    private void SetTheState(PlayState stateToSet)
    {
        currentPlayState = stateToSet;
    }
}