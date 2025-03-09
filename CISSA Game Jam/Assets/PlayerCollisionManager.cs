using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    private int buttonsPressed = 0;
    public GameObject door;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Destroy(door); // Removes all door GameObjects. Can be altered to perform an animation (open the door) etc. Have different gameobjects for each door in the world?
                break;

        }
    }
}
