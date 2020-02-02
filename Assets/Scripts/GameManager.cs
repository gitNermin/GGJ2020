using System.Collections;
using System.Collections.Generic;
using UIHealthAlchemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float _gameTime;
    [SerializeField]
    MaterialHealhBar healthbar;

    private float _currentTime;

    public static bool Stop;
    public static UnityEvent OnHint;
    Tile[] tiles;


    Image _timeImage;

    private void Start()
    {
        AI.OnTilesFinished.AddListener(OnAiFinishedTilesHandler);
        tiles = GameObject.FindObjectsOfType<Tile>();
        _currentTime = _gameTime;
        
    }

    private void Update()
    {
        _currentTime = Mathf.Max(_currentTime - Time.deltaTime,0);
        healthbar.Value = _currentTime / _gameTime;

    }

    private void OnAiFinishedTilesHandler()
    {
        //stopEverything
        Stop = true;
    }

    public void PlayHint()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].IsBatee5a)
            {
                tiles[i].Highlight();
            }
        }
        OnHint.Invoke();
    }
}
