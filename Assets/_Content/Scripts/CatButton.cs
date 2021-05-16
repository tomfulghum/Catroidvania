using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatButton : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float pressedTime;

    Animator anim;
    bool pressed;
    bool active;

    public delegate void ButtonPressedEvent(int id, bool pressed);
    public static event ButtonPressedEvent OnButtonPressed;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var catCollider = Physics2D.OverlapCircle(transform.position, 0.1f, playerMask);
        CongaCat cat = null;
        if (catCollider)
            cat = catCollider.GetComponent<CongaCat>();
        bool isChonker = cat && cat.Type == CatType.Chonk;

        if (!isChonker && !active && pressed) {
            StartCoroutine(ButtonPressedCoroutine());
        } else if (isChonker && !pressed) {
            pressed = true;
            anim.SetBool("Pressed", true);
            OnButtonPressed?.Invoke(id, true);
        }
    }

    IEnumerator ButtonPressedCoroutine()
    {
        active = true;
        yield return new WaitForSeconds(pressedTime);
        active = false;
        pressed = false;
        anim.SetBool("Pressed", false);
        OnButtonPressed?.Invoke(id, false);
    }
}
