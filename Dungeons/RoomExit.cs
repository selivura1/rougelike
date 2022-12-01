using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    bool _active = false;
    [SerializeField] Animator _anim;
    [SerializeField] Collider2D _collider;
    [SerializeField] string _opened, _closed;
    [SerializeField] bool _stageEnd;
    [SerializeField] bool _loop;
    [SerializeField] AudioClip _openSFX, _closeSFX;
    DungeonGenerator _dungeonGenerator;
    [SerializeField] private float transitionDelay = 1;
    HUDActivator HUDActivator;
    private FadeUI fade;
    public bool mute;
    private void GetReferences()
    {
        HUDActivator = ReferenceContainer.HUDActivator;
        _dungeonGenerator = ReferenceContainer.DungeonGenerator;
        fade = FindObjectOfType<FadeUI>();
    }
    public void Activate(bool playSound = true)
    {
        GetReferences();
        if (playSound && !mute)
            ReferenceContainer.SoundSpawner.PlayMusic(_openSFX, 1, 1);
        _anim.Play(_opened);
        _active = true;
        _collider.isTrigger =true;
    }
    public void Deactivate(bool playSound = true)
    {
        GetReferences();
        if (playSound && !mute)
            ReferenceContainer.SoundSpawner.PlayMusic(_closeSFX, 1, 1);
        _active = false;
        _collider.isTrigger = false;
        _anim.Play(_closed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_active) return;
        var player = collision.GetComponent<PlayerControl>();
        if (player)
        {
            if (_stageEnd)
            {
                player.enabled = false;
                HUDActivator.ShowStageClearUI();
                Deactivate(false);
            }
            if(_loop)
            {
                _dungeonGenerator.Loop();
                Deactivate(false);
            }
            else
            {
                fade.Fade();
                Invoke(nameof(ToNextRoom), transitionDelay);     
                Deactivate(true);
            }
        }
    }
    private void ToNextRoom()
    {
        _dungeonGenerator.ProceedToNextRoom();
    }
}
