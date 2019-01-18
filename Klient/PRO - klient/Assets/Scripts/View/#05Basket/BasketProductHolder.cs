using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class BasketProductHolder : MonoBehaviour
{
    #region VARIBLES
    public delegate void RemoveProduct(string id);

    public Text _name;
    public Text _price;

    private string _id;
    private RemoveProduct _remove;
    #endregion

    public void AssignData(JSONNode json)
    {
        _name.text = string.Format("{0} x {1}", json["name"].Value, App.Instance.Basket[json["___id"].Value]);
        _price.text = string.Format("{0} zł", json["price"].Value);
        _id = json["___id"].Value;
    }

    public void OnRemove()
    {
        if (_remove != null)
            _remove(ID);
    }

    public string ID
    {
        get
        {
            return _id;
        }
    }

    public RemoveProduct Remove
    {
        set
        {
            _remove = value;
        }
    }
}