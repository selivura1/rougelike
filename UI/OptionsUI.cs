using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] Slider _sfx, _music;
    [SerializeField] Toggle _fullscreen;
    [SerializeField] TMP_Dropdown _resolutionDropDown;
    Options options;
    Resolution[] _resolutions;
    private void Start()
    {
        options = FindObjectOfType<Options>();
        _resolutions = Screen.resolutions;
        _resolutionDropDown.ClearOptions();
        List<string> listOptions = new List<string>();
        int index = 0;
       
        for (int i = 0; i < _resolutions.Length; i++)
        {
            listOptions.Add(_resolutions[i].width + "x" + _resolutions[i].height + "@" + _resolutions[i].refreshRate);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height && _resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                index = i;
            }
        }
        _resolutionDropDown.AddOptions(listOptions);
        _resolutionDropDown.value = index;
        _resolutionDropDown.RefreshShownValue();
        LoadValues();
    }
    public void LoadValues()
    {
        _sfx.value = options.CurrentConfig.SFX;
        _music.value = options.CurrentConfig.Music;
        _fullscreen.isOn = options.CurrentConfig.Fullscreen;
    }
    public void SetSFX(float sfx)
    {
        options.SetSFX(sfx);
    }
    public void SetMusic(float music)
    {
        options.SetMusic(music);
    }
    public void SetFullscreen(bool val)
    {
        options.SetFullscreen(val);
    }
    public void UpdateResolution(int val)
    {
        options.SetResolution(_resolutions[val]);
    }
}
