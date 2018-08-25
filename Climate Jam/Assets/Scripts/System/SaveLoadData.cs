using System;
using System.IO;
using System.Xml.Serialization;

public static class SaveLoadData
{



    public static void SaveGameData()
    {
        SaveData newSaveData = new SaveData();

        newSaveData.date_Last_Opened = DateTime.UtcNow;
        newSaveData.money = GlobalBlackboard.Instance.money.current_Money;
        newSaveData.years_Passed = GlobalBlackboard.Instance.time.num_Years_Passed;
        //newSaveData.regions = GlobalBlackboard.Instance.regions.ToArray();

        newSaveData.regions = new SaveDataRegion[GlobalBlackboard.Instance.regions.Count];

        //save the region info
        for(int i = 0; i < newSaveData.regions.Length; i++)
        {
            newSaveData.regions[i] = new SaveDataRegion();
            newSaveData.regions[i].GHG = GlobalBlackboard.Instance.regions[i].GHG_Level;
            newSaveData.regions[i].region = GlobalBlackboard.Instance.regions[i].GetRegion();
            for(int j = 0; j < GlobalBlackboard.Instance.regions[i].region_Upgrades.Count; j++)
            {
                newSaveData.regions[i].upgrades.Add(new Entry(j, GlobalBlackboard.Instance.regions[i].region_Upgrades[j].upgrade_Level));

            }
        }


        //serialise the data
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        using (TextWriter writer = new StreamWriter(GetAppdataPath("GameSave.xml")))
        {
            serializer.Serialize(writer, newSaveData);
        }

    }

    public static void LoadGameData()
    {

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));

        StreamReader reader = new StreamReader(GetAppdataPath("GameSave.xml"));

        SaveData saveData = xmlSerializer.Deserialize(reader) as SaveData;

        reader.Close();

        GlobalBlackboard.Instance.time.num_Years_Passed = saveData.years_Passed;
        GlobalBlackboard.Instance.time.date_Last_Opened = saveData.date_Last_Opened;
        GlobalBlackboard.Instance.money.current_Money = saveData.money;
        for(int i = 0; i < saveData.regions.Length; i++)
        {
            GlobalBlackboard.Instance.regions[i].GHG_Level = saveData.regions[i].GHG;
            for(int j = 0; j < saveData.regions[i].upgrades.Count; j++)
            {
                GlobalBlackboard.Instance.regions[i].region_Upgrades[j].upgrade_Level = saveData.regions[i].upgrades[j].value;
            }

        }



    }


    /// <summary>
    /// Get the filepath for appdata and append the file name to it
    /// </summary>
    /// <param name="fileName"></param>
    private static string GetAppdataPath(string fileName)
    {

        //get appdata path
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        appdata = Path.Combine(appdata, "ClimateJam");
        Directory.CreateDirectory(appdata);
        //combine the path with the filename
        return Path.Combine(appdata, fileName);
    }
    

	
}
