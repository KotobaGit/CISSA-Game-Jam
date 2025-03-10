using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SpaceScroller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private RawImage image;
    [SerializeField] private float x, y;
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
