using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameManager>();
                
                if(_instance == null)
                    Debug.LogError("Please add a game manager to the scene.");
                
                return _instance;
            }
        }
    }
}