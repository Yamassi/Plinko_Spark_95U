using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class InApp : MonoBehaviour
{
    [SerializeField] private GameObject _inAppLoadingPage;
    [SerializeField] private TextMeshProUGUI _messageText;
    private IDataService _dataService;
    public static InApp Instance;

    public void Init(IDataService dataService)
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
            Destroy(gameObject);

        _dataService = dataService;

        // onPurchaseComplete.AddListener(PurschaseComplete);
        // onPurchaseFailed.AddListener(PurchaseFailed);
        // onPurchaseDetailedFailedEvent.AddListener(PurchaseDetailedFailed);
    }

    public void PurschaseComplete(Product product)
    {
        Debug.Log($"PurschaseComplete {product}");
        PurchaseCompletedSuccesfull(product);
        CloseInAppLoadingPage("Succesfull!");
    }

    public void PurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log($"PurchaseFailed {product}");
        CloseInAppLoadingPage("Failed!");
    }

    public void PurchaseDetailedFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        Debug.Log($"PurchaseDetailedFailed {product}");
        CloseInAppLoadingPage("Failed!");
    }

    private void PurchaseCompletedSuccesfull(Product product)
    {
        if (product.definition.id == Const.IAP_1)
        {
            _dataService.AddCoins(5000);
        }

        if (product.definition.id == Const.IAP_2)
        {
            _dataService.AddCoins(10000);
        }
    }

    public void OpenInAppLoadingPage(string message)
    {
        Debug.Log($"Message {message}");
        _inAppLoadingPage.SetActive(true);
        _messageText.text = message;
    }

    public async void CloseInAppLoadingPage(string message)
    {
        Debug.Log($"Message {message}");
        _messageText.text = message;

        await UniTask.Delay(2000);
        _inAppLoadingPage.SetActive(false);
    }
}