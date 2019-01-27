using ggj;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Stealth : MonoBehaviour
{
    public string StealthLayerName;
    private PlayerController _playerController;
    public bool IsVisible { get; set; }

    private int _stealthLayer;
    private int _originalLayer;

    // Start is called before the first frame update
    void Awake()
    {
        IsVisible = true;
        _stealthLayer = LayerMask.NameToLayer(StealthLayerName);
        _originalLayer = gameObject.layer;
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(gameObject.layer == _stealthLayer && !_playerController.IsHidden && IsVisible)
        {
            gameObject.layer = _originalLayer;
        }
        else if(gameObject.layer == _originalLayer && (_playerController.IsHidden || !IsVisible))
        {
            gameObject.layer = _stealthLayer;
        }
    }
}
