using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class Product : Page
{
    public Text _desc;
    public Text _price;
    private JSONNode _json;

    public override void LoadPage(JSONNode json, Dictionary<string,object> data)
    {
        _pageName.text = (string)data["itemName"];
        _topBar.gameObject.SetActive(true);
        _isLoaded = true;
        _desc.text = json[0]["desc"].Value;
        _json = json[0];
        _price.text = string.Format("{0} zł", _json["price"].Value);
    }

    public void Order()
    {
        App.Instance.AddProduct(_json["___id"]);
    }

    public override void Return()
    {
        OnPreviousPage();
    }

}
