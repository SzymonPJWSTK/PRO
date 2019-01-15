using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Menu : Page
{
    #region VARIBLES
    public MenuItemHolder _menuItemPrefab;
    public Transform _menuItemsHoder;

    private List<MenuItemHolder> _instantiatedMenu = new List<MenuItemHolder>();
    #endregion

    public override void LoadPage(JSONNode json, Dictionary<string, object> data)
    {
        _pageName.text = (string)data["itemName"];
        _topBar.gameObject.SetActive(true);
        StartCoroutine(GenerateItems(json));
    }

    private IEnumerator GenerateItems(JSONNode json)
    {
        ClearMenu();
        for(int i = 0; i < json.Count; i++)
        {
            MenuItemHolder instantiatedMenuItem = null;
            yield return new WaitUntil(() => InstantiateCategory(out instantiatedMenuItem));
            instantiatedMenuItem.AssignData(json[i]);
            instantiatedMenuItem.CategorySelected = ItemSelected;
            _instantiatedMenu.Add(instantiatedMenuItem);
        }

        _isLoaded = true;
    }

    private bool InstantiateCategory(out MenuItemHolder menuItem)
    {
        menuItem = Instantiate(_menuItemPrefab, _menuItemsHoder);
        return true;
    }

    public void ItemSelected(Dictionary<string, object> data)
    {
        var nextPageData = new Dictionary<string, object>();
        nextPageData.Add("itemName", data["itemName"]);
        OnNextPage("Product", new string[] { "id=" + (string)data["id"] }, nextPageData,App.Instance.GetMenuProduct);
    }

    public override void Return()
    {
        OnPreviousPage();
    }

    private void ClearMenu()
    {
        for (int i = 0; i < _instantiatedMenu.Count; i++)
            Destroy(_instantiatedMenu[i].gameObject);

        _instantiatedMenu.Clear();
    }
}