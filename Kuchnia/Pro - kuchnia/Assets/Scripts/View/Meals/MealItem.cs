using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class MealItem : MonoBehaviour {

    #region VARIBLES
    public Text _name;
    public Toggle _isAvailable;

    private string _id;
    #endregion

    public void AssignData(JSONNode json)
    {
        _id = json["___id"].Value;
        _name.text = json["name"].Value;
        _isAvailable.isOn = bool.Parse(json["available"].Value);
    }

    public void ToggleChanged()
    {
        StartCoroutine(App.Instance.PostMenuAvailability(
            _isAvailable.isOn,
            _id,
            (s) => ChangeSuccess(s),
            (f) => ChangeError(f)
        ));
    }

    private void ChangeSuccess(JSONNode json)
    {

    }

    private void ChangeError(long responseCode)
    {
        Debug.Log(responseCode);
    }
}
