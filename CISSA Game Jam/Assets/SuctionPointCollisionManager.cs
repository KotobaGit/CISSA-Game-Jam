using UnityEngine;

public class SuctionPointCollisionManager : MonoBehaviour
{
    [SerializeField] private ObjectiveTracker ObjectivesScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectivesScript = GameObject.Find("UI/ObjectiveTracker").GetComponent<ObjectiveTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate")) // Sets suction force to false and allows the player to leave the room
        {
            GameObject.Find("RobotPlaceholder").GetComponent<PlayerMovement>().suctionForce = false;
            GameObject.Find("RobotPlaceholder").GetComponent<PlayerCollisionManager>().canLeaveRoom = true; 
            GameObject.Find("Fixed Hole Text").GetComponent<TextBoxObject>().SendText();
            ObjectivesScript.TickObjective(2, true);
            
        }
    }
}
