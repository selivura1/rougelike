using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnimationKey(string key)
    {
        animator.Play(key);
    }
}

