using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActDisplayerUI : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] TMP_Text _title, _sub;
    static ActDisplayerUI inst;
    private void Awake()
    {
        inst = this;
    }
    public static void Show(string title, string sub)
    {
        inst._anim.Play("Show");
        inst._title.text = title;
        inst._sub.text = sub;
    }
}
