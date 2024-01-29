using System.Collections;
using UnityEngine;
using Tretimi;
using System;
public class Bootloader : MonoBehaviour
{
    [SerializeField] private UIHolder _uIHolder;
    [SerializeField] private GamePlay _gamePlay;
    [SerializeField] private InApp _inApp;
    private Game _game;
    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        _game = new Game(_uIHolder, _gamePlay);
        _game.Init();
        _inApp.Init(_game);
        AudioSystem.Instance.LoadSettingsValues();
    }

    private void OnApplicationFocus(bool isFocus)
    {

        Debug.Log($"Application Pause {isFocus}");
        if (!isFocus)
            DataProvider.SaveDataJSON(_game.GetData());

    }
    private void OnApplicationQuit()
    {
        DataProvider.SaveDataJSON(_game.GetData());
    }
}
