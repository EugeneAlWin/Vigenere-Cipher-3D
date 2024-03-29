﻿using UnityEngine;
using static ENUMS;
using static STATES;

public abstract partial class AbstractInputsChecker : MonoBehaviour
{
    internal void MessageValueChanged()
    {
        string trimmedMessage = CleanUp(messageInput.text);
        if (
            InputRegex.IsMatch(
            trimmedMessage.ToLower().Replace(" ", ""),
            CURRENT_ALPHABET == ALPHABETS.LATIN ? InputRegex.latMessage : InputRegex.cyrMessage
            ))
            CURRENT_MESSAGE = "<color=#fff>" + trimmedMessage + "</color>";
        messageInput.text = CURRENT_MESSAGE;
    }
    internal void KeyValueChanged()
    {
        string trimmedKey = CleanUp(keyInput.text);
        if (
            InputRegex.IsMatch(
            trimmedKey.ToLower(),
            CURRENT_ALPHABET == ALPHABETS.LATIN ? InputRegex.latKey : InputRegex.cyrKey
            ))
            CURRENT_KEY = "<color=#fff>" + trimmedKey + "</color>";
        keyInput.text = CURRENT_KEY;
    }
    internal void DepthValueChanged()
    {
        string trimmedDepth = CleanUp(depthInput.text);
        if (InputRegex.IsMatch(trimmedDepth, InputRegex.Depth) && trimmedDepth.Length <= 2)
            CURRENT_DEPTH = "<color=#fff>" + trimmedDepth + "</color>";
        depthInput.text = CURRENT_DEPTH;
    }
    internal void DirectionValueChanged()
    {
        CURRENT_DIRECTION = (DIRECTIONS)directionDropdown.value;
    }
    internal void StepValueChanged()
    {
        string trimmedStep = CleanUp(stepInput.text);
        if (InputRegex.IsMatch(trimmedStep, InputRegex.Step) && trimmedStep.Length <= 2)
            CURRENT_STEP = "<color=#fff>" + trimmedStep + "</color>";
        stepInput.text = CURRENT_STEP;
    }
}

