using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        void Start()
        {
            LoadButtonView(defaultButtonView);
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
    }
}