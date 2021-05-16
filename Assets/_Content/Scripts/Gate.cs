using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] int activationId = 0;
    [SerializeField] int activationCount = 1;
    [SerializeField] bool toggle;

    Animator anim;
    int activeCount = 0;
    bool open = false;

    void OnEnable()
    {
        CatButton.OnButtonPressed += OnButtonPressed;
    }

    void OnDisable()
    {
        CatButton.OnButtonPressed -= OnButtonPressed;
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (activeCount >= activationCount && !open) {
            open = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (activeCount < activationCount && open) {
            open = false;
            gameObject.layer = LayerMask.NameToLayer("BlockPlayer");
        }

        anim.SetBool("Open", open);
    }

    void OnButtonPressed(int id, bool pressed)
    {
        if (id != activationId)
            return;

        if (pressed)
            activeCount++;
        else
            activeCount--;
    }
}
