using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionManager : MonoBehaviour
{
    private int buttonsPressed = 0;
    private int buttonsInRoom = 4; // number of buttons in the room
    public GameObject barrier;
    private SpriteRenderer sr;
    private PlayerHealthManager playerHealthManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealthManager = GetComponent<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonsPressed == buttonsInRoom)
        {
            Destroy(barrier);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // use this for handling collisions with objects that have a Collider2D (No Trigger)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) // use this for handling collision with objects that have a Collider2D (Trigger)
    {
        switch (collision.gameObject.tag)
        {
            case "Button": // If the trigger the player collides with is tagged "Button", will call the HandleButtonCollision function
                sr = collision.GetComponent<SpriteRenderer>();
                sr.color = Color.green; // Sets the button's color to green
                buttonsPressed += 1;
                collision.tag = "Untagged"; // Removes the buttons tag, so it cant call this again
                break;

            case "ElectricWater":
                playerHealthManager.playerTakeDamage(20); // Robot takes 20 damage when it collides with Electric Water
                break;

            case "DoorToRecRoom":
                SceneManager.LoadScene("Rec Room");
                break;

            case "DoorToSpawnRoom":
                SceneManager.LoadScene("Spawn Room");
                break;

            case "DoorToGeneratorRoom":
                SceneManager.LoadScene("Generator Room");
                break;

            case "DoorToCockpitRoom":
                SceneManager.LoadScene("Cockpit Room");
                break;

            case "DoorToAirlockRoom":
                SceneManager.LoadScene("Airlock Room");
                break;

        }
    }
}
