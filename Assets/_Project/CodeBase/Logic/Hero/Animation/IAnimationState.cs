using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation
{
    public interface IAnimationState
    {
        EAnimationState StateName { get; }

        void Construct(Animator animator);
        void Enter();
        void Exit();
    }
}