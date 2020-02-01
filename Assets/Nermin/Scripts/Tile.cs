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

    public static UnityEvent<Tile> OnNonBatee5aBroken;

    public State _myState = State.Fixed;


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

    }
    public void Break()
    {
        if(!_isBatee5a)
        {
            OnNonBatee5aBroken.Invoke(this);
        }
    }
}

public class TileEvent<Tile> { }
