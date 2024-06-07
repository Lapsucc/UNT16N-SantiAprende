using UnityEngine;
// TUTO pete

public class ProfileData
{
    public string filename;
    public string name;
    public int inter1;
    

    public ProfileData()
    {        
        this.filename = "None.xml";
        this.name = "None";
    }

    public ProfileData(string name)
    {
        this.name = name;        
        this.filename = name.Replace(" ", " ") + ".xml";
        
    }
}
