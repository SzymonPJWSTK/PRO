using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Categories : Page
{
    #region VARIBLES
    public CategoriesHolder _categoriesPrefab;
    public Transform _categoriesHolder;

    private List<CategoriesHolder> _instantiatedCategories = new List<CategoriesHolder>();
    #endregion

    public override void LoadPage(JSONNode json, Dictionary<string, object> data)
    {
        _topBar.gameObject.SetActive(false);
        StartCoroutine(LoadCategories(json));
    }

    public IEnumerator LoadCategories(JSONNode categories)
    {
        ClearCatregories();
        for(int i = 0; i < categories.Count; i++)
        {
            CategoriesHolder instantiatedCategory = null;
            yield return new WaitUntil(() => InstantiateCategories(out instantiatedCategory));
            instantiatedCategory.AssignData(categories[i]);
            instantiatedCategory.CategorySelected = CategorySelected;
            _instantiatedCategories.Add(instantiatedCategory);
        }

        _isLoaded = true;
    }

    private bool InstantiateCategories(out CategoriesHolder categories)
    {
        categories = Instantiate(_categoriesPrefab, _categoriesHolder);
        return true;
    }

    public void CategorySelected(Dictionary<string, object> data)
    {
        OnNextPage("Menu", new string[] { "category="+ (string)data["itemName"] }, data, App.Instance.GetMenu);
    }

    private void ClearCatregories()
    {
        for (int i = 0; i < _instantiatedCategories.Count; i++)
            Destroy(_instantiatedCategories[i].gameObject);

        _instantiatedCategories.Clear();
    }

    public override void Return()
    {
        Application.Quit();
    }
}