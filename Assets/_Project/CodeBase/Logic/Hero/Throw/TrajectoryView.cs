using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class TrajectoryView : MonoBehaviour
    {
        [field: SerializeField] public LineRenderer Trajectory { get; private set; }
        [field: SerializeField] public int DefaultPointsCount { get; private set; }
        
        
        public void ToggleTrajectory(bool value) => 
            Trajectory.ToggleComponent(value);

        public void SetPositions(Vector3[] points)
        {
            Trajectory.positionCount = points.Length;
            Trajectory.SetPositions(points);
        }
    }
}