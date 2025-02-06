using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultView : MonoBehaviour
{
    [SerializeField] private Image _deliveryResultImage;

    [SerializeField] private Storage[] _storages;

    [SerializeReference] private Sprite _likeSprite;
    [SerializeReference] private Sprite _dislikeSprite;

    private void Awake()
    {
        _deliveryResultImage.gameObject.SetActive(false);

        foreach (Storage storage in _storages)
            storage.ResultResourcesDelivery += ShowResultResourcesDelivery;
    }

    private void ShowResultResourcesDelivery(DeliveryResult deliveryResult)
    {
        StartCoroutine(ShowResultResourcesDeliveryRoutine(deliveryResult));
    }

    private IEnumerator ShowResultResourcesDeliveryRoutine(DeliveryResult deliveryResult)
    {
        _deliveryResultImage.gameObject.SetActive(true);
        _deliveryResultImage.sprite = deliveryResult == DeliveryResult.Correct ? _likeSprite : _dislikeSprite;
        yield return new WaitForSeconds(1f);
        _deliveryResultImage.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (Storage storage in _storages)
            storage.ResultResourcesDelivery -= ShowResultResourcesDelivery;
    }
}
