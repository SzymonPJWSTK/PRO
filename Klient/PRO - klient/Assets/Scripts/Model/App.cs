using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using SimpleJSON;

public enum MODE { TEST, PRODUCTION};
public delegate IEnumerator DataRequest(string[] args, Action<JSONNode> success, Action<long> failure);

public class App : MonoBehaviour {

    #region VARIBLES
    private static App _instance;
    private static object _lock = new object();

    private const string TESTAPI = "http://localhost:8080/";
    private const string PRODUCTIONAPI = "";

    public MODE _mode;
    public int _timeout = 5;
    #endregion

    #region WEBREQUESTS
    public IEnumerator GetCategories(string[] args, Action<JSONNode> success, Action<long> failure)
    {
        using (var www = UnityWebRequest.Get(API + "categories"))
        {
            yield return www.SendWebRequest();

            switch(www.responseCode)
            {
                case 200: { success(JSONNode.Parse(www.downloadHandler.text)); break; }
                default: failure(www.responseCode); break;
            }
        }
    }

    public IEnumerator GetMenu(string[] args, Action<JSONNode> success, Action<long> failure)
    {
        using (var www = UnityWebRequest.Get(API + "menu" + ParametersFromArgs(args)))
        {
            www.timeout = _timeout;
            yield return www.SendWebRequest();

            switch (www.responseCode)
            {
                case 200: { success(JSONNode.Parse(www.downloadHandler.text)); break; }
                default: failure(www.responseCode); break;
            }
        }
    }

    public IEnumerator GetMenuProduct(string[] args, Action<JSONNode> success, Action<long> failure)
    {
        using (var www = UnityWebRequest.Get(API + "menu/products" + ParametersFromArgs(args)))
        {
            www.timeout = _timeout;
            yield return www.SendWebRequest();

            switch (www.responseCode)
            {
                case 200: { success(JSON.Parse(www.downloadHandler.text)); break; }
                default: failure(www.responseCode); break;
            }
        }
    }

    public IEnumerator PostBasketProducts(string[] args, Action<JSONNode> success, Action<long> failure)
    {
        using (var www = UnityWebRequest.Post(API + "basket/products", _basket))
        {
            yield return www.SendWebRequest();

            switch (www.responseCode)
            {
                case 200: { success(JSONNode.Parse(www.downloadHandler.text)); break; }
                default: failure(www.responseCode); break;
            }
        }
    }

    public IEnumerator PostBasketRecalculate(Action<JSONNode> success, Action<JSONNode> failure)
    {
        using (var www = UnityWebRequest.Post(API + "basket/recalculate", _basket))
        {
            yield return www.SendWebRequest();

            switch(www.responseCode)
            {
                case 200: { success(JSONNode.Parse(www.downloadHandler.text)); break; }
                default: failure(www.responseCode); break;
            }
        }
    }

    public IEnumerator PostBasketPlace(Action<JSONNode> success, Action<long> failure)
    {
        using (var www = UnityWebRequest.Post(API + "basket/place", _basket))
        {
            yield return www.SendWebRequest();

            switch (www.responseCode)
            {
                case 200: { success(JSONNode.Parse(www.downloadHandler.text)); break;}
                default: failure(www.responseCode); break;
            }
        }
    }

    public IEnumerator PostMeals()
    {
        var json = new Dictionary<string, string>();
        json.Add("name", "kopytka");

        using (var www = UnityWebRequest.Post(API + "meals", json))
        {
            www.timeout = _timeout;
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
        }
    }

    private string ParametersFromArgs(string[] args)
    {
        var result = "";
        if (args != null)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                    result += "?";
                else
                    result += "&";

                result += args[i];
            }
        }

        return result;
    }
    #endregion

    #region BASKET

    private Dictionary<string, string> _basket = new Dictionary<string, string>();

    public void AddProduct(string id)
    {
        if (_basket.ContainsKey(id))
        {
            var result = -1;
            var success = int.TryParse(_basket[id], out result);
            if(success && result != -1)
            {
                result++;
                _basket[id] = result.ToString();
            }
            else
            {
                _basket[id] = "1";
            }

        }
        else
            _basket.Add(id, "1");
    }

    public void RemoveProduct(string id)
    {
       if(_basket.ContainsKey(id))
            _basket.Remove(id);
    }

    public void ResetBasket()
    {
        _basket.Clear();
    }

    #endregion

    private string API
    {
        get
        {
            switch (_mode)
            {
                case MODE.PRODUCTION : { return PRODUCTIONAPI;}
                case MODE.TEST: { return TESTAPI; }
            }

            return "";
        }
    }

    public static App Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (App)FindObjectOfType(typeof(App));
                }

                return _instance;
            }
        }
    }
}
