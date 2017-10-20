using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public enum InteractionType
    {
        Monologue,
        StoryConvo,
        MiscConvo,
        MiscNPC
    }
    public enum DialogueTypes
    {
        Conversation,
        Observation,
        Instruction,
        Misc
    }

    Dictionary<InteractionType, XDocument> m_DialogueData = new Dictionary<InteractionType, XDocument>();
    private const string M_DIALOGUE_PATH = "Dialogue/";

    void Awake()
    {
        SceneManager.sceneLoaded += LoadDialogue;
    }

    void LoadDialogue(Scene scene, LoadSceneMode mode)
    {
        m_DialogueData.Clear();
        m_DialogueData.Add(InteractionType.Monologue, XMLManager.LoadXml(M_DIALOGUE_PATH + scene.buildIndex + "/Monologue.xml"));
        m_DialogueData.Add(InteractionType.StoryConvo, XMLManager.LoadXml(M_DIALOGUE_PATH + scene.buildIndex + "/StoryConvo.xml"));
        m_DialogueData.Add(InteractionType.MiscConvo, XMLManager.LoadXml(M_DIALOGUE_PATH + scene.buildIndex + "/MiscConvo.xml"));
        m_DialogueData.Add(InteractionType.MiscNPC, XMLManager.LoadXml(M_DIALOGUE_PATH + scene.buildIndex + "/MiscNPC.xml"));
        StartDialogue(InteractionType.StoryConvo, "Bruce", DialogueTypes.Conversation, 1);
    }

    public XElement GetNPCType(InteractionType interactionType, string npcType)
    {
        XDocument xDoc;
        if(!m_DialogueData.TryGetValue(interactionType, out xDoc))
        {
            Debug.LogError("DialogueManager: GetNPCType: Failed to get value!");
            return null;
        }
        XElement root = xDoc.Root;
        List<XElement> elements = root.Elements().ToList();
        foreach (XElement e in elements)
        {
            if (e.Attribute("Type").Value == npcType)
            {
                return e;
            }
        }
        Debug.LogError("DialogueManager: GetNPCType: Failed to get npc!");
        return null;
    }

    public void StartDialogue(InteractionType interactionType,  string npcType, DialogueTypes dialogueType, int id)
    {
        XElement element = GetNPCType(interactionType, npcType);
        List<XElement> elements = element.Elements().ToList();
        foreach (XElement e in elements)
        {
            if (e.Attribute("Type").Value == dialogueType.ToString())
            {
                switch (dialogueType)
                {
                    case DialogueTypes.Conversation:
                        StartConvo(e, id);
                        return;
                    case DialogueTypes.Instruction:
                        DisplayDialogue(e, id);
                        return;
                    case DialogueTypes.Observation:
                        DisplayDialogue(e, id);
                        return;
                    case DialogueTypes.Misc:
                        DisplayDialogue(e, id);
                        return;
                }
            }

        }
    }

    private void DisplayDialogue(XElement ele, int id)
    {
        foreach (XElement e in ele.Elements())
        {
            if(int.Parse(e.Attribute("id").Value) == id )
            {
                string dialogue = e.Value;
                string dialogueTrimmed = dialogue.Trim();
                Debug.Log(dialogueTrimmed);
                return;
            }
        }
    }

    private void StartConvo(XElement ele, int id)
    {
        foreach (XElement e in ele.Elements())
        {
            if (int.Parse(e.Attribute("id").Value) == id)
            {
                StartCoroutine(Conversation(e));
                return;
            }
        }
    }

    private IEnumerator Conversation(XElement ele)
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

        int convoIndex = 0;
        foreach(XElement e in ele.Elements())
        {
            string text = e.Value.Trim();

            foreach (char c in text)
            {
                yield return waitFrame;
                //Do text animation
            }
            Debug.Log(text);
            convoIndex++;
            if (convoIndex >= ele.Elements().Count())
                break;
            yield return waitFrame;
            while (!Input.GetMouseButton(0))
            {
                yield return waitFrame;
            }
        }
    }
}
