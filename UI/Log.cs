using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Log : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    public static Log _inst;
    public void Awake()
    {
        if (_inst)
            Destroy(gameObject);
        else
        _inst = this;
    }
    public static void WriteInLog(string text)
    {
        _inst.WriteInLogLocal(text);
    }
    public void WriteInLogLocal(string text)
    {
        _text.text = text;
        Invoke(nameof(ResetText), text.Length / 1.5f);
    }
    public void ResetText()
    {
        _inst._text.text = "";
    }
}
