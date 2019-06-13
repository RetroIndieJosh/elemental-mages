using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Update() {
        var gamepadGo = Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame;
        var keyboardGo = Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame;

        if( gamepadGo || keyboardGo )
            SceneManager.LoadScene( "main" );
    }
}
