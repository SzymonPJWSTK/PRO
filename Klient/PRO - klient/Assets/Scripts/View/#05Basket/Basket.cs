using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Basket : Page
{
    #region VARIBLES
    public BasketProductHolder _basketProductPrefab;
    public Transform _productsHolder;
    public Text _sumPrice;

    public List<BasketProductHolder> _instantiatedProducts = new List<BasketProductHolder>();
    #endregion

    public override void LoadPage(JSONNode json, Dictionary<string, object> data)
    {
        _pageName.text = "Koszyk";
        _topBar.gameObject.SetActive(true);
        StartCoroutine(LoadProducts(json));
        RecalculateBasket();
    }

    public override void Return()
    {
        OnPreviousPage();
    }

    private IEnumerator LoadProducts(JSONNode json)
    {
        ClearBasketProducts();
        for(int i = 0; i < json.Count; i++)
        {
            BasketProductHolder instantiatedProduct = null;
            yield return new WaitUntil(() => InstantiateProduct(out instantiatedProduct));
            instantiatedProduct.AssignData(json[i]);
            instantiatedProduct.Remove = RemoveProduct;
            _instantiatedProducts.Add(instantiatedProduct);
        }

        _isLoaded = true;
    }

    public void RemoveProduct(string id)
    {
        var product = from p in _instantiatedProducts
                      where p.ID.Equals(id)
                      select p;

        if(product.Count() > 0)
        {
            Destroy(product.ElementAt(0).gameObject);
            App.Instance.RemoveProduct(id);
            _instantiatedProducts.Remove(product.ElementAt(0));
            RecalculateBasket();
        }
    }

    private bool InstantiateProduct(out BasketProductHolder product)
    {
        product = Instantiate(_basketProductPrefab, _productsHolder);
        return true;
    }

    private void ClearBasketProducts()
    {
        for (int i = 0; i < _instantiatedProducts.Count; i++)
            Destroy(_instantiatedProducts[i].gameObject);

        _instantiatedProducts.Clear();
    }

    public void PlaceOrder()
    {
        StartCoroutine(App.Instance.PostBasketPlace(
            (s) => Success(s),
            (f) => Failure(f)
        ));
    }

    private void Success(JSONNode json)
    {
        App.Instance.ResetBasket();
        OnNextPage("Categories", null, null, App.Instance.GetCategories);
    }

    private void Failure(long code)
    {

    }

    #region RECALCULATEBASKET
    private void RecalculateBasket()
    {
        StartCoroutine(App.Instance.PostBasketRecalculate(
            (s) => RecalculateSuccess(s),
            (f) => RecalculateFailure(f)
        ));
    }

    private void RecalculateSuccess(JSONNode json)
    {
        _sumPrice.text = string.Format("Suma: {0} zł", json["cost"].Value);
    }

    private void RecalculateFailure(long code)
    {
        Debug.Log(code);
    }
    #endregion
}
