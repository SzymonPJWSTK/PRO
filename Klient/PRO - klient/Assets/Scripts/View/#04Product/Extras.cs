using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Extras : MonoBehaviour {

    #region VARIBLES
    public delegate void clicked(JSONNode josn);
    public clicked _clicked;
    public Text _name;

    private JSONNode _json;
    #endregion

    public void AssignData(JSONNode json)
    {
        _json = json;
        _name.text = json["name"].Value;
    }

    public void Clicked()
    {
        if (_clicked != null)
            _clicked(_json);
    }
}
