using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

namespace UnityMidLevel.Menu
{
    /// <summary>
    /// Controls the canvas and all objects within it for the Menu scene.
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Tooltip("The first button view for the menu to start on")]
        [SerializeField]
        private int defaultButtonView = 0;

        [Tooltip("The parent objects of each view in the menu")]
        [SerializeField]
        private GameObject[] buttonView;

        [Tooltip("The default value for gravity, if no playerpref or firestore is found")]
        [SerializeField]
        private float defaultGravityValue;

        [SerializeField]
        private TMP_InputField gravityInputField;

        [Tooltip("The name of the loading scene file found in building settigns")]
        [SerializeField]
        private string loadingSceneName;

        void Start()
        {
            // Load the gravity value from firebase
            FirebaseDatabase.DefaultInstance.GetReference("Gravity")
                .GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError(task.Exception);
                    } else if (task.IsCompleted)
                    {
                        DataSnapshot data = task.Result;
                        if(data.Value == null)
                        {
                            Debug.Log("No data!");
                            float playerPrefGravity = PlayerPrefs.GetFloat("Gravity", -999);
                            if (playerPrefGravity == -999)
                            {
                                gravityInputField.text = defaultGravityValue.ToString();
                            } else
                            {
                                gravityInputField.text = playerPrefGravity.ToString();
                                Debug.Log("gravity input field loaded from playerpref");
                            }
                        } else
                        {
                            gravityInputField.text = data.Value.ToString();
                            Debug.Log("gravity input field loaded from firebase");
                        }
                    } else
                    {
                        Debug.LogWarning("Grativty fetch task is not complete");
                    }
                });

            LoadButtonView(defaultButtonView);
        }

        /// <summary>
        /// Function ran when the gravity input field is modified
        /// Used to keep the value above the minimum of 1
        /// </summary>
        public void GravityInputFieldValueChanged()
        {
            float gravityInputFieldTryParse = -1.0f;
            if (float.TryParse(gravityInputField.text, out gravityInputFieldTryParse))
            {
                if(gravityInputFieldTryParse < 1)
                    gravityInputField.text = "1.0";
            }
        }

        /// <summary>
        /// Save the values in the gravity input field
        /// Called by the "SaveButton" from the menu
        /// </summary>
        public void SaveGravityInputField()
        {
            float gravityInputFieldTryParse = -1.0f;
            if (!float.TryParse(gravityInputField.text, out gravityInputFieldTryParse))
            {
                gravityInputFieldTryParse = defaultGravityValue;
            }

            PlayerPrefs.SetFloat("Gravity", gravityInputFieldTryParse);
            FirebaseDatabase.DefaultInstance.GetReference("Gravity").SetValueAsync(gravityInputFieldTryParse);
        }

        /// <summary>
        /// Set the menu to a specific button view base on an index.
        /// May be called from buttons in the menu
        /// </summary>
        /// <param name="index">The index of the button view to load</param>
        public void LoadButtonView(int index)
        {
            for(int i = 0; i < buttonView.Length; i++)
            {
                buttonView[i].SetActive(i == index);
            }
        }

        /// <summary>
        /// Load the loading scene when the play button is pressed
        /// </summary>
        public void LoadLoadingScene()
        {
            SceneManager.LoadScene(loadingSceneName);
        }
    }
}