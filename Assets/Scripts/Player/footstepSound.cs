using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class FootstepSound : MonoBehaviour
    {
        [FormerlySerializedAs("left_Foot")] [SerializeField] private AudioSource leftFoot;

        [FormerlySerializedAs("right_foot")] [SerializeField] private AudioSource rightFoot;
    
    
        public void LeftFoot()
        {
            leftFoot.Play();
        }

        public void RightFoot()
        {
            rightFoot.Play();
        }
    }
}
