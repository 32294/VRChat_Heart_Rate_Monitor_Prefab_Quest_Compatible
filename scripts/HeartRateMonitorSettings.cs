using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class HeartRateMonitorSettings : MonoBehaviour
{
    public GameObject selectedPrefab;

    public PlatformOption selectedPlatform = PlatformOption.PC;
    public StyleOption selectedStyle = StyleOption.ScreenHeart;
    public ColorOption selectedColor = ColorOption.Default;
    public ColorOptionHUD selectedColorHUD = ColorOptionHUD.Default;
    public TextColorOption selectedTextColor = TextColorOption.Default;
    public TextColorOptionQuest selectedTextColorQuest = TextColorOptionQuest.White;
    
    public TextOutlineColorOption selectedTextOutlineColor = TextOutlineColorOption.White;

    public bool autoTurnOff = false;

    public bool toggleOnOff = false;
    public bool toggleAct = false;
    public bool toggleHUD = false;

    public enum PlatformOption
    {
        PC,
        Quest
    }

    public enum StyleOption
    {
        ScreenHeart,
        ScreenSquare,
        Heart,
        Text,
        OutlinedText,
        HUD
    }

    public enum ColorOption
    {
        Default,
        White,
        RGB
    }

    public enum ColorOptionHUD
    {
        Default,
        RGB
    }

    public enum TextColorOption
    {
        Default,
        White,
        Black,
        RGB
    }
    
    public enum TextColorOptionQuest
    {
        White,
        Black
    }

    public enum TextOutlineColorOption
    {
        White,
        Black,
        RGB
    }

}
