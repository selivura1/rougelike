using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    static TimeControl inst;
    private void Awake()
    {
        if (inst) Destroy(gameObject);
        else inst = this;
    }
    public static void FreezeTime(float time)
    {
        inst.StartCoroutine(inst.TimeFreeze(time));
    }
    private IEnumerator TimeFreeze(float pauseTime, float timeScale = 0.1f)
    {
        Time.timeScale = timeScale;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    public static void SetPause(bool value)
    {
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
