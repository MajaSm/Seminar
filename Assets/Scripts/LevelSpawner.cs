using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;
    [SerializeField] private PlayerController _player;

    private GameObject _spawnedLevel;

    public void LevelSelected(int lvlIndex)
    {
        _spawnedLevel = Instantiate(_levels[lvlIndex]);

        _player.SetIsPlayerAbleToMove(true);
    }

    public void RemoveLevel()
    {
        Destroy(_spawnedLevel);
    }
}
