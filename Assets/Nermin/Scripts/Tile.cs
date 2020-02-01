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
        Fixing
    }

    [SerializeField]
    float _highlightTime;
    
    public static TileEvent OnNonBatee5aBroken = new TileEvent();
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

    private void Start()
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
        yield return new WaitForSeconds(FixTime);
        _myState = State.Fixed;
    }


    public void Break()
    {
        if(!_isBatee5a)
        {
            OnNonBatee5aBroken.Invoke(this);
        }
        Debug.Log("break");
    }


    IEnumerator BreakCoroutine()
    {
        _myState = State.Breaking;
        yield return new WaitForSeconds(BreakTime);
        _myState = State.Broken;
    }

    public void Highlight()
    {
        _myRenderer.material.color = Color.red;
        Invoke("Dehighlight", _highlightTime);
    }

    public void Dehighlight()
    {
        _myRenderer.material.color = Color.white;
    }
}

public class TileEvent:UnityEvent<Tile> { }
