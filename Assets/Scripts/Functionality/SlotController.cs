using System;
using System.Collections;
using System.Collections.Generic;
using Best.SocketIO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    [Header("Arrays & Lists")]
    [SerializeField]
    private Sprite[] Slot_Sprites;
    [SerializeField]
    private Transform[] Slot_Transform;
    [SerializeField]
    private Image[] Stop_Images;
    [SerializeField]
    private ImageAnimation[] Stop_Anims;

    [Header("Animated Sprites")]
    [SerializeField]
    private Sprite[] Symbol1;
    [SerializeField]
    private Sprite[] Symbol2;
    [SerializeField]
    private Sprite[] Symbol3;
    [SerializeField]
    private Sprite[] Symbol4;
    [SerializeField]
    private Sprite[] Symbol5;
    [SerializeField]
    private Sprite[] Symbol6;

    internal bool IsSpinning = false;

    internal int SlotNumber = 3;

    [SerializeField]
    private int IconSizeFactor = 0;
    [SerializeField]
    private int SpaceFactor = 0;
    [SerializeField]
    private int tweenHeight = 0;
    [SerializeField]
    private int MidIconSizeFactor = 0;
    [SerializeField]
    private int MidSpaceFactor = 0;
    [SerializeField]
    private int MidtweenHeight = 0;

    private List<Tweener> alltweens = new List<Tweener>();

    [SerializeField]
    private List<int> dummyresponse;

    private void Start()
    {
        tweenHeight = (15 * IconSizeFactor) - 280;
        MidtweenHeight = (15 * MidIconSizeFactor) - 280;
    }

    internal void StartSpin()
    {
        StartCoroutine(TweenRoutine());
    }

    private IEnumerator TweenRoutine()
    {
        IsSpinning = true;
        for (int i = 0; i < SlotNumber; i++)
        {
            if (i == 1)
            {
                InitializeTweening(Slot_Transform[i], true);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                InitializeTweening(Slot_Transform[i]);
                yield return new WaitForSeconds(0.1f);
            }
        }
        for (int i = 0; i < dummyresponse.Count; i++)
        {
            if (dummyresponse[i] != -1)
            {
                PopulateAnimationSprites(Stop_Anims[i], Stop_Images[i], dummyresponse[i]);
            }
            else
            {
                int m_index = UnityEngine.Random.Range(6, 9);
                Stop_Images[i].sprite = Slot_Sprites[m_index];
            }
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < SlotNumber; i++)
        {
            if (i == 1)
            {
                yield return StopTweening(5, Slot_Transform[i], i, true);
            }
            else
            {
                yield return StopTweening(5, Slot_Transform[i], i);
            }
        }

        yield return new WaitForSeconds(0.3f);
        KillAllTweens();
        IsSpinning = false;
    }

    private void ResetSpin()
    {
        for (int i = 0; i < Stop_Anims.Length; i++)
        {
            Stop_Anims[i].StopAnimation();
        }
    }

    private void PopulateAnimationSprites(ImageAnimation animScript,Image StopImage, int val)
    {
        animScript.textureArray.Clear();
        animScript.textureArray.TrimExcess();
        switch (val)
        {
            case 100:
                for (int i = 0; i < Symbol1.Length; i++)
                {
                    animScript.textureArray.Add(Symbol1[i]);
                }
                StopImage.sprite = Slot_Sprites[0];
                break;
            case 0:
                for (int i = 0; i < Symbol2.Length; i++)
                {
                    animScript.textureArray.Add(Symbol2[i]);
                }
                StopImage.sprite = Slot_Sprites[1];
                break;
            case 1:
                for (int i = 0; i < Symbol3.Length; i++)
                {
                    animScript.textureArray.Add(Symbol3[i]);
                }
                StopImage.sprite = Slot_Sprites[2];
                break;
            case 2:
                for (int i = 0; i < Symbol4.Length; i++)
                {
                    animScript.textureArray.Add(Symbol4[i]);
                }
                StopImage.sprite = Slot_Sprites[3];
                break;
            case 5:
                for (int i = 0; i < Symbol5.Length; i++)
                {
                    animScript.textureArray.Add(Symbol5[i]);
                }
                StopImage.sprite = Slot_Sprites[4];
                break;
            case 10:
                for (int i = 0; i < Symbol6.Length; i++)
                {
                    animScript.textureArray.Add(Symbol6[i]);
                }
                StopImage.sprite = Slot_Sprites[5];
                break;
        }
        animScript.StartAnimation();
    }

    private void InitializeTweening(Transform slotTransform, bool IsMid = false)
    {
        if (IsMid)
        {
            slotTransform.localPosition = new Vector2(slotTransform.localPosition.x, 0);
            Tweener tweener = slotTransform.DOLocalMoveY(-MidtweenHeight, 0.2f).SetLoops(-1, LoopType.Restart).SetDelay(0);
            tweener.Play();
            alltweens.Add(tweener);
        }
        else
        {
            slotTransform.localPosition = new Vector2(slotTransform.localPosition.x, 0);
            Tweener tweener = slotTransform.DOLocalMoveY(-tweenHeight, 0.2f).SetLoops(-1, LoopType.Restart).SetDelay(0);
            tweener.Play();
            alltweens.Add(tweener);
        }
    }

    private IEnumerator StopTweening(int reqpos, Transform slotTransform, int index,bool isMid = false)
    {
        if(isMid)
        {
            alltweens[index].Pause();
            int tweenpos = (reqpos * (MidIconSizeFactor + MidSpaceFactor)) - (MidIconSizeFactor + (2 * MidSpaceFactor));
            alltweens[index] = slotTransform.DOLocalMoveY(-tweenpos + 100 + (MidSpaceFactor > 0 ? MidSpaceFactor / 4 : 0), 0.5f).SetEase(Ease.OutElastic);
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            alltweens[index].Pause();
            int tweenpos = (reqpos * (IconSizeFactor + SpaceFactor)) - (IconSizeFactor + (2 * SpaceFactor));
            alltweens[index] = slotTransform.DOLocalMoveY(-tweenpos + 100 + (SpaceFactor > 0 ? SpaceFactor / 4 : 0), 0.5f).SetEase(Ease.OutElastic);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void KillAllTweens()
    {
        for (int i = 0; i < SlotNumber; i++)
        {
            alltweens[i].Kill();
        }
        alltweens.Clear();
    }
}
