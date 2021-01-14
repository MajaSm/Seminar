using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Animation Settings")]
    [Range(0.1f,5f)]
    [SerializeField] private float _animationSpeed = 1f;
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private PlayerController _player;
    [SerializeField] private List<Button> _levelButtons;
    public static UIManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _deadPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _startPanel.SetActive(true);
        _levelPanel.SetActive(false);
    }
    public void OnButtonJump()
    {
        _player.Jump();
    }
    public void OnHorizontalInput(float value)
    {
       _player.SetHorizontalInput(value);
    }
    public void OnPlayerDeath()
    {
        _deadPanel.SetActive(true);
    }
    public void OnPlayerFinish()
    {
        StartCoroutine(PopUpPanel(_finishPanel.transform));
        _player.SetIsPlayerAbleToMove(false);
    }
    public void OnReplayButtonPressed()
    {
        GameManager.Instance.OnReplayLevel();
        _deadPanel.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
    public void OnStartButtonPressed()
    {

        _startPanel.SetActive(false);

        GameManager.Instance.SelectCurrentLevel();

    }
    public void OnLevelButtonPressed()
    {
        _levelPanel.SetActive(true);
        int lastCompletedLevel = -1;
        if (PlayerPrefs.HasKey("LastFinishedLevel"))
            lastCompletedLevel = PlayerPrefs.GetInt("LastFinishedLevel");

        _startPanel.SetActive(false);
        for(int i = 0;i<_levelButtons.Count;i++)
        {
            _levelButtons[i].interactable = false;

        }
        for (int i = 0; i < lastCompletedLevel+2; i++)
        {
            
            _levelButtons[i].interactable = true;

        }
        

    }
    public void OnLevelSelected(int lvlIndex)
    {
        GameManager.Instance.LevelSelected(lvlIndex);

        _levelPanel.SetActive(false);
    }
    public void OnButtonNext()
    {
        GameManager.Instance.OnNextLevel();
        _finishPanel.SetActive(false);
    }
    IEnumerator PopUpPanel(Transform panelTransform)
    {
        panelTransform.localScale = new Vector3(0,0,1);
        panelTransform.gameObject.SetActive(true);
        while (true)
        {
            if (panelTransform.localScale.x >= 1)
            {
                panelTransform.localScale = Vector3.one;
                break;
            }

            panelTransform.localScale = new Vector3(panelTransform.localScale.x + 0.01f, panelTransform.localScale.y + 0.01f, 1);
            yield return new WaitForSeconds(0.01f/_animationSpeed);
        }

        yield return null;
    }
}
