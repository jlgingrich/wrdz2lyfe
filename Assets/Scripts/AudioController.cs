using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct AudioMapping
{
    public Component audioButton;
    public AudioClip audioClip;
}

public class AudioController : MonoBehaviour
{
    public Material playTexture;
    public Material pauseTexture;
    public List<AudioMapping> buttonAudioMapping;
    AudioSource audioSource;
    int currentlyPlayingId = -1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Set all audio buttons to have the play texture
        foreach (AudioMapping key in buttonAudioMapping)
        {
            key.audioButton.GetComponent<MeshRenderer>().material = playTexture;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // Raycast from touch point on main camera to see if it touches a button
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int btnId = hit.transform.GetInstanceID();
                // Toggle the state of the touched button
                foreach (AudioMapping key in buttonAudioMapping)
                {
                    if (key.audioButton.GetInstanceID().Equals(btnId))
                    {
                        // Pause if already playing, otherwise switch to start of other track
                        if (btnId.Equals(currentlyPlayingId))
                        {
                            audioSource.Pause();
                            key.audioButton.GetComponent<MeshRenderer>().material = playTexture;
                            currentlyPlayingId = -1;
                        }
                        else
                        {
                            audioSource.clip = key.audioClip;
                            audioSource.Play();
                            key.audioButton.GetComponent<MeshRenderer>().material = pauseTexture;
                            currentlyPlayingId = btnId;
                        }
                    } else
                    {
                        key.audioButton.GetComponent<MeshRenderer>().material = playTexture;
                    }
                }
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                // Set all audio buttons to have the play texture
                foreach (AudioMapping key in buttonAudioMapping)
                {
                    key.audioButton.GetComponent<MeshRenderer>().material = playTexture;
                    currentlyPlayingId = -1;
                }
            }
        }
    }
}