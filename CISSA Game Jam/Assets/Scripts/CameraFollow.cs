using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 camVelocity = Vector3.zero;
    public float smoothTime;
    [SerializeField] private Transform robot;

    // Camera Clamp Ranges
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Update is called once per frame
    void Update()
    {

        float xClamp = Mathf.Clamp(robot.position.x, xMin, xMax); // The player's x value, but within the clamp range. IE gives the player's position, until its outside the clamp range
        float yClamp = Mathf.Clamp(robot.position.y, yMin, yMax); // The player's y value, but within the clamp range. IE gives the player's position, until its outside the clamp range

        Vector3 clampedpos = new Vector3(xClamp, yClamp, -1); // turns the clamp into a vector3
        Vector3 smoothpos = Vector3.SmoothDamp(transform.position, clampedpos, ref camVelocity, smoothTime * Time.deltaTime); // gradually smooths the target position, which is the clamped player position
        transform.position = smoothpos; // moves the camera to the target position
    }
}
