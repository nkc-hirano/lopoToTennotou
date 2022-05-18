using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerSus : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private Subject<Unit> playerSusSubject = new Subject<Unit>();
    public Subject<Unit> PlayerSusSubject => playerSusSubject;
    private void Start()
    {
        playerSusSubject.Subscribe(var =>
        {
            Time.timeScale = 0;
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            Debug.Log("AAAAAAAAAAAAA‚ ‚ ‚ ‚ ");
        });
    }
}
