using System.Linq;
using UnityEngine;

/*This class manages audio of moving of the character on the ground*/

public class PersonAudio : MonoBehaviour
{
    [Header("Step")]
    [SerializeField]
    private AudioSource stepAudio;
    [SerializeField]
    private AudioSource runningAudio;
    [Tooltip("Minimum velocity for moving audio to play")]
    /// <summary> "Minimum velocity for moving audio to play" </summary>
    [SerializeField]
    private float velocityThreshold = .01f;

    public Movement character;

    private Vector2 lastCharacterPosition;
    private Vector2 CurrentCharacterPosition => new Vector2(character.transform.position.x, character.transform.position.z);

    private AudioSource[] MovingAudios => new AudioSource[] { stepAudio, runningAudio };


    void Reset()
    {
        // Setup stuff.
        character = GetComponentInParent<Movement>();
        stepAudio = GetOrCreateAudioSource("Step Audio");
        runningAudio = GetOrCreateAudioSource("Running Audio");
    }


    void FixedUpdate()
    {
        // Play moving audio if the character is moving and on the ground.
        float velocity = Vector3.Distance(CurrentCharacterPosition, lastCharacterPosition);
        if (velocity >= velocityThreshold)
        {
            if (character.getIsRunning())
            {
                SetPlayingMovingAudio(runningAudio);
            }
            else
            {
                SetPlayingMovingAudio(stepAudio);
            }
        }
        else
        {
            SetPlayingMovingAudio(null);
        }

        // Remember lastCharacterPosition.
        lastCharacterPosition = CurrentCharacterPosition;
    }


    /// <summary>
    /// Pause all MovingAudios and enforce play on audioToPlay.
    /// </summary>
    /// <param name="audioToPlay">Audio that should be playing.</param>
    void SetPlayingMovingAudio(AudioSource audioToPlay)
    {
        // Pause all MovingAudios.
        foreach (var audio in MovingAudios.Where(audio => audio != audioToPlay && audio != null))
        {
            audio.Pause();
        }

        // Play audioToPlay if it was not playing.
        if (audioToPlay && !audioToPlay.isPlaying)
        {
            audioToPlay.Play();
        }
    }


    #region Utility.
    /// <summary>
    /// Get an existing AudioSource from a name or create one if it was not found.
    /// </summary>
    /// <param name="name">Name of the AudioSource to search for.</param>
    /// <returns>The created AudioSource.</returns>
    AudioSource GetOrCreateAudioSource(string name)
    {
        // Try to get the audiosource.
        AudioSource result = System.Array.Find(GetComponentsInChildren<AudioSource>(), a => a.name == name);
        if (result)
            return result;

        // Audiosource does not exist, create it.
        result = new GameObject(name).AddComponent<AudioSource>();
        result.spatialBlend = 1;
        result.playOnAwake = false;
        result.transform.SetParent(transform, false);
        return result;
    }


    static void PlayRandomClip(AudioSource audio, AudioClip[] clips)
    {
        if (!audio || clips.Length <= 0)
            return;

        // Get a random clip. If possible, make sure that it's not the same as the clip that is already on the audiosource.
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        if (clips.Length > 1)
            while (clip == audio.clip)
                clip = clips[Random.Range(0, clips.Length)];

        // Play the clip.
        audio.clip = clip;
        audio.Play();
    }
    #endregion 
}
