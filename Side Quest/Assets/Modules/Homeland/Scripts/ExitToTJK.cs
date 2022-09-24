// Copyright 2022 Niantic, Inc. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace Niantic.ARVoyage
{
    /// <summary>
    /// Manages the UI button that allows users to go from the Homeland scene to the VPS scene
    /// </summary>
    public class ExitToTJK : MonoBehaviour
    {
        [SerializeField] private Button exitToTJKButton;
        [SerializeField] private GameObject exitToTJKPane;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button letsGoButton;

        protected LevelSwitcher levelSwitcher;
        AudioManager audioManager;

        void Awake()
        {
            levelSwitcher = SceneLookup.Get<LevelSwitcher>();
        }

        public void OnToggleButtonClick()
        {
            ButtonSFX();
            exitToTJKPane.SetActive(!exitToTJKPane.activeInHierarchy);
        }

        public void OnCancelButtonClick()
        {
            ButtonSFX();
            exitToTJKPane.SetActive(false);
        }

        public void OnLetsGoButtonClick()
        {
            ButtonSFX();

            if (levelSwitcher != null)
            {
                Debug.Log("Exit to TJK");
                levelSwitcher.ExitToTJK();
            }
            else
            {
                Debug.LogError(name + " couldn't find scene switcher");
            }
        }

        protected void ButtonSFX(string audioKey = AudioKeys.UI_Button_Press)
        {
            audioManager = SceneLookup.Get<AudioManager>();
            audioManager?.PlayAudioNonSpatial(audioKey);
        }
    }
}
