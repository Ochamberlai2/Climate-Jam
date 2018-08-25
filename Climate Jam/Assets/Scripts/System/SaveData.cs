using System;
using System.Collections.Generic;

public class SaveData {

    public SaveDataRegion[] regions;
    public float money;
    public float years_Passed;
    public DateTime date_Last_Opened;

}

public class SaveDataRegion
{
    public int GHG;
    public Region region;
    public List<Entry> upgrades = new List<Entry>();
}

public class Entry
{
    public Entry()
    {

    }

    public Entry(int _key, int _value)
    {
        key = _key;
        value = _value;
    }

    public int key;
    public int value;
}
