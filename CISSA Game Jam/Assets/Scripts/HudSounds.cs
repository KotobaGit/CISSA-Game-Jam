using UnityEngine;

public class HudSounds : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField]private AudioSource audioSource;
    [SerializeField] private AudioClip[] ambientSounds;
    [SerializeField]private AudioSource audioSourceAmbient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        PlayTrack();
    }


    private void Update()
    {
    }

    // Update is called once per frame
    public void PlayClick()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void PlayTrack()
    {
        audioSourceAmbient.loop = false;
        audioSourceAmbient.clip = ambientSounds[Random.Range(0, ambientSounds.Length)];
        audioSourceAmbient.Play();
    }

}
