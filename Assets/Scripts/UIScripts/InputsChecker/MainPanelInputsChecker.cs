﻿using UnityEngine;
using UnityEngine.UI;
using static ENUMS;
using static STATES;

internal class MainPanelInputsChecker : AbstractInputsChecker
{
    public Toggle latToggle, cyrToggle;
    private readonly float waitTime = .5f;
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= waitTime) return;
        switch (CURRENT_ALPHABET)
        {
            case ALPHABETS.LATIN:
                latToggle.isOn = true;
                cyrToggle.isOn = false;
                break;
            case ALPHABETS.CYRILLIC:
                latToggle.isOn = false;
                cyrToggle.isOn = true;
                break;
            default:
                break;
        }
        timer = 0;
    }

    internal override void SetResult(STEPS newStep, ACTIONS newAction) { }
}

