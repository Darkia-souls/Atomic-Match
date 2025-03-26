using System;
using AtomicMatch.Scripts;
using UnityEngine;

public class RoundManagerV3 : MonoBehaviour
{
    public static RoundManagerV3 Instance { get; private set; }
    public event Action OnGameRestart; // Event for restarting game

    [SerializeField] private ElementSpawner elementSpawner;
    [SerializeField] private SpawnManager spawnManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartNextRound()
    {
        if (elementSpawner.HasElementsLeft())
        {
            spawnManager.StartNextRound();
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("🎉 Game Over! No more elements left.");
        // Add UI or restart logic here
    }

    public void RestartGame()
    {
        elementSpawner.ResetGame();
        OnGameRestart?.Invoke(); // Notify other scripts
        StartNextRound();
    }
}

