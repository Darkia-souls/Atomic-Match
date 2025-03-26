using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManagerV2 : MonoBehaviour
{
    [Header("Game Settings")]
    public List<GameObject> atomObjectPacks;
    public TextMeshPro winnerUI;
    public GameObject periodicTableHint;

    private int blueScore = 0;
    private int redScore = 0;
    private int currentIndex = 0; // Track which pack to activate next

    private void Start()
    {
        // Deactivate all packs initially
        foreach (var pack in atomObjectPacks)
        {
            pack.SetActive(false);
        }

        periodicTableHint.SetActive(true); // Show the hint at the start
        StartCoroutine(StartGameAfterHint());
    }

    private IEnumerator StartGameAfterHint()
    {
        yield return new WaitForSeconds(10f);
        periodicTableHint.SetActive(false);
        ActivateNextPack();
    }

    private void ActivateNextPack()
    {
        if (currentIndex > 0)
        {
            atomObjectPacks[currentIndex - 1].SetActive(false); // Deactivate previous pack
        }

        if (currentIndex < atomObjectPacks.Count)
        {
            atomObjectPacks[currentIndex].SetActive(true);
            currentIndex++;
        }
        else
        {
            EndGame();
        }
    }

    public void RegisterPoint(string side)
    {
        if (side == "Blue") blueScore++;
        else if (side == "Red") redScore++;

        ActivateNextPack(); // Activate the next pack when a point is scored
    }

    private void EndGame()
    {
        string winner = (blueScore > redScore) ? "Blue Wins!" :
                       (redScore > blueScore) ? "Red Wins!" : "It's a Tie!";

        winnerUI.text = winner;
        winnerUI.gameObject.SetActive(true);
    }
}
