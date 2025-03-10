using UnityEngine;

public class DialogueManagerScript : MonoBehaviour
{
    // Place GameObjects holding dialogue here
    public GameObject introTextObject; // GameObject holding the Intro Text dialogue

    // Place other GameObjects here
    public GameObject robot;
    public GameObject dialogueBox;

    void Start()
    {
        if (introTextObject == null) // Finds the Intro Text if its null at start
        {
            introTextObject = GameObject.Find("Intro Text");
            introTextObject.GetComponent<TextBoxObject>().SendText();
            
        }
        if (robot == null)
        {
            robot = GameObject.FindGameObjectWithTag("Robot"); // Finds the Robot tag if null
        }
        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
