using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Meal : MonoBehaviour {

    #region VARIBLES
    public Transform _mealsHolder;
    public MealItem _mealPrefab;

    private JSONNode _json;
    private bool _newData;
    #endregion

    private void Start()
    {
        App.Instance.OnDataRecived += OrderRecived;
    }

    private void OrderRecived(JSONNode json)
    {
        if (json["zamówienie"].Count > 0)
            return;

        _json = json;
        _newData = true;
    }

    private void Update()
    {
        if(_newData)
        {
            _newData = false;
            StartCoroutine(MenuRecived(_json));
        }
    }

    private IEnumerator MenuRecived(JSONNode json)
    {
        for(int i = 0; i < json.Count; i++)
        {
            MealItem instantiatedMeal = null;
            yield return new WaitUntil(() => InstantiateMeal(out instantiatedMeal));
            instantiatedMeal.AssignData(json[i]);
        }
    }

    private bool InstantiateMeal(out MealItem instantiatedMeal)
    {
        instantiatedMeal = Instantiate(_mealPrefab, _mealsHolder);
        return true;
    }

    private void MealsError(long errorCode)
    {

    }
}
