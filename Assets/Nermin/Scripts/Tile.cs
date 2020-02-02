using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public enum State
    {
        Broken,
        Fixed,
        Breaking,
        Fixing, 
        Hint
    }

    [SerializeField]
    float _highlightTime;
    
    public static TileEvent OnTileFixed = new TileEvent();
    public static TileEvent OnTileBroke = new TileEvent();

    public State _myState = State.Fixed;

    private Renderer _myRenderer;


    public float BreakTime;
    public float FixTime;

    //gwa el pattern
    [SerializeField]
    bool _isBatee5a;
    public bool IsBatee5a
    {
        get
        {
            return _isBatee5a;
        }
    }

    private void Awake()
    {
        _myRenderer = GetComponent<Renderer>();
        if (_isBatee5a)
        {
            transform.tag = Tags.Batee5a;
            Highlight();
        }
    }

    private void OnMouseDown()
    {
        if(_myState ==  State.Broken)
        {
            Fix();
        }

        else if(_myState == State.Fixed)
        {
            Break();
        }
    }

    public void Fix()
    {
        Debug.Log("fix");
        StartCoroutine("FixCoroutine");
    }
    public IEnumerator FixCoroutine()
    {
        _myState = State.Fixing;
        GameObject LoadingBar = Instantiate(Resources.Load("LoadingBar")) as GameObject;
        LoadingBar.GetComponent<LoadingBar>().ShowLoadingBar(FixTime, transform.position);
        yield return new WaitForSeconds(FixTime);
        if (_isBatee5a) _myRenderer.material.color = Color.green;
        else
            _myRenderer.material.color = Color.white;
        _myState = State.Fixed;
        AudioManager.Instance.play("Fix");

        OnTileFixed.Invoke(this);
        
    }


    public void Break()
    {
        
        StartCoroutine(BreakCoroutine());
        Debug.Log("break");
    }


    IEnumerator BreakCoroutine()
    {
        _myState = State.Breaking;
        GameObject LoadingBar = Instantiate(Resources.Load("LoadingBar")) as GameObject;
        LoadingBar.GetComponent<LoadingBar>().ShowLoadingBar(FixTime, transform.position);
        yield return new WaitForSeconds(BreakTime);
        _myRenderer.material.color = Color.black;
        _myState = State.Broken;
        AudioManager.Instance.play("Break");
        OnTileBroke.Invoke(this);
    }
    State _preHighlightState;
    public void Highlight()
    {
        _preHighlightState = _myState;
        _myState = State.Hint;
        _myRenderer.material.color = Color.red;
        Invoke("Dehighlight", _highlightTime);
    }

    public void Dehighlight()
    {
        _myState = _preHighlightState;
        _myRenderer.material.color = Color.white;
    }
}

public class TileEvent:UnityEvent<Tile> { }
