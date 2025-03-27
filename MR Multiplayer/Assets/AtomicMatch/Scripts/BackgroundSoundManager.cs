using UnityEngine;
using System.Collections;

public class BackgroundSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudio;  // Assign in Inspector
    [SerializeField] private int totalRounds = 9;          // Set total rounds
    private int currentRound = 0;
    private bool isPlaying = false;

    public void StartPeriodicTableDestroySequence()
    {
        StartCoroutine(StartBackgroundSoundAfterDelay());
    }

    private IEnumerator StartBackgroundSoundAfterDelay()
    {
        yield return new WaitForSeconds(30f); // Wait for 30 real-time seconds

        if (!isPlaying)
        {
            backgroundAudio.Play();
            isPlaying = true;
        }
    }

    public void IncrementRound()
    {
        currentRound++;
        if (currentRound >= totalRounds)
        {
            backgroundAudio.Stop();
        }
    }
}
