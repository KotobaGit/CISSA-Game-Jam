using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisionManager : MonoBehaviour
{
    private int buttonsPressed = 0;
    private int buttonsInRoom = 4; // number of buttons in the generator room
    public GameObject generatorBarrier1;
    public GameObject generatorBarrier2;
    private SpriteRenderer sr;
    public Transform healthBar;
    public TextMeshProUGUI hudTextComponent;
    [SerializeField] private float playerHealth = 99f; // Players health

    private bool canLeaveRoom = false;
    private bool foundAutopilotPassword = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (healthBar == null || hudTextComponent == null)
        {
            healthBar = GameObject.Find("UI/Health/Health Bar").transform;
            hudTextComponent = GameObject.Find("UI/Health/Health Num").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3((Mathf.Clamp((playerHealth/99f),0f,1f)), 1f, 1f);
        hudTextComponent.text = (Mathf.RoundToInt(playerHealth)).ToString();


        if (buttonsPressed == buttonsInRoom) // Destroys the 2 barriers in the generator room when all buttons are pressed
        {
            Destroy(generatorBarrier1);
            Destroy(generatorBarrier2);
        }
        if (playerHealth <1) // Reloads the current scene if playerhealth falls below 1
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

            case "AutopilotPassword": // When the player finds the autopilot password note, sets foundAutopilotPassword to true. Add dialogue here
                foundAutopilotPassword = true;
                GameObject.Find("Passkey Text").GetComponent<TextBoxObject>().SendText(); // Runs the passkey dialogue
                break;

            case "GenComputer":
                canLeaveRoom = true;  // Player can interact with the door to the rec room. Add checkmark to journal here
                GameObject.Find("GenComputer Text").GetComponent<TextBoxObject>().SendText();
                break;

            case "AutopilotComputer":
                if (!foundAutopilotPassword)
                {
                    GameObject.Find("Computer Text 1").GetComponent<TextBoxObject>().SendText(); // Runs computer text 1 if the player hasnt found the password
                }
                else if (foundAutopilotPassword == true)
                {
                    GameObject.Find("Computer Text 2").GetComponent<TextBoxObject>().SendText(); // Runs computer text 2 if player found password
                    canLeaveRoom = true; // Player can leave the room if they interact with the autopilot computer after they have found the password on the note. Add checkmark to journal here
                }
                break;

            case "ElectricWater":
                Debug.Log("HIT WATER");
                PlayerTakeDamage(20); // Robot takes 20 damage when it collides with Electric Water
                break;

            case "DoorToRecRoom":
                if (canLeaveRoom == true)
                {
                    SceneManager.LoadScene("Rec Room");
                }
                break;

            case "DoorToSpawnRoom":
                {
                    SceneManager.LoadScene("Spawn Room");
                }
                break;

            case "DoorToGeneratorRoom":
                {
                    SceneManager.LoadScene("Generator Room");
                }
                break;

            case "DoorToCockpitRoom":
                {
                    SceneManager.LoadScene("Cockpit Room");
                }
                break;

            case "DoorToAirlockRoom":
                {
                    SceneManager.LoadScene("Airlock Room");
                }
                break;

            case "SuctionPoint":
                {
                    PlayerTakeDamage(100);
                }
                break;

        }
    }

    public void PlayerTakeDamage(float damage) // Use this function to make the player take a certain amount of damage
    {
        playerHealth -= damage;
        Mathf.Clamp(playerHealth, 0f, 99f);
    }
}
