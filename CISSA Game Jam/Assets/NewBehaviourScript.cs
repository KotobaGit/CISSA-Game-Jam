using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject objectStoringText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (objectStoringText == null)
        {
            objectStoringText = GameObject.Find("GameObject");
            Debug.Log("Found obj");
            objectStoringText.GetComponent<TextBoxObject>().SendText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colllision");
        objectStoringText.GetComponent<TextBoxObject>().SendText();
    }
}
