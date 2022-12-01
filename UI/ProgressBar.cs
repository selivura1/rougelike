using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] string barName = "Progress bar";
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _barText;
    public float Min = 0, Max = 10, CurrentValue = 0;
    [SerializeField] string _floatAmount = "F1";
    [SerializeField] bool _showName = false;
    [SerializeField] bool _showMax = false;
    [SerializeField] bool _useFullColor = false;
    [SerializeField] Color _color;
    [SerializeField] Color _fullColor;
    [SerializeField] TooltipTrigger _tooltipTrigger;
    void Update()
    {
        if (_slider)
        {
            _slider.minValue = Min;
            _slider.maxValue = Max;
            _slider.value = CurrentValue;
            if(_slider.targetGraphic)
            _slider.targetGraphic.color = _color;
        }
        if (_barText)
        {
            _barText.text = "";
            if (_showName)
                _barText.text += barName + ":";
            _barText.text += CurrentValue.ToString(_floatAmount);
            if(_showMax)
                _barText.text += "/" + Max.ToString(_floatAmount);
        }
        if(_useFullColor)
        {
            if(CurrentValue >= Max)
            {
                if (_slider.targetGraphic)
                    _slider.targetGraphic.color = _fullColor;
            }
        }
        if(_tooltipTrigger)
        {
            _tooltipTrigger.content = CurrentValue.ToString("F0") + "/" + Max.ToString("F0");
        }
    }

}
