using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

public class DirectoryManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [MenuItem("NZTech/Setup Directories")]
    static void SetupFolders()
    {
        string rootDirectory = "Assets/";
        string parentDirectory = "";

        //Create Art Folders
        parentDirectory = "01_Art";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Materials"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Materials");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Models"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Models");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Textures"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Textures");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Prefabs"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Prefabs");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Shaders"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Shaders");

        //Create Animation Folders
        parentDirectory = "02_Animation";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Controllers"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Controllers");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Animations"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Animations");

        //Create Scripts Folders
        parentDirectory = "03_Scripts";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        CreateMainTS(rootDirectory + parentDirectory, "Main");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Character"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Character");

        CreateCharacterSpawnTS(rootDirectory + parentDirectory + "/Character", "BasicCharacterSpawn");
        CreateBasicTS(rootDirectory + parentDirectory + "/Character", "CharacterController");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Game Management"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Game Management");

        CreateBasicTS(rootDirectory + parentDirectory + "/Game Management", "GameManager");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/UI"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/UI");

        CreateBasicTS(rootDirectory + parentDirectory + "/UI", "UIManager");

        //Create Sound Folders
        parentDirectory = "04_Audio";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        if (!Directory.Exists(rootDirectory + parentDirectory + "/Music"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/Music");

        if (!Directory.Exists(rootDirectory + parentDirectory + "/SFX"))
            Directory.CreateDirectory(rootDirectory + parentDirectory + "/SFX");

        parentDirectory = "05_Scenes";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        parentDirectory = "Resources";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        parentDirectory = "Editor";
        if (!Directory.Exists(rootDirectory + parentDirectory))
            Directory.CreateDirectory(rootDirectory + parentDirectory);

        //Duplicate Assets
        parentDirectory = "02_Animation";
        if (!File.Exists(rootDirectory + parentDirectory + "/Controllers/ZepetoAnimatorV2.controller"))
        {
            File.Copy("Packages/zepeto.character.controller/Runtime/_Resources/AnimatorController/ZepetoAnimatorV2.controller", rootDirectory + parentDirectory + "/Controllers/ZepetoAnimatorV2.controller");
        }

        parentDirectory = "01_Art";
        if (!File.Exists(rootDirectory + parentDirectory + "/Prefabs/ZepetoCamera.prefab"))
        {
            File.Copy("Packages/zepeto.character.controller/Runtime/_Resources/Camera/ZepetoCamera.prefab", rootDirectory + parentDirectory + "/Prefabs/ZepetoCamera.prefab");
        }

        if (!File.Exists(rootDirectory + parentDirectory + "/Prefabs/UIController_Touchpad_Horizontal.prefab"))
        {
            File.Copy("Packages/zepeto.character.controller/Runtime/_Resources/Prefabs/UIController_Touchpad_Horizontal.prefab", rootDirectory + parentDirectory + "/Prefabs/UIController_Touchpad_Horizontal.prefab");
        }

        if (!File.Exists(rootDirectory + parentDirectory + "/Prefabs/UIController_Touchpad_Vertical.prefab"))
        {
            File.Copy("Packages/zepeto.character.controller/Runtime/_Resources/Prefabs/UIController_Touchpad_Vertical.prefab", rootDirectory + parentDirectory + "/Prefabs/UIController_Touchpad_Vertical.prefab");
        }

        if (!File.Exists(rootDirectory + parentDirectory + "/Prefabs/CharacterShadow.prefab"))
        {
            File.Copy("Packages/zepeto.character.controller/Runtime/_Resources/Prefabs/Shadow/CharacterShadow.prefab", rootDirectory + parentDirectory + "/Prefabs/CharacterShadow.prefab");
        }

        parentDirectory = "03_Scripts";
        if (!File.Exists(rootDirectory + parentDirectory + "/Character/ZepetoInputControl.inputactions"))
        {
            File.Copy("Packages/zepeto.character.controller/Runtime/_Resources/InputActions/ZepetoInputControl.inputactions", rootDirectory + parentDirectory + "/Character/ZepetoInputControl.inputactions");
        }

        AssetDatabase.Refresh();

        parentDirectory = "05_Scenes";
        if (!File.Exists(rootDirectory + parentDirectory + "/Main.unity"))
        {
            CreateEmptyScene(rootDirectory + parentDirectory, "Main.unity");
        }
    }

    static void CreateEmptyScene(string path, string name)
    {
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        EditorApplication.ExecuteMenuItem("GameObject/ZEPETO/ZepetoPlayers");

        Transform mainParent = new GameObject("Main").transform;
        new GameObject("CharacterController").transform.SetParent(mainParent);
        new GameObject("GameManager").transform.SetParent(mainParent);
        new GameObject("UIManager").transform.SetParent(mainParent);

        GameObject multiplay = new GameObject("Multiplay");
        multiplay.AddComponent<ZEPETO.World.ZepetoWorldMultiplay>();

        EditorApplication.ExecuteMenuItem("GameObject/3D Object/Plane");
        GameObject plane = GameObject.Find("Plane");
        plane.name = "Floor";
        plane.transform.localScale = Vector3.one * 5;
        plane.transform.position = Vector3.down;

        EditorApplication.ExecuteMenuItem("GameObject/Light/Directional Light");

        EditorSceneManager.SaveScene(newScene, path + "/" + name);
    }

    static void CreateBasicTS(string path, string name)
    {
        if (File.Exists(path + name + ".ts"))
            return;

        StreamWriter file = File.CreateText(path + "/" + name + ".ts");

        file.WriteLine("import { ZepetoScriptBehaviour } from 'ZEPETO.Script'");
        file.WriteLine("");
        file.WriteLine("export default class " + name + " extends ZepetoScriptBehaviour {");
        file.WriteLine("");
        file.WriteLine("    public Init()");
        file.WriteLine("    {");
        file.WriteLine("");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("}");
        file.Close();
    }

    static void CreateMainTS(string path, string name)
    {
        if (File.Exists(path + name + ".ts"))
            return;

        StreamWriter file = File.CreateText(path + "/" + name + ".ts");

        file.WriteLine("import { ZepetoScriptBehaviour } from 'ZEPETO.Script'");
        file.WriteLine("import { GameObject } from 'UnityEngine';");
        file.WriteLine("import CharacterController from './Character/CharacterController';");
        file.WriteLine("import GameManager from './Game Management/GameManager';");
        file.WriteLine("import UIManager from './UI/UIManager';");
        file.WriteLine("");
        file.WriteLine("export default class " + name + " extends ZepetoScriptBehaviour {");
        file.WriteLine("    public static instance: Main;");
        file.WriteLine("");
        file.WriteLine("    public characterController: CharacterController;");
        file.WriteLine("    public gameMgr: GameManager;");
        file.WriteLine("    public uiMgr: UIManager;");
        file.WriteLine("");
        file.WriteLine("    public static GetInstance(): Main");
        file.WriteLine("    {");
        file.WriteLine("        let gameObject = GameObject.Find(\"Main\");");
        file.WriteLine("        if (gameObject != null)");
        file.WriteLine("            return gameObject.GetComponent<Main>();");
        file.WriteLine("        else");
        file.WriteLine("            return new Main();");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    public Awake()");
        file.WriteLine("    {");
        file.WriteLine("        Main.instance = this;");
        file.WriteLine("        this.gameMgr = this.GetComponentInChildren<GameManager>();");
        file.WriteLine("        this.uiMgr = this.GetComponentInChildren<UIManager>();");
        file.WriteLine("        this.characterController = this.GetComponentInChildren<CharacterController>();");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    public Start()");
        file.WriteLine("    {");
        file.WriteLine("        this.InitializeAll();");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    public InitializeAll()  ");
        file.WriteLine("    {");
        file.WriteLine("        this.gameMgr.Init();");
        file.WriteLine("        this.uiMgr.Init();");
        file.WriteLine("        this.characterController.Init();");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("}");
        file.Close();
    }


    static void CreateCharacterSpawnTS(string path, string name)
    {
        if (File.Exists(path + name + ".ts"))
            return;

        StreamWriter file = File.CreateText(path + "/" + name + ".ts");

        file.WriteLine("import { ZepetoScriptBehaviour } from 'ZEPETO.Script'");
        file.WriteLine("import { SpawnInfo, ZepetoPlayers } from 'ZEPETO.Character.Controller'");
        file.WriteLine("import { WorldService } from 'ZEPETO.World'");
        file.WriteLine("");
        file.WriteLine("export default class " + name + " extends ZepetoScriptBehaviour {");
        file.WriteLine("");
        file.WriteLine("    Start() ");
        file.WriteLine("    {");
        file.WriteLine("        ZepetoPlayers.instance.CreatePlayerWithUserId(WorldService.userId, new SpawnInfo(), true);");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("}");
        file.Close();
    }
}
