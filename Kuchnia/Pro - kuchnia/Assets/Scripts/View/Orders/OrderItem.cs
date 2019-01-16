using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class OrderItem : MonoBehaviour {

    public Text _orderDesc;

	public void AssignData(JSONNode json)
    {
        _orderDesc.text = string.Format("{0} x {1}", json["name"].Value, json["quantity"].Value);
    }
}
