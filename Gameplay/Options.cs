using UnityEngine;

public class Options : MonoBehaviour
{
    public OptionsConfig CurrentConfig { get; private set; } = new OptionsConfig();
    [SerializeField] private string _optionsKey = "options";
    private void Start()
    {
        LoadOptions();
    }
    public void SetOptions(OptionsConfig config)
    {
        CurrentConfig = config;
    }
    public void LoadOptions()
    {
        if (!PlayerPrefs.HasKey(_optionsKey))
        {
            CurrentConfig.Resolution = Screen.currentResolution;
            SaveOptions();
        }
        else
            SetOptions(JsonUtility.FromJson<OptionsConfig>(PlayerPrefs.GetString(_optionsKey)));
        Screen.fullScreen = CurrentConfig.Fullscreen;
        Debug.Log("Options Loaded: | SFX: " + CurrentConfig.SFX + " | MUSIC: " + CurrentConfig.Music + " | FULLSCREEN: " + CurrentConfig.Fullscreen);
    }
    public void SaveOptions()
    {
        PlayerPrefs.SetString(_optionsKey, JsonUtility.ToJson(CurrentConfig));
        Debug.Log("Options saved");
    }
    public void SetSFX(float sfx)
    {
        CurrentConfig.SFX = sfx;
        SaveOptions();
    }
    public void SetMusic(float music)
    {
        CurrentConfig.Music = music;
        SaveOptions();
    }
    public void SetFullscreen(bool val)
    {
        CurrentConfig.Fullscreen = val;
        Screen.fullScreen = val;
        SaveOptions();
    }
    public void SetResolution(Resolution res)
    {
        CurrentConfig.Resolution = res;
        Screen.SetResolution(res.width, res.height, CurrentConfig.Fullscreen);
        SaveOptions();
    }
}
[System.Serializable]
public class OptionsConfig
{
    public float SFX = .4f;
    public float Music = .2f;
    public bool Fullscreen = true;
    public bool Vsync = false;
    public Resolution Resolution;
}

