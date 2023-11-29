using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;

[System.Serializable]
public struct VideoControllerMapping
{
    public Component controlButton;
    public VideoClip videoClip;
}

public class VideoController : MonoBehaviour
{
    public Material playTexture;
    public Material stopTexture;
    public List<VideoControllerMapping> videoMappings;
    VideoPlayer videoPlayer;
    string currentlyPlaying = "";

    private void Start()
    {
        videoPlayer= GetComponent<VideoPlayer>();
        // Set all audio buttons to have the play texture
        foreach (VideoControllerMapping key in videoMappings)
        {
            key.controlButton.GetComponent<MeshRenderer>().material = playTexture;
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
                string btnName = hit.transform.name;
                // Toggle the state of the touched button
                foreach (VideoControllerMapping key in videoMappings)
                {
                    if (key.controlButton.name.Equals(btnName))
                    {
                        // stop if already playing, otherwise switch to start of other track
                        if (btnName.Equals(currentlyPlaying))
                        {
                            videoPlayer.Stop();
                            key.controlButton.GetComponent<MeshRenderer>().material = playTexture;
                            currentlyPlaying = "";
                        }
                        else
                        {
                            videoPlayer.clip = key.videoClip;
                            videoPlayer.Play();
                            key.controlButton.GetComponent<MeshRenderer>().material = stopTexture;
                            currentlyPlaying = btnName;
                            break;
                        }
                    }
                }
            }
        }
    }
}