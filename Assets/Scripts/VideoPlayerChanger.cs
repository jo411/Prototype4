using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Changes the video player
/// </summary>
public class VideoPlayerChanger : MonoBehaviour
{
    public VideoPlayer TVVideoPlayer;

    public List<VideoClip> TVVideoClipList;

    private int videoPlayerIndex = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(TrashItem)))
        {
            VideoClip currentClip = null;

            if (TVVideoPlayer.isPlaying)
                currentClip = TVVideoPlayer.clip;

            if (TVVideoClipList == null || TVVideoClipList.Count <= 0)
                return;

            if (!TVVideoClipList.Contains(currentClip))
            {
                currentClip = TVVideoClipList[0];
                videoPlayerIndex = 0;
            }
            else
            {
                videoPlayerIndex = TVVideoClipList.IndexOf(currentClip);
            }



            if(videoPlayerIndex + 1 < TVVideoClipList.Count)
            {
                TVVideoPlayer.clip = TVVideoClipList[videoPlayerIndex + 1];
                videoPlayerIndex++;
            }
            else
            {
                TVVideoPlayer.clip = TVVideoClipList[0];
                videoPlayerIndex = 0;
            }

            Destroy(other.gameObject);
        }
    }



}
