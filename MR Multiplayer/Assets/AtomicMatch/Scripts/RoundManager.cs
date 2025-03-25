using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [Header("Game Settings")]
    public List<GameObject> atomObjectPacks;
    public Transform spawnPoint;
    public TextMeshPro winnerUI;
    public GameObject periodicTableHint;

    private int blueScore = 0;
    private int redScore = 0;
    private int roundCount = 0;
    private GameObject currentPack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(StartGameAfterHint());
    }

    private IEnumerator StartGameAfterHint()
    {
        yield return new WaitForSeconds(10f);
        periodicTableHint.SetActive(false);
        StartNextRound();
    }

    private void StartNextRound()
    {
        if (roundCount >= 9)
        {
            EndGame();
            return;
        }

        roundCount++;

        if (currentPack != null)
        {
            Destroy(currentPack);
        }

        int randomIndex = Random.Range(0, atomObjectPacks.Count);
        currentPack = Instantiate(atomObjectPacks[randomIndex], spawnPoint.position, Quaternion.identity);

        atomObjectPacks.RemoveAt(randomIndex);
    }

    public void RegisterPoint(string side)
    {
        if (side == "Blue") blueScore++;
        else if (side == "Red") redScore++;

        StartNextRound();
    }

    private void EndGame()
    {
        if (currentPack != null)
        {
            Destroy(currentPack);
        }

        string winner = (blueScore > redScore) ? "Blue Wins!" :
                       (redScore > blueScore) ? "Red Wins!" : "It's a Tie!";

        winnerUI.text = winner;
        winnerUI.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (currentPack != null)
        {
            Destroy(currentPack);
            Debug.Log("Destroyed remaining currentPack in OnDisable().");
        }
    }
}
