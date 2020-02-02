using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator myAnimation;

    void Start()
    {
        myAnimation = GetComponent<Animator>();
    }
    public void Walk()
    {
        myAnimation.SetTrigger("Walk");
    }
    public void Idle()
    {
        myAnimation.SetTrigger("Idle");
    }
    public void Shout()
    {
        myAnimation.SetTrigger("Shout");
    }
    public void Break()
    {
        myAnimation.SetTrigger("Break");
    }
}
