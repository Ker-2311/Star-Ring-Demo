using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeStream
{
    public Action action;
    public int remainTime;
    public int originTime;
    //ִ�д���
    public int runCount;

    public GameTimeStream(Action func,int gameTime,int count)
    {
        runCount = count;
        action = func;
        remainTime = gameTime;
        originTime = gameTime;
    }
}
