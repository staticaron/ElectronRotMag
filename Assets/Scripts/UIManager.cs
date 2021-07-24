using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button playPauseButton;
    [SerializeField] Image playPauseIcon;
    [SerializeField] Button restartButton;
    [SerializeField] Slider angleSlider;
    [SerializeField] TMP_Dropdown velocityDropDown;
    [SerializeField] TMP_Dropdown magneticFieldDropDown;

    [SerializeField] Sprite playSprite, pauseSprite;

    [SerializeField] PlayStateChannelSO playStateChannelSO;

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
        if (playState == PlayState.Pause)
        {
            angleSlider.interactable = true;
            velocityDropDown.interactable = true;
            magneticFieldDropDown.interactable = true;
        }
        else
        {
            angleSlider.interactable = false;
            velocityDropDown.interactable = false;
            magneticFieldDropDown.interactable = false;
        }
    }
}
