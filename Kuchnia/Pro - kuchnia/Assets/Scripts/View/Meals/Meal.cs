using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Meal : MonoBehaviour {

    #region VARIBLES
    public Transform _mealsHolder;
    public MealItem _mealPrefab;
    #endregion

    private void Start()
    {
        StartCoroutine(App.Instance.GetMenuAll(
            (s) => StartCoroutine(MenuRecived(s)),
            (f) => MealsError(f)
        ));
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
