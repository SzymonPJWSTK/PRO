using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class Page : ChangePage{

    public bool _isLoaded = false;
    public Text _pageName;
    public Transform _topBar;

    public virtual void LoadPage(JSONNode json, Dictionary<string,object> data){}

    public virtual void Return() { }
}
