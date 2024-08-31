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
    [SerializeField]
    private TMP_Text Bet_Text;
    [SerializeField]
    private TMP_Text Denom_Text;
    [SerializeField]
    private Button Betplus_Button;
    [SerializeField]
    private Button Betminus_Button;
    [SerializeField]
    private Button Denomplus_Button;
    [SerializeField]
    private Button Denomminus_Button;
    [SerializeField]
    private Image[] Slots_image;
    [SerializeField]
    private Color Disabled_Color;
    [SerializeField]
    private Color Win_Color;
    [SerializeField]
    private Button Sound_Button;
    [SerializeField]
    private Button Mute_Button;
    [SerializeField]
    private Button Rules_Button;
    [SerializeField]
    private Button BetSettings_Button;
    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private RectTransform MenuButtons_Rect;
    [SerializeField]
    private GameObject MenuButton_Object;
    [SerializeField]
    private GameObject CMHeading_object;
    [SerializeField]
    private GameObject RespinHeading_object;
    [SerializeField]
    private GameObject RedSpinSetup;
    [SerializeField]
    private GameObject RulesPopup;
    [SerializeField]
    private GameObject AutoSpinPopup;
    [SerializeField]
    private GameObject Mute_Object;
    [SerializeField]
    private GameObject Sound_Object;
    [SerializeField]
    private Button CloseRules_Button;
    [SerializeField]
    private Button CloseAutoPopup_Button;
    [SerializeField]
    private GameObject PopupMain_Object;

    private int SpinCount = 0;
    private bool isAtOpen = false;
    private bool isBetOpen = false;
    private bool isMenuOpen = false;

    private void Start()
    {
        isAtOpen = false;
        isBetOpen = false;
        isMenuOpen = false;

        if (Spin_Button) Spin_Button.onClick.RemoveAllListeners();
        if (Spin_Button) Spin_Button.onClick.AddListener(StartSpinning);

        if (AutoSpinPanel_Button) AutoSpinPanel_Button.onClick.RemoveAllListeners();
        if (AutoSpinPanel_Button) AutoSpinPanel_Button.onClick.AddListener(OnATClick);

        if (RayCast_Button) RayCast_Button.onClick.RemoveAllListeners();
        if (RayCast_Button) RayCast_Button.onClick.AddListener(OnRayCastClick);

        if (StopAutoSpin_Button) StopAutoSpin_Button.onClick.RemoveAllListeners();
        if (StopAutoSpin_Button) StopAutoSpin_Button.onClick.AddListener(StoppingAutoSpin);

        if (Bet_Button) Bet_Button.onClick.RemoveAllListeners();
        if (Bet_Button) Bet_Button.onClick.AddListener(OnBetClick);

        if (Bet_Slider) Bet_Slider.onValueChanged.RemoveAllListeners();
        if (Bet_Slider) Bet_Slider.onValueChanged.AddListener(OnBetChange);

        if (Denom_Slider) Denom_Slider.onValueChanged.RemoveAllListeners();
        if (Denom_Slider) Denom_Slider.onValueChanged.AddListener(OnDenomChange);

        if (Betplus_Button) Betplus_Button.onClick.RemoveAllListeners();
        if (Betplus_Button) Betplus_Button.onClick.AddListener(delegate { OnBetButton(true); });

        if (Betminus_Button) Betminus_Button.onClick.RemoveAllListeners();
        if (Betminus_Button) Betminus_Button.onClick.AddListener(delegate { OnBetButton(false); });

        if (Denomplus_Button) Denomplus_Button.onClick.RemoveAllListeners();
        if (Denomplus_Button) Denomplus_Button.onClick.AddListener(delegate { OnDenomButton(true); });

        if (Denomminus_Button) Denomminus_Button.onClick.RemoveAllListeners();
        if (Denomminus_Button) Denomminus_Button.onClick.AddListener(delegate { OnDenomButton(false); });

        if (Menu_Button) Menu_Button.onClick.RemoveAllListeners();
        if (Menu_Button) Menu_Button.onClick.AddListener(OnMenuClick);

        if (Mute_Button) Mute_Button.onClick.RemoveAllListeners();
        if (Mute_Button) Mute_Button.onClick.AddListener(delegate { ToggleSound(false); });

        if (Sound_Button) Sound_Button.onClick.RemoveAllListeners();
        if (Sound_Button) Sound_Button.onClick.AddListener(delegate { ToggleSound(true); });

        if (BetSettings_Button) BetSettings_Button.onClick.RemoveAllListeners();
        if (BetSettings_Button) BetSettings_Button.onClick.AddListener(OnBetClick);

        if (Rules_Button) Rules_Button.onClick.RemoveAllListeners();
        if (Rules_Button) Rules_Button.onClick.AddListener(OpenRulesPopup);

        if (Settings_Button) Settings_Button.onClick.RemoveAllListeners();
        if (Settings_Button) Settings_Button.onClick.AddListener(OpenSettingsPopup);

        if (CloseRules_Button) CloseRules_Button.onClick.RemoveAllListeners();
        if (CloseRules_Button) CloseRules_Button.onClick.AddListener(CloseRulesPopup);

        if (CloseAutoPopup_Button) CloseAutoPopup_Button.onClick.RemoveAllListeners();
        if (CloseAutoPopup_Button) CloseAutoPopup_Button.onClick.AddListener(CloseSettingsPopup);

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
        if (isBetOpen)
        {
            OnBetClick();
        }
        if(isMenuOpen)
        {
            OnMenuClick();
        }
        ToggleButtonGrp(false);
        if (slotManager) slotManager.StartSpin();
    }

    private void OnRayCastClick()
    {
        if(isAtOpen)
        {
            OnATClick();
        }
        if(isBetOpen)
        {
            OnBetClick();
        }
        if(isMenuOpen)
        {
            OnMenuClick();
        }
    }

    #region AutoSpin
    private void OnATClick()
    {
        if (AutoSpinPanel_Button) AutoSpinPanel_Button.interactable = false;
        if (!isAtOpen)
        {
            if (AT_ImageTransform) AT_ImageTransform.DORotate(new Vector3(0, 180, 0), 0.5f);
            if (AT_GameObject) AT_GameObject.SetActive(true);
            if (isBetOpen)
            {
                OnBetClick();
            }
            if(isMenuOpen)
            {
                OnMenuClick();
            }
            if (RayCast_Object) RayCast_Object.SetActive(true);
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
    #endregion

    #region BetSettings
    private void OnBetClick()
    {
        if (!isBetOpen)
        {
            if (BetPanel_Object) BetPanel_Object.SetActive(true);
            if (isAtOpen)
            {
                OnATClick();
            }
            if (isMenuOpen)
            {
                OnMenuClick();
            }
            isBetOpen = true;
            if (RayCast_Object) RayCast_Object.SetActive(true);
        }
        else
        {
            if (RayCast_Object) RayCast_Object.SetActive(false);
            if (BetPanel_Object) BetPanel_Object.SetActive(false);
            isBetOpen = false;
        }
    }

    private void OnBetChange(float value)
    {
        int myvalue = 0;
        switch (value)
        {
            case 0:
                myvalue = 1;
                if (slotManager) slotManager.SlotNumber = 1;
                if (Slots_image[1]) Slots_image[1].color = Disabled_Color;
                if (Slots_image[2]) Slots_image[2].color = Disabled_Color;
                break;
            case 1:
                myvalue = 5;
                if (slotManager) slotManager.SlotNumber = 2;
                if (Slots_image[1]) Slots_image[1].color = Color.white;
                if (Slots_image[2]) Slots_image[2].color = Disabled_Color;
                break;
            case 2:
                myvalue = 10;
                if (slotManager) slotManager.SlotNumber = 3;
                if (Slots_image[1]) Slots_image[1].color = Color.white;
                if (Slots_image[2]) Slots_image[2].color = Color.white;
                break;
        }
        if (Bet_Text) Bet_Text.text = myvalue.ToString();
    }

    private void OnDenomChange(float value)
    {
        int myvalue = 0;
        switch (value)
        {
            case 0:
                myvalue = 1;
                break;
            case 1:
                myvalue = 10;
                break;
        }
        if (Denom_Text) Denom_Text.text = myvalue.ToString();
    }

    private void OnBetButton(bool isIncrement)
    {
        if (isIncrement) 
        {
            if (Bet_Slider.value < 2)
            {
                Bet_Slider.value++;
            }
        }
        else
        {
            if (Bet_Slider.value > 0)
            {
                Bet_Slider.value--;
            }
        }
    }

    private void OnDenomButton(bool isIncrement)
    {
        if (isIncrement)
        {
            if (Denom_Slider.value < 1)
            {
                Denom_Slider.value++;
            }
        }
        else
        {
            if (Denom_Slider.value > 0)
            {
                Denom_Slider.value--;
            }
        }
    }
    #endregion

    private void OnMenuClick()
    {
        if (!isMenuOpen)
        {
            if (MenuButton_Object) MenuButton_Object.SetActive(true);
            if (isBetOpen)
            {
                OnBetClick();
            }
            if(isAtOpen)
            {
                OnATClick();
            }
            if (RayCast_Object) RayCast_Object.SetActive(true);
            if (MenuButtons_Rect) MenuButtons_Rect.DOLocalMoveY(-36, 0.5f);
            isMenuOpen = true;
        }
        else
        {
            if (RayCast_Object) RayCast_Object.SetActive(false);
            if (MenuButtons_Rect) MenuButtons_Rect.DOLocalMoveY(-445, 0.5f).OnComplete(delegate
            {
                if (MenuButton_Object) MenuButton_Object.SetActive(false);
            });
            isMenuOpen = false;
        }
    }

    private void ToggleSound(bool isActive)
    {
        if(isActive)
        {
            if (Mute_Object) Mute_Object.SetActive(true);
            if (Sound_Object) Sound_Object.SetActive(false);
        }
        else
        {
            if (Mute_Object) Mute_Object.SetActive(false);
            if (Sound_Object) Sound_Object.SetActive(true);
        }
    }

    private void OpenRulesPopup()
    {
        OnMenuClick();
        if (PopupMain_Object) PopupMain_Object.SetActive(true);
        if (RulesPopup) RulesPopup.SetActive(true);
    }

    private void CloseRulesPopup()
    {
        if (PopupMain_Object) PopupMain_Object.SetActive(false);
        if (RulesPopup) RulesPopup.SetActive(false);
    }

    private void OpenSettingsPopup()
    {
        OnMenuClick();
        if (PopupMain_Object) PopupMain_Object.SetActive(true);
        if (AutoSpinPopup) AutoSpinPopup.SetActive(true);
    }

    private void CloseSettingsPopup()
    {
        if (PopupMain_Object) PopupMain_Object.SetActive(false);
        if (AutoSpinPopup) AutoSpinPopup.SetActive(false);
    }

    #region InternalMethods
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

    internal void AddWinColor(int value)
    {
        Slots_image[value].color = Win_Color;
    }

    internal void resetWinColor()
    {
        foreach(Image i in Slots_image)
        {
            i.color = Color.white;
        }
    }

    internal void StopAutoSpin()
    {
        if (AutoSpinImage_Object) AutoSpinImage_Object.SetActive(false);
        if (SpinImage_Object) SpinImage_Object.SetActive(true);
    }

    internal void GreenRespin(bool isActice)
    {
        if(isActice)
        {
            if (CMHeading_object) CMHeading_object.SetActive(false);
            if (RespinHeading_object) RespinHeading_object.SetActive(true);
        }
        else
        {
            if (CMHeading_object) CMHeading_object.SetActive(true);
            if (RespinHeading_object) RespinHeading_object.SetActive(false);
        }
    }

    internal void RedRespin(bool isActive)
    {
        if(isActive)
        {
            StartCoroutine(RedRespinRoutine());
        }
        else
        {
            if (RedSpinSetup) RedSpinSetup.SetActive(false);
        }
    }

    private IEnumerator RedRespinRoutine()
    {
        if (RedSpinSetup) RedSpinSetup.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (RedSpinSetup) RedSpinSetup.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (RedSpinSetup) RedSpinSetup.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (RedSpinSetup) RedSpinSetup.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (RedSpinSetup) RedSpinSetup.SetActive(true);
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
    #endregion
}
