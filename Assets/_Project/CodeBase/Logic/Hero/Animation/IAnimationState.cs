using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation
{
    public interface IAnimationState
    {
        void Construct(Animator animator);
        void Enter();
        void Exit();
    }
}