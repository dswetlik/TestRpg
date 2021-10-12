using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipContainer : MonoBehaviour
{

    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();

    public List<AudioClip> GetAudioClips() { return audioClips; }

}
