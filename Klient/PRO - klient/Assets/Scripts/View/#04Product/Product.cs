using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Product : Page
{

    public override void LoadPage(JSONNode json, Dictionary<string,object> data)
    {
        _pageName.text = (string)data["itemName"];
        _topBar.gameObject.SetActive(true);
        _isLoaded = true;
    }

    public override void Return()
    {
        OnPreviousPage();
    }

}
