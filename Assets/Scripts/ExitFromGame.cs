using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitFromGame : MonoBehaviour
{
    [SerializeField] private Button _exitFromGameButton;

    private void Awake()
    {
        _exitFromGameButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        _exitFromGameButton.onClick.RemoveListener(QuitGame);
    }
}
