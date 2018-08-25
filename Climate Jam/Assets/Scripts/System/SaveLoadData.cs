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

        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        using (TextWriter writer = new StreamWriter(GetAppdataPath("GameSave.xml")))
        {
            serializer.Serialize(writer, newSaveData);
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
