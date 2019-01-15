using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemHolder : Item
{
    public Text _price;

    public override void AssignData(JSONNode json)
    {
        _data.Add("id", json["___id"].Value);
        _data.Add("itemName", json["name"].Value);
        _name.text = json["name"].Value;
        _price.text = string.Format("{0} zł", json["price"].Value);
    }

    public void OnOrder()
    {
        App.Instance.AddProduct((string)_data["id"]);
    }

}