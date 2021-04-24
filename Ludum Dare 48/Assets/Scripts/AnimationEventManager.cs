using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    private Character character;
    private float lastTime;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    public void CallEvent(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight >= 0.5f && lastTime != Time.time)
        {
            lastTime = Time.time;
            character.AnimEvent();
        }
    }
}
