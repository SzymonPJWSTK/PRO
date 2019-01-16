using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class OrderHolder : MonoBehaviour {

    public Transform _orderItemsHolder;
    public OrderItem _orderItemPrefab;
    public Text _orderId;

    private string _id;

	public void AssignData(JSONNode json)
    {
        _id = json["___id"].Value;
        _orderId.text = _id;

        for(int i = 0; i < json["zamówienie"].Count;i++)
        {
            var instantiatedItem = Instantiate(_orderItemPrefab, _orderItemsHolder);
            instantiatedItem.AssignData(json["zamówienie"][i]);
        }
    }

}
