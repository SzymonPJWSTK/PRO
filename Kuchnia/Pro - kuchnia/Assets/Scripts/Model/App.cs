using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;
using SimpleJSON;

public enum MODE { TEST,PRODUCTION}

public class App : MonoBehaviour {

    #region VARIBLES
    public static App _instance;
    public static object _lock = new object();

    private const string TESTAPI = "http://localhost:8080/";
    private const string PRODUCTIONAPI = "";

    public MODE _mode = MODE.TEST;

    private WebSocket ws = new WebSocket("ws://localhost:8081/");
    #endregion

    #region EVENTS
    public event System.Action<JSONNode> OnDataRecived;
    #endregion

    private void Start()
    {
        ws.OnOpen += OnOpen;
        ws.OnMessage += OnMessage;

        ws.ConnectAsync();
    }

    #region WEBSOCKET
    private void OnOpen(object sender, System.EventArgs e)
    {

    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
        var json = JSONNode.Parse(e.Data);
        if(json.Count > 0)
        {
            if (OnDataRecived != null)
                OnDataRecived(json);
        }
    }

    private void OnClose()
    {

    }

    private void OnError()
    {

    }

    private void Cokolwiek(string data)
    {
    }
    #endregion

    public string API
    {
        get
        {
            switch (_mode)
            {
                case MODE.PRODUCTION: { return PRODUCTIONAPI; }
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