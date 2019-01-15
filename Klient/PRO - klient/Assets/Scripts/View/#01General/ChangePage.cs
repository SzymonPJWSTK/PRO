using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ChangePage : MonoBehaviour {

    public delegate void Next(string pageName, string[] args, Dictionary<string, object> data, DataRequest request);
    public delegate void Previous();
    public event Next NextPage;
    public event Previous PreviousPage;

    public void OnNextPage(string pageName, string[] args, Dictionary<string, object> data, DataRequest request)
    {
        if (NextPage != null)
            NextPage(pageName, args, data, request);
    }

    public void OnPreviousPage()
    {
        if (PreviousPage != null)
            PreviousPage();
    }
}
