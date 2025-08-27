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
    public OutlinedTextColorOption selectedOutlinedTextColor = OutlinedTextColorOption.Black;
    public OutlinedTextColorOptionQuest selectedOutlinedTextColorQuest = OutlinedTextColorOptionQuest.Black;

    public bool autoTurnOff = false;
    public bool useSound = false;

    public bool toggleOnOff = false;
    public bool toggleAct = false;
    public bool toggleSound = false;
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

    public enum OutlinedTextColorOption
    {
        Black,
        White,
        RGB
    }
    
    public enum OutlinedTextColorOptionQuest
    {
        Black,
        White
    }

}
