using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class BasketProductHolder : MonoBehaviour
{
    #region VARIBLES
    public delegate void RemoveProduct(string id);
    public delegate void Recalculate();

    public Text _name;
    public Text _price;
    public Text _quantity;

    private JSONNode _json;
    private string _id;
    private RemoveProduct _remove;
    private Recalculate _recalculate;
    #endregion

    public void AssignData(JSONNode json)
    {
        _name.text = string.Format("{0}", json["name"].Value);
        _quantity.text = App.Instance.Basket[json["___id"].Value];
        _price.text = string.Format("{0} zł", json["price"].Value);
        _id = json["___id"].Value;
        _json = json;
    }

    public void AddOn()
    {
        App.Instance.AddProduct(_id);
        _quantity.text = App.Instance.Basket[_json["___id"].Value];
        if (_recalculate != null)
            _recalculate();
    }

    public void RemoveOne()
    {
        App.Instance.RemoveOneProduct(_id);
        if (App.Instance.Basket[_id] == "0")
            OnRemove();
        else
        {
            if (_recalculate != null)
                _recalculate();
            _quantity.text = App.Instance.Basket[_json["___id"].Value];
        }
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

    public Recalculate Rec
    {
        set { _recalculate = value; }
    }
}