using UnityEngine;

public class DialogueManagerScript : MonoBehaviour
{
    public GameObject introTextObject; // GameObject holding the Intro Text dialogue
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (introTextObject == null) // Finds the Intro Text if its null at start
        {
            introTextObject = GameObject.Find("Intro Text");
            introTextObject.GetComponent<TextBoxObject>().SendText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
