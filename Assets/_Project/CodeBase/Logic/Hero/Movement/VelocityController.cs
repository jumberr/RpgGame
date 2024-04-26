using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class VelocityController
    {
        public Vector3 Velocity { get; private set; }
        
        
        public void ResetVelocity() => 
            Velocity = Vector3.zero;
        
        public void SetVelocity(Vector3 velocity) => 
            Velocity = velocity;
        
        public void SetVelocityY(float y) => 
            Velocity = new Vector3(Velocity.x, y, Velocity.z);
    }
}