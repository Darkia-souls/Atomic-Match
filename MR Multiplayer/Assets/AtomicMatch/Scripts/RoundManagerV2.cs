using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManagerV2 : MonoBehaviour
{
    [Header("Game Settings")]
    public List<GameObject> atomObjectPackPrefabs; // Prefabs to instantiate
    public TextMeshPro winnerUI;
    public GameObject periodicTableHint;
    public GameObject periodicTableHint2; // Second periodic table hint
    public Button playAgainButton;
    public GameObject answersGameObject; // Reference to the answers GameObject

    private int blueScore = 0;
    private int redScore = 0;
    private int currentIndex = 0;
    private List<GameObject> instantiatedPacks = new List<GameObject>(); // Track instantiated objects

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        // Deactivate all packs and reset variables
        foreach (var pack in instantiatedPacks)
        {
            Destroy(pack);  // Destroy any existing instantiated packs
        }
        instantiatedPacks.Clear();  // Clear the list of instantiated packs

        blueScore = 0;
        redScore = 0;
        currentIndex = 0;

        periodicTableHint.SetActive(true);  // Show the hint at the start
        periodicTableHint2.SetActive(true); // Show the second hint at the start
        playAgainButton.gameObject.SetActive(false);  // Hide the Play Again button initially
        answersGameObject.SetActive(false);  // Hide the answers GameObject initially

        StartCoroutine(StartGameAfterHint());
    }

    private IEnumerator StartGameAfterHint()
    {
        yield return new WaitForSeconds(15f);
        periodicTableHint.SetActive(false);
        periodicTableHint2.SetActive(false); // Hide second hint
        ActivateNextPack();
    }

    private void ActivateNextPack()
    {
        // Deactivate previous pack if it exists
        if (currentIndex > 0 && currentIndex - 1 < instantiatedPacks.Count)
        {
            instantiatedPacks[currentIndex - 1].SetActive(false);
        }

        // Spawn the next pack in order using the prefab's own transform
        if (currentIndex < atomObjectPackPrefabs.Count)
        {
            GameObject newPack = Instantiate(atomObjectPackPrefabs[currentIndex],
                                 atomObjectPackPrefabs[currentIndex].transform.position,
                                 atomObjectPackPrefabs[currentIndex].transform.rotation);

            newPack.transform.SetParent(transform, true); // Keep world position
            instantiatedPacks.Add(newPack);
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

        ActivateNextPack(); // Move to the next round
    }

    private void EndGame()
    {
        // Display winner message
        string winner = (blueScore > redScore) ? "Blue Wins!" :
                       (redScore > blueScore) ? "Red Wins!" : "It's a Tie!";

        //winnerUI.text = winner;
        //winnerUI.gameObject.SetActive(true);

        // Show Play Again button
        playAgainButton.gameObject.SetActive(true);  // Activate the button

        // Show answers GameObject
        answersGameObject.SetActive(true); // Activate the answers at the end

        playAgainButton.onClick.RemoveAllListeners(); // Prevent multiple listeners
        playAgainButton.onClick.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        // Re-run the game without reloading the scene
        StartGame();
    }
}
