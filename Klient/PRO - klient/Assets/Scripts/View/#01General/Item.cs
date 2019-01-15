using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Item : MonoBehaviour {

    #region VARIBELS
    public delegate void Clicked(Dictionary<string, object> data);

    public Transform _childs;
    public Text _name;

    protected Dictionary<string, object> _data = new Dictionary<string, object>();

    private Clicked _clicked;
    #endregion

    public virtual void AssignData(JSONNode json){}

    public void CategoryClicked()
    {
        if (_clicked != null)
            _clicked(_data);
    }

    public Clicked CategorySelected
    {
        set
        {
            _clicked = value;
        }
    }
}