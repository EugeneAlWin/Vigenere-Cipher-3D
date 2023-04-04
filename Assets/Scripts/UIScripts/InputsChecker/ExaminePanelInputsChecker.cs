﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ENUMS;
using static STATES;
public class ExaminePanelInputsChecker : AbstractInputsChecker
{
    [SerializeField] private TMP_Text result;
    private CipherVector vect;
    [SerializeField] private Button nextButton;
    private void Update()
    {
        nextButton.gameObject.SetActive(CURRENT_EXAMINE_STEP != STEPS.SIXTH);
    }

    internal override void EncodeClick()
    {
        Controller.onStudyModeChanged += SetResult;
        EXAMINE_CURRENT_CHAR_POSITION = 0;
        CURRENT_EXAMINE_ACTION = ACTIONS.ENCODING;
        EXAMINE_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        EXAMINE_KEY = CleanUp(CURRENT_KEY).ToLower();
        EXAMINE_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        EXAMINE_STEP = int.Parse(CleanUp(CURRENT_STEP));
        EXAMINE_DIRECTION = CURRENT_DIRECTION;
        Controller.onStudyModeChanged?.Invoke(STEPS.SECOND, ACTIONS.ENCODING);
    }

    internal override void DecodeClick()
    {
        Controller.onStudyModeChanged += SetResult;
        EXAMINE_CURRENT_CHAR_POSITION = 0;
        CURRENT_EXAMINE_ACTION = ACTIONS.DECODING;
        EXAMINE_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        EXAMINE_KEY = CleanUp(CURRENT_KEY).ToLower();
        EXAMINE_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        EXAMINE_STEP = int.Parse(CleanUp(CURRENT_STEP));
        EXAMINE_DIRECTION = CURRENT_DIRECTION;
        Controller.onStudyModeChanged?.Invoke(STEPS.SECOND, ACTIONS.DECODING);
    }
    private readonly Dictionary<string, CipherVector> vectorsDict = new();
    public void SetResult(STEPS newStep, ACTIONS newAction)
    {
        switch (CURRENT_EXAMINE_STEP)
        {
            case STEPS.NONE:
                break;
            case STEPS.SECOND:
                EXAMINE_CURRENT_LETTER = EXAMINE_MESSAGE[EXAMINE_CURRENT_CHAR_POSITION];
                if (!vectorsDict.TryGetValue($"{EXAMINE_CURRENT_LETTER}{0}{0}", out vect))
                {
                    vect = new CipherVector(
                    EXAMINE_CURRENT_LETTER.ToString(),
                    EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                    0, EXAMINE_DIRECTION, 0, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{EXAMINE_CURRENT_LETTER}{vect.Depth}{vect.Step}", vect);
                }
                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.onCipherVectorChanged?.Invoke(vect);
                break;
            case STEPS.THIRD:
                if (!vectorsDict.TryGetValue($"{EXAMINE_CURRENT_LETTER}{EXAMINE_DEPTH}{0}", out vect))
                {
                    vect = new CipherVector(
                    EXAMINE_CURRENT_LETTER.ToString(),
                    EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                    EXAMINE_DEPTH, EXAMINE_DIRECTION, 0, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{EXAMINE_CURRENT_LETTER}{vect.Depth}{vect.Step}", vect);
                }

                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.onCipherVectorChanged?.Invoke(vect);
                break;
            case STEPS.FOURTH:
                if (!vectorsDict.TryGetValue($"{EXAMINE_CURRENT_LETTER}{EXAMINE_DEPTH}{EXAMINE_STEP}", out vect))
                {
                    vect = new CipherVector(
                    EXAMINE_CURRENT_LETTER.ToString(),
                    EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                    EXAMINE_DEPTH, EXAMINE_DIRECTION, EXAMINE_STEP, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{EXAMINE_CURRENT_LETTER}{vect.Depth}{vect.Step}", vect);
                }

                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.lightFourthStep?.Invoke(EXAMINE_CODED_LETTER, EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString());
                result.text += EXAMINE_CODED_LETTER;
                break;
            case STEPS.FIFTH:
                EXAMINE_CURRENT_LETTER = EXAMINE_MESSAGE[EXAMINE_CURRENT_CHAR_POSITION];
                if (!vectorsDict.TryGetValue($"{EXAMINE_CURRENT_LETTER}{EXAMINE_DEPTH}{EXAMINE_STEP}", out vect))
                {
                    vect = new CipherVector(
                    EXAMINE_CURRENT_LETTER.ToString(),
                    EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                    EXAMINE_DEPTH, EXAMINE_DIRECTION, EXAMINE_STEP, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{EXAMINE_CURRENT_LETTER}{vect.Depth}{vect.Step}", vect);
                }

                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.lightFourthStep?.Invoke(EXAMINE_CODED_LETTER, EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString());
                result.text += EXAMINE_CODED_LETTER;
                break;
            default: break;
        }
    }
}

