using UnityEngine;

public class MovementDragon : MonoBehaviour
{
    public float speed;
    public float turning;

    // Reference to the camera GameObject
    public GameObject cameraObject;

    void Update()
    {
        // Player movement logic
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, 0, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(0, 0, -speed * Time.deltaTime / 2);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -turning * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, turning * Time.deltaTime, 0);

        // Camera follow logic
        if (cameraObject != null)
        {
            // Set the camera position to follow the player
            cameraObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z - 5f);

            // Look at the player from the back
            cameraObject.transform.LookAt(transform.position);
        }
    }
}
