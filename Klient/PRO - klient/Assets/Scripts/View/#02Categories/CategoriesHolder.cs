using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CategoriesHolder : Item
{

    public override void AssignData(JSONNode json)
    {
        _data.Add("itemName",json["name"].Value);

        _name.text = json["name"].Value;
    }

}
