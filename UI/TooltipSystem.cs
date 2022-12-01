using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] Tooltip _tooltip;
    public  void Show(string content, string header = "")
    {
        _tooltip.SetText(content, header);
        _tooltip.gameObject.SetActive(true);
    }
    public  void Hide()
    {
        if(_tooltip)
        _tooltip.gameObject.SetActive(false);
    }
}
