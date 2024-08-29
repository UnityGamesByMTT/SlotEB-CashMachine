using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button Spin_Button;
    [SerializeField]
    private Button StopAutoSpin_Button;
    [SerializeField]
    private Button AutoSpinPanel_Button;
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject AT_GameObject;
    [SerializeField]
    private RectTransform AT_Transform;
    [SerializeField]
    private Button RayCast_Button;
    [SerializeField]
    private GameObject RayCast_Object;
    [SerializeField]
    private Transform AT_ImageTransform;
    [SerializeField]
    private SlotController slotManager;
    [SerializeField]
    private Button[] AutoCount_Buttons;
    [SerializeField]
    private GameObject SpinImage_Object;
    [SerializeField]
    private GameObject AutoSpinImage_Object;
    [SerializeField]
    private TMP_Text AutoCounter_Text;
    [SerializeField]
    private GameObject BetPanel_Object;
    [SerializeField]
    private Button Bet_Button;
    [SerializeField]
    private Slider Bet_Slider;
    [SerializeField]
    private Slider Denom_Slider;

    private int SpinCount = 0;
    private bool isAtOpen = false;
    private bool isBetOpen = false;

    private void Start()
    {
        isAtOpen = false;

        if (Spin_Button) Spin_Button.onClick.RemoveAllListeners();
        if (Spin_Button) Spin_Button.onClick.AddListener(StartSpinning);

        if (AutoSpinPanel_Button) AutoSpinPanel_Button.onClick.RemoveAllListeners();
        if (AutoSpinPanel_Button) AutoSpinPanel_Button.onClick.AddListener(OnATClick);

        if (RayCast_Button) RayCast_Button.onClick.RemoveAllListeners();
        if (RayCast_Button) RayCast_Button.onClick.AddListener(OnATClick);

        if (StopAutoSpin_Button) StopAutoSpin_Button.onClick.RemoveAllListeners();
        if (StopAutoSpin_Button) StopAutoSpin_Button.onClick.AddListener(StoppingAutoSpin);

        

        for (int i = 0; i < AutoCount_Buttons.Length; i++)
        {
            switch(i)
            {
                case 0:
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.RemoveAllListeners();
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.AddListener(OnATClick);
                    break;
                case 1:
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.RemoveAllListeners();
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.AddListener(delegate { ChangeAutoView(10); OnATClick(); });
                    break;
                case 2:
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.RemoveAllListeners();
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.AddListener(delegate { ChangeAutoView(25); OnATClick(); });
                    break;
                case 3:
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.RemoveAllListeners();
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.AddListener(delegate { ChangeAutoView(50); OnATClick(); });
                    break;
                case 4:
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.RemoveAllListeners();
                    if (AutoCount_Buttons[i]) AutoCount_Buttons[i].onClick.AddListener(delegate { ChangeAutoView(100); OnATClick(); });
                    break;
            }
        }
    }

    private void StartSpinning()
    {
        if (isAtOpen) 
        {
            OnATClick();
        }
        ToggleButtonGrp(false);
        if (slotManager) slotManager.StartSpin();
    }
    private void OnATClick()
    {
        if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = false;
        if (!isAtOpen)
        {
            if (RayCast_Object) RayCast_Object.SetActive(true);
            if (AT_ImageTransform) AT_ImageTransform.DORotate(new Vector3(0, 180, 0), 0.5f);
            if (AT_GameObject) AT_GameObject.SetActive(true);
            if (AT_Transform) AT_Transform.DOLocalMoveX(0, 0.5f).OnComplete(delegate
            {
                if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = true;
            });
            isAtOpen = true;
        }
        else
        {
            if (RayCast_Object) RayCast_Object.SetActive(false);
            if (AT_ImageTransform) AT_ImageTransform.DORotate(new Vector3(0, 0, 0), 0.5f);
            if (AT_GameObject) AT_GameObject.SetActive(true);
            if (AT_Transform) AT_Transform.DOLocalMoveX(940, 0.5f).OnComplete(delegate
            {
                if (AT_GameObject) AT_GameObject.SetActive(false);
                if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = true;
            });
            isAtOpen = false;
        }
    }

    private void OnBetClick()
    {
        if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = false;
        if (!isAtOpen)
        {
            if (RayCast_Object) RayCast_Object.SetActive(true);
            if (AT_ImageTransform) AT_ImageTransform.DORotate(new Vector3(0, 180, 0), 0.5f);
            if (AT_GameObject) AT_GameObject.SetActive(true);
            if (AT_Transform) AT_Transform.DOLocalMoveX(0, 0.5f).OnComplete(delegate
            {
                if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = true;
            });
            isAtOpen = true;
        }
        else
        {
            if (RayCast_Object) RayCast_Object.SetActive(false);
            if (AT_ImageTransform) AT_ImageTransform.DORotate(new Vector3(0, 0, 0), 0.5f);
            if (AT_GameObject) AT_GameObject.SetActive(true);
            if (AT_Transform) AT_Transform.DOLocalMoveX(940, 0.5f).OnComplete(delegate
            {
                if (AT_GameObject) AT_GameObject.SetActive(false);
                if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = true;
            });
            isAtOpen = false;
        }
    }

    private void ChangeAutoView(int count)
    {
        SpinCount = count;
        if (StopAutoSpin_Button) StopAutoSpin_Button.interactable = true;
        if (AutoSpinImage_Object) AutoSpinImage_Object.SetActive(true);
        if (SpinImage_Object) SpinImage_Object.SetActive(false);
        updateAutoCount(SpinCount);
    }

    private void StartAutoSpin()
    {
        ToggleButtonGrp(false);
        if (slotManager) slotManager.AutoSpin(SpinCount);
    }

    internal void StoppingAutoSpin()
    {
        if (slotManager.IsAutoSpin)
        {
            if (StopAutoSpin_Button) StopAutoSpin_Button.interactable = false;
            if (slotManager) slotManager.StopAutoSpin();
        }
        else
        {
            StartAutoSpin();
        }
    }

    internal void updateAutoCount(int count)
    {
        if (AutoCounter_Text) AutoCounter_Text.text = count.ToString();
    }

    internal void StopAutoSpin()
    {
        if (AutoSpinImage_Object) AutoSpinImage_Object.SetActive(false);
        if (SpinImage_Object) SpinImage_Object.SetActive(true);
    }

    internal void ToggleButtonGrp(bool isActive)
    {
        if (Spin_Button) Spin_Button.interactable = isActive;
        if (slotManager.IsAutoSpin && isActive)
        {
            if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = false;
        }
        else
        {
            if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = isActive;
        }
        if (Spin_Button) Spin_Button.interactable = isActive;
    }
}
