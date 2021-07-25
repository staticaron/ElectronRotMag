using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private const string MainMenuSceneName = "MainMenu";

    [SerializeField] Button playPauseButton;
    [SerializeField] Image playPauseIcon;
    [SerializeField] Button restartButton;

    [SerializeField, Space] TMP_Dropdown velocityDropDown;
    [SerializeField] TMP_Dropdown magneticFieldDropDown;
    [SerializeField] Slider angleSlider;
    [SerializeField] Slider chargeSlider;

    [SerializeField, Space] TMP_Text angleSliderValue;
    [SerializeField] TMP_Text chargeSliderValue;

    [SerializeField, Space] CanvasGroup settingsGroup;
    [SerializeField] float noInteractionAlpha = 0.5f;

    [SerializeField, Space] Sprite playSprite;
    [SerializeField] Sprite pauseSprite;

    [SerializeField, Space] PlayStateChannelSO playStateChannelSO;          //Controls the playState of the game
    [SerializeField] ElectronDataChannelSO electronDataChannelSO;           //Stores the electron data for easy editing
    [SerializeField] LevelLoadChannelSO levelLoadChannelSO;

    private void Start()
    {
        SetPlayPauseButtonUI(PlayPauseManager.currentPlayState);
        UpdateElectronData();
    }

    private void OnEnable()
    {
        playStateChannelSO.ESetState += ToggleSettingsInteraction;

        //For Shitty Sliders :(
        angleSlider.onValueChanged.AddListener(delegate { UpdateElectronData(); });
        chargeSlider.onValueChanged.AddListener(delegate { UpdateElectronData(); });
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

    public void UpdateElectronData()
    {
        //Get the values from the settings input
        float velocityValue;
        float magneticFieldValue;
        float.TryParse(velocityDropDown.options[velocityDropDown.value].text, out velocityValue);
        float.TryParse(magneticFieldDropDown.options[magneticFieldDropDown.value].text, out magneticFieldValue);

        //Set the slider values
        angleSliderValue.text = angleSlider.value.ToString();
        chargeSliderValue.text = chargeSlider.value.ToString();

        //Broadcast the values to the listeners
        electronDataChannelSO.UpdateValues(angleSlider.value, velocityValue, magneticFieldValue, chargeSlider.value);
    }

    public void Restart()
    {
        levelLoadChannelSO.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void MainMenu(){
        levelLoadChannelSO.LoadLevel(MainMenuSceneName);
    }
}
