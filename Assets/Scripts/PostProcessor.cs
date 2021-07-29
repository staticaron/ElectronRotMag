using UnityEngine;

public class PostProcessor : MonoBehaviour
{
    private const string PostProcessingPrefsName = "PostProcessing";

    private void Start()
    {
        LoadSavedData();
    }

    private void LoadSavedData()
    {
        bool postProcessing = PlayerPrefs.GetInt(PostProcessingPrefsName) == 1 ? true : false;

        gameObject.SetActive(postProcessing);
    }
}
