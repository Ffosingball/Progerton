using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class KeyRebinder : MonoBehaviour
{
    [SerializeField]
    private InputActionReference[] actionReferences; // Assign in Inspector
    [SerializeField]
    private TMP_Text[] buttonTexts; // UI Text to show current key
    [SerializeField]
    private int numOfActions;

    public SettingsManager settingsManager;
    public UIManager uIManager;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    /*private string prevBinding;
    private int[] group1, group2, group3;

    private void Start()
    {
        group1 = new int[5]{4,5,7,8,9};
        group2 = new int[3]{10,11,12};
        group3 = new int[5]{0,1,2,3,6};
    }*/

    public void updateText()
    {
        for (int i=0; i<4; i++)
        {
            actionReferences[i].action.ApplyBindingOverride(1+i,settingsManager.getSettingsPreferences().keyBindings[i]);
            buttonTexts[i].text = GetButtonTextMove(i);
        }

        for (int i=4; i<numOfActions; i++)
        {
            actionReferences[i].action.ApplyBindingOverride(0,settingsManager.getSettingsPreferences().keyBindings[i]);
            buttonTexts[i].text = GetButtonText(i);
        }
    }

    public void StartRebinding0()
    {
        buttonTexts[0].text = uIManager.getTextForKeyRebinding();
        //prevBinding = actionReferences[0].action.bindings[0].effectivePath;

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(0,0))
            .Start();
    }

    public void StartRebinding1()
    {
        buttonTexts[1].text = uIManager.getTextForKeyRebinding();

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(1,0))
            .Start();
    }

    public void StartRebinding2()
    {
        buttonTexts[2].text = uIManager.getTextForKeyRebinding();

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(2,0))
            .Start();
    }

    public void StartRebinding3()
    {
        buttonTexts[3].text = uIManager.getTextForKeyRebinding();

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(3,0))
            .Start();
    }

    public void StartRebinding4()
    {
        buttonTexts[4].text = uIManager.getTextForKeyRebinding();

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(4,1))
            .Start();
    }

    public void StartRebinding5()
    {
        buttonTexts[5].text = uIManager.getTextForKeyRebinding();

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(5,1))
            .Start();
    }

    public void StartRebinding6()
    {
        buttonTexts[6].text = uIManager.getTextForKeyRebinding();

        actionReferences[numOfActions].action.Disable();
        rebindingOperation = actionReferences[numOfActions].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(6,1))
            .Start();
    }

    public void StartRebinding7()
    {
        buttonTexts[7].text = uIManager.getTextForKeyRebinding();

        actionReferences[7].action.Disable();
        rebindingOperation = actionReferences[7].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(7,2))
            .Start();
    }

    public void StartRebinding8()
    {
        buttonTexts[8].text = uIManager.getTextForKeyRebinding();

        actionReferences[8].action.Disable();
        rebindingOperation = actionReferences[8].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(8,2))
            .Start();
    }

    public void StartRebinding9()
    {
        buttonTexts[9].text = uIManager.getTextForKeyRebinding();

        actionReferences[9].action.Disable();
        rebindingOperation = actionReferences[9].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(9,2))
            .Start();
    }

    public void StartRebinding10()
    {
        buttonTexts[10].text = uIManager.getTextForKeyRebinding();

        actionReferences[10].action.Disable();
        rebindingOperation = actionReferences[10].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(10,2))
            .Start();
    }

    public void StartRebinding11()
    {
        buttonTexts[11].text = uIManager.getTextForKeyRebinding();

        actionReferences[11].action.Disable();
        rebindingOperation = actionReferences[11].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(11,2))
            .Start();
    }

    public void StartRebinding12()
    {
        buttonTexts[12].text = uIManager.getTextForKeyRebinding();

        actionReferences[12].action.Disable();
        rebindingOperation = actionReferences[12].action.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(12,2))
            .Start();
    }

    private void RebindComplete(int i, int type)
    {
        switch (type)
        {
            case 0:
                actionReferences[numOfActions].action.Enable();
                actionReferences[i].action.ApplyBindingOverride(1+i, actionReferences[numOfActions].action.bindings[0].effectivePath);
                settingsManager.getSettingsPreferences().keyBindings[i] = actionReferences[numOfActions].action.bindings[0].effectivePath;
                buttonTexts[i].text = GetButtonTextMove(i);
                break;
            case 1:
                actionReferences[numOfActions].action.Enable();
                actionReferences[i].action.ApplyBindingOverride(0, actionReferences[numOfActions].action.bindings[0].effectivePath);
                settingsManager.getSettingsPreferences().keyBindings[i] = actionReferences[numOfActions].action.bindings[0].effectivePath;
                buttonTexts[i].text = GetButtonText(i);
                break;
            case 2:
                actionReferences[i].action.Enable();
                settingsManager.getSettingsPreferences().keyBindings[i] = actionReferences[i].action.bindings[0].effectivePath;
                buttonTexts[i].text = GetButtonText(i);
                break;
        }
        
        SaveSystem.SaveSettingsPreferences(settingsManager.getSettingsPreferences());
        rebindingOperation.Dispose();

        if(uIManager!=null)
            uIManager.updateText();
    }

    public string GetButtonTextMove(int i)
    {
        return InputControlPath.ToHumanReadableString(
            actionReferences[i].action.bindings[1+i].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public string GetButtonText(int i)
    {
        return InputControlPath.ToHumanReadableString(
            actionReferences[i].action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
