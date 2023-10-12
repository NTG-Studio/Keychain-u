using UnityEngine;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {
        public GameObject currentCamera;
   

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("camZone"))
            {
                CameraZone zone = other.gameObject.GetComponent<CameraZone>();
                if (currentCamera != null)
                {
                    currentCamera.SetActive(false);
                }

                currentCamera = zone.affectedCamera;
                currentCamera.SetActive(true);
            }
        }
    }
}
