using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Order : MonoBehaviour {

    #region VARIBLES
    public Transform _itemsHolder;
    public OrderHolder _orderPrefab;

    private JSONNode _json;
    private bool _newData;
    #endregion

    private void Start()
    {
        App.Instance.OnDataRecived += OrderRecived;
    }

    private void OrderRecived(JSONNode json)
    {
        _json = json;
        _newData = true;
    }

    private void Update()
    {
        if(_newData)
        {
            _newData = false;
            StartCoroutine(ShowOrder(_json));
        }
    }

    private IEnumerator ShowOrder(JSONNode json)
    {
        OrderHolder instantiatedOrder = null;
        yield return new WaitUntil(() => InstantiateOrder(out instantiatedOrder));
        instantiatedOrder.AssignData(json);
    }

    private bool InstantiateOrder(out OrderHolder instantiatedOrder)
    {
        instantiatedOrder = Instantiate(_orderPrefab, _itemsHolder);
        return true;
    }
}
