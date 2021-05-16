using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tomfulghum.EventSystem;

public class Goal : MonoBehaviour
{
    [SerializeField] IntEvent levelFinishedEvent;
    [SerializeField] LevelData levelData;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float levelFinishDelay = 0.5f;

    AudioSource audioSource;
    bool locked = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var sowaCollider = Physics2D.OverlapCircle(transform.position, 0.1f, playerMask);
        if (!locked && sowaCollider) {
            var sowa = sowaCollider.GetComponent<CongaCat>();
            if (sowa.Type == CatType.Sowa) {
                locked = true;
                sowa.IsLeader = false;
                audioSource.Play();
                StartCoroutine(LevelFinishedCoroutine());
            }
        }
    }

    IEnumerator LevelFinishedCoroutine()
    {
        yield return new WaitForSeconds(levelFinishDelay);
        levelFinishedEvent.Raise(levelData.Id);
    }
}
