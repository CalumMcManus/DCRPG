using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using UnityEngine;

public static class XMLManager {

    static void ReadXml() { }
    public static XDocument LoadXml(string path)
    {
        TextAsset textXml;

        XDocument xDoc = new XDocument();

        if (File.Exists(getPath(path)))
        {
            xDoc = XDocument.Parse(File.ReadAllText(getPath(path)));
        }

        else
        {
            textXml = (TextAsset)Resources.Load(path, typeof(TextAsset));
            Debug.Log(path);
            xDoc = XDocument.Parse(textXml.text);
        }

        return xDoc;
    }

    public static XElement GetNPCType(string path, string type)
    {
        XDocument xDoc = LoadXml(path);
        XElement root = xDoc.Root;
        List<XElement> elements = root.Elements().ToList();
        foreach (XElement e in elements)
        {
            if (e.Attribute("Type").Value == type)
            {
                Debug.Log(e.Value);
            } 
        }
        return root;
    }

    #region XML Utilities
    static private string getPath(string fileName)
    {
        #if UNITY_EDITOR
            return Application.dataPath + "/Resources/" + fileName;
        #else
            return Application.dataPath +"/"+ fileName;
        #endif
    }
    #endregion
    
}
