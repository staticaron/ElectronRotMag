using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    private const string SensitivityPrefName = "Sensitivity";
    private const string PostProcessingPrefName = "PostProcessing";
    private const string MainMenuSceneName = "MainMenu";
    private const int DefaultSensitivity = 150;

    public Slider sensitivitySlider;
    public Toggle postProcessingToggle;
    
    [SerializeField] TMP_Text sensitivitySliderHandleLabel;
    
    [SerializeField] SettingsDataSO settingsDataSO;
    [SerializeField] LevelLoadChannelSO levelLoadChannelSO;

    public void Start()
    {
        LoadSavedValues();

        //Shitty sliders and toggles
        sensitivitySlider.onValueChanged.AddListener(delegate { SetSensitivity(sensitivitySlider); });
    }

    public void LoadSavedValues()
    {
        int sensitivityValue = PlayerPrefs.GetInt(SensitivityPrefName);
        sensitivityValue = sensitivityValue == 0 ? DefaultSensitivity : sensitivityValue;

        bool postProcessingValue = PlayerPrefs.GetInt(PostProcessingPrefName) == 1 ? true : false;
        if(PlayerPrefs.GetInt(PostProcessingPrefName) == 0)
        {
            postProcessingValue = true;
            PlayerPrefs.SetInt(PostProcessingPrefName, 1);
        }

        sensitivitySlider.value = sensitivityValue;
        sensitivitySliderHandleLabel.text = sensitivityValue.ToString();
        postProcessingToggle.isOn = postProcessingValue;
    }

    public void SetValues()
    {
        PlayerPrefs.SetInt(SensitivityPrefName, (int)sensitivitySlider.value);
        PlayerPrefs.SetInt(PostProcessingPrefName, postProcessingToggle.isOn == true ? 1 : -1);

        Debug.Log($"Values set were : Sensitivity {PlayerPrefs.GetInt(SensitivityPrefName)} and Post Processing {PlayerPrefs.GetInt(PostProcessingPrefName)}");
    }

    public void SetSensitivity(Slider slider){
        sensitivitySliderHandleLabel.text = slider.value.ToString();
    }

    public void Home(){
        SetValues();
        levelLoadChannelSO.LoadLevel(MainMenuSceneName);
    }
}
