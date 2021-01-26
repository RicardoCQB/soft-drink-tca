using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Library added specifically to this script

public class PauseMenu : MonoBehaviour
{
    public UnityEvent openMenu, closeMenu;
    bool pauseIsOpen = false;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && pauseIsOpen == false)
        {
            openMenu.Invoke();
            pauseIsOpen = true;
        }
        else if (Input.GetButtonDown("Cancel") && pauseIsOpen)
        {
            Close();
        }
    }
    public void Close()
    {
        // This function was created to be used by one of the pause menu's UI buttons.
        closeMenu.Invoke();
        pauseIsOpen = false;
    }
}
