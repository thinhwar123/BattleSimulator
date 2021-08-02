using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerAnimator : MonoBehaviour
{
    [SerializeField] private Character character;
    public void AnimationTrigger()
    {
        character.AnimationTrigger();
    }
    public void AnimationFinish()
    {
        character.AnimationFinish();
    }
}
