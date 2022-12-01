using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitHandler : MonoBehaviour
{
    float timer;
    [SerializeField] float timeReq = 3;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            timer += Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            timer = 0;
        }
        if(timer >= timeReq)
        {
            Application.Quit();
        }
    }
}
