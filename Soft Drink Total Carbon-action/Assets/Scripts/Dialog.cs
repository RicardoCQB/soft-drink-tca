﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{

    public string speakerName;

    [TextArea(3,20)]
    public string[] sentences;
}
