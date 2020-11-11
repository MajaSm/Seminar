using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Animation Settings")]
    [Range(0.1f,5f)]
    [SerializeField] private float _animationSpeed = 1f;
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _startPanel;
    public static UIManager Instance;
    [SerializeField] private PlayerController _player; 
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _deadPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _startPanel.SetActive(true);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
    public void OnStartButtonPressed()
    {
        _startPanel.SetActive(false);
        _player.SetIsPlayerAbleToMove(true);
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
