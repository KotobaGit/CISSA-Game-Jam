using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisionManager : MonoBehaviour
{
    private int buttonsPressed = 0;
    private int buttonsInRoom = 5; // number of buttons in the generator room
    public GameObject generatorBarrier1;
    public GameObject generatorBarrier2;
    private SpriteRenderer sr;
    public Transform healthBar;
    public TextMeshProUGUI hudTextComponent;
    [SerializeField] private float playerHealth = 99f; // Players health
    [SerializeField] private ObjectiveTracker ObjectivesScript;

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip failSound;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip damageSound;
    private AudioSource audioSource;


    public bool canLeaveRoom = false;
    private bool foundAutopilotPassword = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
            audioSource = GetComponent<AudioSource>();

            healthBar = GameObject.Find("UI/Health/Health Bar").transform;
            hudTextComponent = GameObject.Find("UI/Health/Health Num").GetComponent<TextMeshProUGUI>();
            ObjectivesScript = GameObject.Find("UI/ObjectiveTracker").GetComponent<ObjectiveTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3((Mathf.Clamp((playerHealth/99f),0f,1f)), 1f, 1f);
        hudTextComponent.text = (Mathf.RoundToInt(playerHealth)).ToString();

        if (buttonsPressed >= buttonsInRoom - 1) // Destroys the 2 barriers in the generator room when all buttons are pressed
        {
            Destroy(generatorBarrier2);
            if (buttonsPressed >= buttonsInRoom)
            {
                Destroy(generatorBarrier1);
            }
        }

        if (playerHealth <1) // Reloads the current scene if playerhealth falls below 1
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // use this for handling collision with objects that have a Collider2D (Trigger)
    {
        audioSource.loop = false;
        switch (collision.gameObject.tag)
        {
            
            case "Button": // If the trigger the player collides with is tagged "Button", will call the HandleButtonCollision function
                sr = collision.GetComponent<SpriteRenderer>();
                sr.color = Color.green; // Sets the button's color to green
                buttonsPressed += 1;
                collision.GetComponent<Light2D>().enabled = false;
                collision.tag = "Untagged"; // Removes the buttons tag, so it cant call this again

                audioSource.clip = doorSound;
                audioSource.Play();

                break;

            case "AutopilotPassword": // When the player finds the autopilot password note, sets foundAutopilotPassword to true. Add dialogue here
                foundAutopilotPassword = true;
                GameObject.Find("Passkey Text").GetComponent<TextBoxObject>().SendText(); // Runs the passkey dialogue

                audioSource.clip = paperSound;
                audioSource.Play();

                break;

            case "GenComputer":
                canLeaveRoom = true;  // Player can interact with the door to the rec room. Add checkmark to journal here
                //OBJECTIVE 2
                ObjectivesScript.TickObjective(1, true);
                GameObject.Find("GenComputer Text").GetComponent<TextBoxObject>().SendText();

                audioSource.clip = winSound;
                audioSource.Play();

                break;

            case "AutopilotComputer":
                if (!foundAutopilotPassword)
                {

                    audioSource.clip = failSound;
                    audioSource.Play();

                    GameObject.Find("Computer Text 1").GetComponent<TextBoxObject>().SendText(); // Runs computer text 1 if the player hasnt found the password
                }
                else
                {

                    audioSource.clip = winSound;
                    audioSource.Play();

                    GameObject.Find("Computer Text 2").GetComponent<TextBoxObject>().SendText(); // Runs computer text 2 if player found password
                    canLeaveRoom = true; // Player can leave the room if they interact with the autopilot computer after they have found the password on the note. Add checkmark to journal here
                    //OBJECTIVE 3
                    ObjectivesScript.TickObjective(2, true);
                }
                break;

            case "ElectricWater":
                Debug.Log("HIT WATER");
                PlayerTakeDamage(20); // Robot takes 20 damage when it collides with Electric Water
                break;

            case "DoorToRecRoom":
                if (canLeaveRoom == true)
                {

                    audioSource.clip = doorSound;
                    audioSource.Play();

                    SceneManager.LoadScene("Rec Room");
                }
                break;

            case "DoorToSpawnRoom":
                {

                    audioSource.clip = doorSound;
                    audioSource.Play();

                    SceneManager.LoadScene("Spawn Room");
                }
                break;

            case "DoorToGeneratorRoom":
                {

                    audioSource.clip = doorSound;
                    audioSource.Play();

                    SceneManager.LoadScene("Generator Room");
                }
                break;

            case "DoorToCockpitRoom":
                {
                    if (canLeaveRoom == true)
                    {

                        audioSource.clip = doorSound;
                        audioSource.Play();

                        SceneManager.LoadScene("Cockpit Room");
                    }
                }
                break;

            case "DoorToAirlockRoom":
                {
                    if (canLeaveRoom == true)
                    {

                        audioSource.clip = doorSound;
                        audioSource.Play();

                        SceneManager.LoadScene("Air Lock Room");
                    }
                }
                break;

            case "SpawnDoor":
                {

                    audioSource.clip = doorSound;
                    audioSource.Play();

                    SceneManager.LoadScene("Rec Room");
                }
                break;

            case "SuctionPoint":
                {
                    PlayerTakeDamage(100);
                }
                break;
            
            case "DoorToGameWonRoom":
            {
                if (canLeaveRoom == true)
                {
                        audioSource.clip = doorSound;
                        audioSource.Play();

                        SceneManager.LoadScene("Game Won Scene");
                }
                break;
            }

        }
    }

    public void PlayerTakeDamage(float damage) // Use this function to make the player take a certain amount of damage
    {

        audioSource.clip = damageSound;
        audioSource.Play();

        playerHealth -= damage;
        Mathf.Clamp(playerHealth, 0f, 99f);
    }
}
