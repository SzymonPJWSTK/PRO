using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

[System.Serializable]
public struct PreviousPage
{
    public string pageName;
    public string[] args;
    public Dictionary<string, object> data;
    public DataRequest request;
}

public class NavigationStack : MonoBehaviour {

    #region VARIBLES
    public static NavigationStack _instance;
    public static object _lock = new object();

    public List<PreviousPage> _previousPages = new List<PreviousPage>();
    #endregion

    public void AddPage(PreviousPage previous)
    {
        if (_previousPages.Count - 1 > 0)
        {
            if (previous.pageName == _previousPages[_previousPages.Count - 1].pageName)
                return;
        }

        if (previous.pageName == "Categories")
            _previousPages.Clear();

        _previousPages.Add(previous);
    }

    public PreviousPage Return()
    {
        _previousPages.RemoveAt(_previousPages.Count - 1);
        return _previousPages[_previousPages.Count - 1];
    }

    public PreviousPage LastPage()
    {
        return _previousPages[_previousPages.Count - 1];
    }

    public static NavigationStack Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (NavigationStack)FindObjectOfType(typeof(NavigationStack));
                }

                return _instance;
            }
        }
    }
}
