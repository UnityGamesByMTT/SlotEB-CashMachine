using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button Spin_Button;
    [SerializeField]
    private Button AutoSpin_Button;
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private SlotController slotManager;

    private void Start()
    {
        if (Spin_Button) Spin_Button.onClick.RemoveAllListeners();
        if (Spin_Button) Spin_Button.onClick.AddListener(StartSpinning);
    }

    private void StartSpinning()
    {
        if (slotManager) slotManager.StartSpin();
    }
}
