using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PushableItem : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public LayerMask cantMoveLayer;

    public float moveDuration = 0.1f;

    public Transform visualTransform;   // Visual lơ lửng
    public Transform shadowTransform;   // Shadow co giãn

    private float floatHeight = 0.2f;
    private float floatDuration = 1f;

    private Tween floatTweenVisual;
    private Tween floatTweenShadow;
    private Tween moveTween;
    void Start()
    {
        StartFloating();
    }

    void StartFloating()
    {
        if (visualTransform != null)
        {
            floatTweenVisual = visualTransform.DOLocalMoveY(floatHeight, floatDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
        if (shadowTransform != null)
        {
            floatTweenShadow = shadowTransform.DOScale(new Vector3(0.8f, 0.8f, 1f), floatDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }

    public bool TryPush(Vector3 direction)
    {
        // Danh sách các PushableItem theo hướng đẩy
        List<PushableItem> pushChain = new List<PushableItem>();
        Vector3 checkPos = transform.position + direction;

        // Tìm chuỗi các vật thể có thể đẩy theo hướng direction
        while (true)
        {
            Collider2D hit = Physics2D.OverlapCircle(checkPos, 0.1f, obstacleLayer | cantMoveLayer);

            if (hit == null)
                break;

            // Nếu chạm vật cản cứng (noMoveLayer)
            if (((1 << hit.gameObject.layer) & cantMoveLayer) != 0)
                return false;

            PushableItem item = hit.GetComponent<PushableItem>();
            if (item == null)
                return false;

            pushChain.Add(item);
            checkPos += direction;
        }

        for (int i = pushChain.Count - 1; i >= 0; i--)
        {
            pushChain[i].moveTween?.Kill();
            Vector3 targetPos = pushChain[i].transform.position + direction;
            pushChain[i].moveTween = pushChain[i].transform.DOMove(targetPos, moveDuration).SetEase(Ease.Linear);
        }

        moveTween?.Kill();
        Vector3 myTargetPos = transform.position + direction;
        moveTween = transform.DOMove(myTargetPos, moveDuration).SetEase(Ease.Linear);

        return true;
    }

    public void KillTweens()
    {
        moveTween?.Kill();
        floatTweenVisual?.Kill();
        floatTweenShadow?.Kill();
    }
}