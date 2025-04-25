using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimationComponent : MonoBehaviour
{
    public NamedAnimancerComponent AnimancerComponent;
    public AnimationClip IdleAnimation;
    public AnimationClip LeftTurnAnimation;
    public AnimationClip RightTurnAnimation;
    public AnimationClip AccelerateAnimation;
    public float Speed { get; set; } = 1f;

    private void Awake()
    {
        AnimancerComponent.Animator.fireEvents = false;
        AnimancerComponent.States.CreateIfNew(IdleAnimation);
        AnimancerComponent.States.CreateIfNew(LeftTurnAnimation);
        AnimancerComponent.States.CreateIfNew(RightTurnAnimation);
        AnimancerComponent.States.CreateIfNew(AccelerateAnimation);
    }

    public void Play(AnimationClip clip)
    {
        if (clip == null) return;
        var state = AnimancerComponent.States.GetOrCreate(clip);
        state.Speed = Speed;
        AnimancerComponent.Play(state);
    }

    public void PlayFade(AnimationClip clip)
    {
        var state = AnimancerComponent.States.GetOrCreate(clip);
        state.Speed = Speed;
        AnimancerComponent.Play(state, 0.25f);
    }

    public void TryPlayFade(AnimationClip clip)
    {
        if (clip == null) return;
        var state = AnimancerComponent.States.GetOrCreate(clip);
        state.Speed = Speed;
        if (AnimancerComponent.IsPlaying(clip))
        {
            return;
        }
        AnimancerComponent.Play(state, 0.25f);
    }

}
