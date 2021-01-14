using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private PlayerController _playerController;
    private int _currentLevel;

    void Start()
    {
        Instance = this;
        _currentLevel = PlayerPrefs.GetInt("LastFinishedLevel");
    }

    public void OnPlayerDeath()
    {
        UIManager.Instance.OnPlayerDeath();
    }
    public void OnPlayerFinish()
    {
        PlayerPrefs.SetInt("LastFinishedLevel", _currentLevel);
        PlayerPrefs.Save();

        UIManager.Instance.OnPlayerFinish();

        _levelSpawner.RemoveLevel();
    }

    public void LevelSelected(int lvlIndex)
    {
        _currentLevel = lvlIndex;

        _levelSpawner.LevelSelected(_currentLevel);
    }

    public void SelectCurrentLevel()
    {

        if (PlayerPrefs.HasKey("LastFinishedLevel"))
            _currentLevel = PlayerPrefs.GetInt("LastFinishedLevel") + 1;
        else
            _currentLevel = 0;

        _levelSpawner.LevelSelected(_currentLevel);
    }

    public void OnReplayLevel()
    {
        //vracanje playera
        _playerController.OnResetPlayer();
        //brisanje trenutnog level
        _levelSpawner.RemoveLevel();
        //ponovno instanciranje
        _levelSpawner.LevelSelected(_currentLevel);

        
    }
    public void OnNextLevel()
    {
        _playerController.OnResetPlayer();
        _levelSpawner.LevelSelected(_currentLevel + 1);
    }
}
