using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SimpleJSON;

public class PagesManager : MonoBehaviour {

    #region VARIBLES
    public Transform _loader;
    public Page _currentPage;

    private Page _nextPage;
    #endregion

    private void Start()
    {
        _currentPage._isLoaded = false;
        _currentPage.NextPage += ChangePage;
        _currentPage.PreviousPage += PreviousPage;
        ShowLoader();

        NavigationStack.Instance.AddPage(new PreviousPage
        {
            pageName = _currentPage.name,
            args = null,
            request = App.Instance.GetCategories,
            data = null
        });
        StartCoroutine(App.Instance.GetCategories(
            null,
            (s) => StartCoroutine(InitialLoad(s)),
            (f) => Error(f)
            ));
    }

    private IEnumerator InitialLoad(JSONNode json)
    {
        _currentPage.gameObject.SetActive(true);
        _currentPage.LoadPage(json,null);
        yield return new WaitUntil(() => _currentPage._isLoaded);
        HideLoader();
    }

    #region CHANGEPAGE
    public void ChangePage(string pageName, string[] args, Dictionary<string, object> data, DataRequest request)
    {
        _nextPage = FindPage(pageName);

        if(_nextPage != null)
        {
            NavigationStack.Instance.AddPage(new PreviousPage
            {
                pageName = pageName,
                args = args,
                request = request,
                data = data
            });

            ShowLoader();
            StartCoroutine(request(
                args,
                (s) => StartCoroutine(PageLoaded(s)),
                (f) => Error(f)
                ));
        }
        else
        {
            Debug.LogError(pageName + " nie istnieje");
        }
    }

    public void PreviousPage()
    {
        var lastPage = NavigationStack.Instance.Return();
        ChangePage(lastPage.pageName, lastPage.args, lastPage.data, lastPage.request);
    }

    public void ReturnBtn()
    {
        _currentPage.Return();
    }
    #endregion

    #region WEBRESPONSE
    private IEnumerator PageLoaded(JSONNode json)
    {
        _nextPage.gameObject.SetActive(true);
        _nextPage._isLoaded = false;
        _nextPage.LoadPage(json, NavigationStack.Instance.LastPage().data);
        yield return new WaitUntil(() => _nextPage._isLoaded);
        _currentPage.gameObject.SetActive(false);
        _nextPage.NextPage += ChangePage;
        _nextPage.PreviousPage += PreviousPage;
        _currentPage.NextPage -= ChangePage;
        _currentPage.PreviousPage -= PreviousPage;
        _currentPage = _nextPage;
        HideLoader();
    }

    private void Error(long errorCode)
    {
        Debug.Log(errorCode);
    }
    #endregion

    #region LOADER
    private void ShowLoader()
    {
        _loader.gameObject.SetActive(true);
    }

    private void HideLoader()
    {
        _loader.gameObject.SetActive(false);
    }
    #endregion

    #region FINDPAGE
    private Page FindPage(string name)
    {
        var childrenObject = FindChildObjects(name);

        if (childrenObject.Count() > 0)
            return childrenObject.ElementAt(0).gameObject.GetComponent<Page>();

        return null;
    }

    private IEnumerable<Transform> FindChildObjects(string name)
    {
        Transform[] childrenObjcets = this.transform.GetComponentsInChildren<Transform>(true);

        var childrens = from child in childrenObjcets
                        where child.name.Equals(name)
                        select child;

        return childrens;
    }
    #endregion

    public void Categories()
    {
        ChangePage("Categories", null, null, App.Instance.GetCategories);
    }

    public void Basket()
    {
        ChangePage("Basket", null,null,App.Instance.PostBasketProducts);
    }
}
