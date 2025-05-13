using System;
using System.Collections.Generic;

[System.Serializable]
public struct MatchFrameData
{
    public int FrameCount;
    //Currently not important will skip for now
    public DateTime TimestampUTC;
    public List<Person> Persons;
}
