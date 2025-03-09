using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AngelMoveScript : MonoBehaviour
{
    public float angelSmoothTime; // How fast the angel will smooth to the robot
    private Vector3 angelVelocity = Vector3.zero;
    public float angelSpeed;
    Rigidbody2D rb;
    public GameObject robot;
    public Light2D robotTorch;
    private bool IsRobotInLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsRobotInLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRobotInLight)
            {
                rb.linearVelocity = Vector3.zero; // Freezes the angel
            }
        else if (!IsRobotInLight)
        {
            Vector3 targetpos = GameObject.FindGameObjectWithTag("Robot").transform.position; // Finds the Robots transform.position via its tag
            Vector3 direction = (targetpos - transform.position).normalized; // Gets the direction to move in as a Vector3
            rb.linearVelocity = direction * angelSpeed; // Adds velocity to the angel in the direction of the player at a rate of angelSpeed
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // When the angel collides with a trigger with the tag RobotTorch Light, will set IsRobotInLight to true
    {
        if (collision.CompareTag("RobotTorchLight"))
            {
            IsRobotInLight = true;
        }

    }
    void OnTriggerExit2D(Collider2D other) // When the angel leaves a collision, if the other collider was the RobotTorchLight, will set IsRobotInLight to false
    {
        if (other.CompareTag("RobotTorchLight"))
            {
            IsRobotInLight = false;
        }
    }
}
