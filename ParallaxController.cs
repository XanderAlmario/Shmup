using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds;  //Array of background layers
        [SerializeField] float smoothing = 10f; //Smoothness of parallax effect
        [SerializeField] float multiplier = 15f; //Increments per layer of parallax effect

        Transform cam; //Reference to main cam
        Vector2 previousCamPos; //Position of cam in last frame

        void Awake() => cam = Camera.main.transform;

        void Start() => previousCamPos = cam.position;

        void Update()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (previousCamPos.y - cam.position.y) * multiplier;
                float bgTargetPosY = backgrounds[i].position.y + parallax;
                var bgFinalPos = new Vector2(backgrounds[i].position.x, bgTargetPosY);

                backgrounds[i].position = Vector2.Lerp(backgrounds[i].position, bgFinalPos, smoothing * Time.deltaTime);
            }

            previousCamPos = cam.position;
        }
    }
}
