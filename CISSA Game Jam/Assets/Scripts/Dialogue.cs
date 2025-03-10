using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObj;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update

    private void Awake()
    {
        if (PlayerObj == null)
        {
            PlayerObj = GameObject.Find("RobotPlaceholder");
        }
    }
    void Start()
    {
         StartDialogue();
         
    }

    // Update is called once per frame
    void SkipText()
    {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
    }

    public void FreezePlayer(bool StopPlayer)
    {
        PlayerObj.GetComponent<PlayerMovement>().NoInput = StopPlayer;
    }

    public void StartDialogue()
    {
        transform.localScale = new Vector3(0.61f, 0.67f, 0.67f);
        StopAllCoroutines();
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
        //if FREEZE TEXT IS TRUE
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
            FreezePlayer(false);
            //unfreeze player
        }
    }
}
