using UnityEngine;

public class TextBoxObject : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBoxObj;
    public string[] TextLines;

    private string[] lines;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //incase you didn't set it yourself I will look for it here
        if (DialogueBoxObj == null) 
        {
            DialogueBoxObj = GameObject.Find("UI/Dialogue");
        }
    }

    // Update is called once per frame
    public void SendText()
    {
        DialogueBoxObj.GetComponent<Dialogue>().lines = TextLines;
        DialogueBoxObj.GetComponent<Dialogue>().StartDialogue();
    }
}
