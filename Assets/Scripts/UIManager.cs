using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button playPauseButton;
    [SerializeField] Image playPauseIcon;
    [SerializeField] Button restartButton;

    [SerializeField, Space] CanvasGroup settingsGroup;
    [SerializeField] float noInteractionAlpha = 0.5f;

    [SerializeField, Space] Sprite playSprite;
    [SerializeField] Sprite pauseSprite;

    [SerializeField, Space] PlayStateChannelSO playStateChannelSO;

    private void Start()
    {
        SetPlayPauseButtonUI(PlayPauseManager.currentPlayState);
    }

    private void OnEnable()
    {
        playStateChannelSO.ESetState += ToggleSettingsInteraction;
    }

    private void OnDisable()
    {
        playStateChannelSO.ESetState -= ToggleSettingsInteraction;
    }

    public void TogglePlayPause()
    {
        if (PlayPauseManager.currentPlayState == PlayState.Pause)
        {
            SetPlayPauseButtonUI(PlayState.Play);
            playStateChannelSO.RaiseSetStateEvent(PlayState.Play);
        }
        else
        {
            SetPlayPauseButtonUI(PlayState.Pause);
            playStateChannelSO.RaiseSetStateEvent(PlayState.Pause);
        }
    }

    private void SetPlayPauseButtonUI(PlayState state)
    {
        if (state == PlayState.Play)
        {
            playPauseIcon.sprite = pauseSprite;
        }
        else
        {
            playPauseIcon.sprite = playSprite;
        }
    }

    private void ToggleSettingsInteraction(PlayState playState)
    {
        if (playState == PlayState.Play)
        {
            settingsGroup.interactable = false;
            settingsGroup.alpha = noInteractionAlpha;
        }
        else
        {
            settingsGroup.interactable = true;
            settingsGroup.alpha = 1;
        }
    }
}
