using UnityEngine;

public class DragonScaleBuffs : MonoBehaviour
{
    private Vector3 _normalScale;
    private float _normalSpaceBetween;
    private DragonTail _dragonTail;
    private TailEffect _tailEffect;
    private UIManager _uiManager;
    private DragonCollision _dragonCollision;

    private void Start()
    {
        _dragonTail = GetComponent<DragonTail>();
        _tailEffect = GetComponent<TailEffect>();
        _uiManager = FindObjectOfType<UIManager>();
        _dragonCollision = GetComponent<DragonCollision>();
        _normalScale = transform.GetChild(0).localScale;
    }

    public void ApplyScaleBuff(Vector3 newScale, float newSpaceBetween, float duration)
    {
        _dragonCollision.SetCrushBuff(true);
        _tailEffect.ApplyScale(newScale);
        _normalSpaceBetween = _dragonTail.GetSpaceBetween();
        _dragonTail.SetSpaceBetween(newSpaceBetween);
        Invoke(nameof(RemoveBuff), duration);
        _uiManager.StartTimer(duration);
    }

    public void RemoveBuff()
    {
        _dragonCollision.SetCrushBuff(false);
        _tailEffect.ApplyScale(_normalScale);
        _dragonTail.SetSpaceBetween(_normalSpaceBetween);
    }
}