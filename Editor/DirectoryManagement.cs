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

        EditorUtility.DisplayDialog("Setup Complete!", "Open Scenes > Main and attach the relevant scripts to the objects you see there. (Main, Character Controller, GameManager, UIManager, ClientStarter etc..) \n\nFor your Client Starter, please connect the multiplay object as well in its inspector. \n\nLet's create a world!!", "Lets go!");
    }

    [MenuItem("NZTech/Initialize Multiplayer Scripts")]
    static void UpdateWithMultiplayerScripts()
    {
        string rootDirectory = "Assets/";
        string parentDirectory = "";

        if (!Directory.Exists(rootDirectory + "World.multiplay"))
        {
            EditorApplication.ExecuteMenuItem("Assets/Create/ZEPETO/Multiplay Server");
            EditorUtility.DisplayDialog("Multiplay Object Didn't Exist. Created one for you.", "If you already have a multiplay object, please rename your multiplayer object \"World\" and run the command again.", "Got it!");
            return;
        }

        parentDirectory = "03_Scripts";
        //Create Multiplayer Object
        if (!Directory.Exists(rootDirectory + parentDirectory))
        {
            EditorUtility.DisplayDialog("Please Initialize Directories First", "Go to NZTech -> Setup Direcotires", "Got it!");
            return;
        }


        //Initialize Multiplayer Object
        if (Directory.Exists(rootDirectory + "World.multiplay"))
        {
            CreateClientStarterTS(rootDirectory + parentDirectory + "/Game Management", "ClientStarter");

            parentDirectory = "World.multiplay";

            CreateMultiplaySchemaTS(rootDirectory + parentDirectory, "schemas");
            CreateMultiplayServerTS(rootDirectory + parentDirectory, "index");
            AssetDatabase.Refresh();
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
        new GameObject("ClientStarter").transform.SetParent(mainParent);

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

    static void CreateMultiplayServerTS(string path, string name)
    {
        File.WriteAllText(path + "/" + name + ".ts", "");
        StreamWriter file = File.CreateText(path + "/" + name + ".ts");

        file.WriteLine("import { Sandbox, SandboxOptions, SandboxPlayer } from \"ZEPETO.Multiplay\";");
        file.WriteLine("import { Player, Vector3Schema } from \"ZEPETO.Multiplay.Schema\";");
        file.WriteLine("");
        file.WriteLine("enum MultiplayMessageType {");
        file.WriteLine("");
        file.WriteLine("    // When position is synced");
        file.WriteLine("    CharacterTransform = \"CharacterTransform\",");
        file.WriteLine("");
        file.WriteLine("    // For Animation states");
        file.WriteLine("    CharacterState = \"CharacterState\",");
        file.WriteLine("}");
        file.WriteLine("");
        file.WriteLine("//Transform position data");
        file.WriteLine("type MultiplayMessageCharacterTransform = {");
        file.WriteLine("    positionX: number,");
        file.WriteLine("    positionY: number,");
        file.WriteLine("    positionZ: number,");
        file.WriteLine("}");
        file.WriteLine("");
        file.WriteLine("//Character state data");
        file.WriteLine("type MultiplayMessageCharacterState = {");
        file.WriteLine("");
        file.WriteLine("    //state id number for translation to enum. ");
        file.WriteLine("    characterState: number");
        file.WriteLine("}");
        file.WriteLine("");
        file.WriteLine("export default class extends Sandbox {");
        file.WriteLine("    onCreate(options: SandboxOptions) {");
        file.WriteLine("        // Position Sync Message");
        file.WriteLine("        this.onMessage(MultiplayMessageType.CharacterTransform, (client, message: MultiplayMessageCharacterTransform) => {");
        file.WriteLine("            // Only continue if the player exists based on the userId");
        file.WriteLine("            const userId = client.userId;");
        file.WriteLine("            if (!this.state.players.has(userId)) return;");
        file.WriteLine("");
        file.WriteLine("            // Grab the player based on userId");
        file.WriteLine("            const player = this.state.players.get(userId);");
        file.WriteLine("            // Sync Position Data");
        file.WriteLine("            const position = new Vector3Schema();");
        file.WriteLine("            position.x = message.positionX;");
        file.WriteLine("            position.y = message.positionY;");
        file.WriteLine("            position.z = message.positionZ;");
        file.WriteLine("        });");
        file.WriteLine("");
        file.WriteLine("        // Character State (Jumping, running etc) sync message");
        file.WriteLine("        this.onMessage(MultiplayMessageType.CharacterState, (client, message: MultiplayMessageCharacterState) => {");
        file.WriteLine("            const player = this.state.players.get(client.userId);");
        file.WriteLine("            player.characterState = message.characterState;");
        file.WriteLine("        });");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    onJoin(client: SandboxPlayer) {");
        file.WriteLine("        const userId = client.userId;");
        file.WriteLine("        const player = new Player();");
        file.WriteLine("");
        file.WriteLine("        // Apply the schema userID value to the player object. ");
        file.WriteLine("        player.userId = userId;");
        file.WriteLine("");
        file.WriteLine("        // Apply the schema's position data to our copy");
        file.WriteLine("        player.position = new Vector3Schema();");
        file.WriteLine("");
        file.WriteLine("        // Reset position to (0,0,0)");
        file.WriteLine("        player.position.x = 0;");
        file.WriteLine("        player.position.y = 0;");
        file.WriteLine("        player.position.z = 0;");
        file.WriteLine("");
        file.WriteLine("        //Cache our player to the map. ");
        file.WriteLine("        this.state.players.set(userId, player);");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    onLeave(client: SandboxPlayer, consented?: boolean) {");
        file.WriteLine("        // Delete the player data");
        file.WriteLine("        this.state.players.delete(client.userId);");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("}");

        file.Close();
    }

    static void CreateMultiplaySchemaTS(string path, string name)
    {
        File.WriteAllText(path + "/" + name + ".json", "");
        StreamWriter file = File.CreateText(path + "/" + name + ".json");

        file.WriteLine("{");
        file.WriteLine("\"State\" : {\"players\" : {\"map\" : \"Player\"}},");
        file.WriteLine("\"Vector3Schema\" : {\"x\" : \"number\",\"y\" : \"number\",\"z\" : \"number\"},");
        file.WriteLine("\"Player\" : {\"userId\" : \"string\",\"characterState\" : \"number\",\"position\" : \"Vector3Schema\",\"sessionId\" : \"string\"}");
        file.WriteLine("}");

        file.Close();
    }

    static void CreateClientStarterTS(string path, string name)
    {
        if (File.Exists(path + name + ".ts"))
            return;

        StreamWriter file = File.CreateText(path + "/" + name + ".ts");

        file.WriteLine("import { GameObject, Quaternion, Vector3, WaitForSeconds, Transform } from 'UnityEngine';");
        file.WriteLine("import { CharacterState, SpawnInfo, ZepetoCharacter, ZepetoPlayers } from 'ZEPETO.Character.Controller';");
        file.WriteLine("import { Room } from 'ZEPETO.Multiplay';");
        file.WriteLine("import { Player, State } from 'ZEPETO.Multiplay.Schema';");
        file.WriteLine("import { ZepetoScriptBehaviour } from 'ZEPETO.Script'");
        file.WriteLine("import { WorldService, ZepetoWorldMultiplay } from 'ZEPETO.World';");
        file.WriteLine("");
        file.WriteLine("export enum MultiplayMessageType {");
        file.WriteLine("");
        file.WriteLine("    // When position is synced");
        file.WriteLine("    CharacterTransform = \"CharacterTransform\",");
        file.WriteLine("");
        file.WriteLine("    // For Animation states");
        file.WriteLine("    CharacterState = \"CharacterState\",");
        file.WriteLine("}");
        file.WriteLine("");
        file.WriteLine("//Transform position data");
        file.WriteLine("export type MultiplayMessageCharacterTransform = {");
        file.WriteLine("    positionX: number,");
        file.WriteLine("    positionY: number,");
        file.WriteLine("    positionZ: number,");
        file.WriteLine("}");
        file.WriteLine("");
        file.WriteLine("//Character state data");
        file.WriteLine("export type MultiplayMessageCharacterState = {");
        file.WriteLine("");
        file.WriteLine("    //state id number for translation to enum. ");
        file.WriteLine("    characterState: number");
        file.WriteLine("}");
        file.WriteLine("");
        file.WriteLine("export default class ClientScript extends ZepetoScriptBehaviour {");
        file.WriteLine("    private static instance: ClientScript;");
        file.WriteLine("");
        file.WriteLine("    static GetInstance(): ClientScript {");
        file.WriteLine("        if (!ClientScript.instance) {");
        file.WriteLine("            const targetObj = GameObject.Find(\"Client\");");
        file.WriteLine("            if (targetObj) ClientScript.instance = targetObj.GetComponent<ClientScript>();");
        file.WriteLine("        }");
        file.WriteLine("        return ClientScript.instance;");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    public multiplay: ZepetoWorldMultiplay;");
        file.WriteLine("");
        file.WriteLine("    public multiplayRoom: Room;");
        file.WriteLine("");
        file.WriteLine("    //Map of the players coming from the multiplay server. ");
        file.WriteLine("    private multiplayPlayers: Map<string, Player> = new Map<string, Player>();");
        file.WriteLine("");
        file.WriteLine("    public spawnLocation: GameObject;");
        file.WriteLine("");
        file.WriteLine("    public objZepetoPlayers: ZepetoPlayers;");
        file.WriteLine("");
        file.WriteLine("    Start() { ");
        file.WriteLine("        //Cache the room in the Callback when the server creates a room object. ");
        file.WriteLine("        this.multiplay.RoomCreated += (room: Room) => {");
        file.WriteLine("            this.multiplayRoom = room;");
        file.WriteLine("        };");
        file.WriteLine("");
        file.WriteLine("        //Callback for when the room is joined. ");
        file.WriteLine("        this.multiplay.RoomJoined += (room: Room) => {");
        file.WriteLine("            //Called each time the room state variables are altered");
        file.WriteLine("            room.OnStateChange += this.OnStateChange;");
        file.WriteLine("        }");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private OnStateChange(state: State, isFirst: boolean) {");
        file.WriteLine("        // Called for the first state change only");
        file.WriteLine("        if (isFirst) {");
        file.WriteLine("            // Apply sync logic for player if they already exist. ");
        file.WriteLine("            state.players.ForEach((userId, player) => { this.OnPlayerAdd(player, userId) });");
        file.WriteLine("");
        file.WriteLine("            // Register Player Add/Remove events ");
        file.WriteLine("            state.players.add_OnAdd((player, userId) => { this.OnPlayerAdd(player, userId) });");
        file.WriteLine("            state.players.add_OnRemove((player, userId) => { this.OnPlayerRemove(player, userId) });");
        file.WriteLine("");
        file.WriteLine("            this.InitializeCharacter(state);");
        file.WriteLine("        }");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private OnPlayerAdd(player: Player, userId: string) {");
        file.WriteLine("        if (this.multiplayPlayers.has(userId)) return;");
        file.WriteLine("");
        file.WriteLine("        // Cache the player to our map ");
        file.WriteLine("        this.multiplayPlayers.set(userId, player);");
        file.WriteLine("");
        file.WriteLine("        //Create spawn info for our new character. ");
        file.WriteLine("        const spawnInfo = new SpawnInfo();");
        file.WriteLine("        const position = this.spawnLocation.transform.position;");
        file.WriteLine("        spawnInfo.position = position;");
        file.WriteLine("        spawnInfo.rotation = Quaternion.identity;");
        file.WriteLine("");
        file.WriteLine("        // If the added player id matches the world service id, we know this is the local player. ");
        file.WriteLine("        const isLocal = WorldService.userId === userId;");
        file.WriteLine("");
        file.WriteLine("        // Instantiate character with the above settings. ");
        file.WriteLine("        ZepetoPlayers.instance.CreatePlayerWithUserId(userId, userId, spawnInfo, isLocal);");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private OnPlayerRemove(player: Player, userId: string) {");
        file.WriteLine("        if (!this.multiplayPlayers.has(userId)) return;");
        file.WriteLine("        ZepetoPlayers.instance.RemovePlayer(userId);");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private InitializeCharacter(state: State) {");
        file.WriteLine("        // Callback when the localplayer is fully loaded into the scene.");
        file.WriteLine("        ZepetoPlayers.instance.OnAddedLocalPlayer.AddListener(() => {");
        file.WriteLine("            // cache the player and userIds");
        file.WriteLine("            const zepetoPlayer = ZepetoPlayers.instance.LocalPlayer.zepetoPlayer;");
        file.WriteLine("            const userId = WorldService.userId;");
        file.WriteLine("");
        file.WriteLine("            zepetoPlayer.character.gameObject.layer = 5;");
        file.WriteLine("");
        file.WriteLine("            // Change the character's name to the userID");
        file.WriteLine("            zepetoPlayer.character.name = userId;");
        file.WriteLine("");
        file.WriteLine("            // Send a message to the server every time the character state is altered. ");
        file.WriteLine("            zepetoPlayer.character.OnChangedState.AddListener((current, previous) => {");
        file.WriteLine("                this.SendMessageCharacterState(current);");
        file.WriteLine("            });");
        file.WriteLine("");
        file.WriteLine("            // Check the character transform positions every 0.1 seconds and update. ");
        file.WriteLine("            this.StartCoroutine(this.SendMessageCharacterTransformLoop(0.1));");
        file.WriteLine("        });");
        file.WriteLine("");
        file.WriteLine("        // Callback when the player is fully loaded into the scene. ");
        file.WriteLine("        ZepetoPlayers.instance.OnAddedPlayer.AddListener((userId: string) => {");
        file.WriteLine("            //Cache the player by userId");
        file.WriteLine("            const zepetoPlayer = ZepetoPlayers.instance.GetPlayer(userId);");
        file.WriteLine("");
        file.WriteLine("            // Set the character object's name to the userId");
        file.WriteLine("            zepetoPlayer.character.name = userId;");
        file.WriteLine("");
        file.WriteLine("            //Grab the player instance from the server schema map based on the userId");
        file.WriteLine("            const player: Player = this.multiplayRoom.State.players.get_Item(userId);");
        file.WriteLine("");
        file.WriteLine("            // Add to the OnChange Schema Type Callback Message");
        file.WriteLine("            player.position.OnChange += () => {");
        file.WriteLine("");
        file.WriteLine("                // Only sync for everyone but the local player");
        file.WriteLine("                if (zepetoPlayer.isLocalPlayer == false) {");
        file.WriteLine("");
        file.WriteLine("                    // Cache the postion values. ");
        file.WriteLine("                    const x = player.position.x;");
        file.WriteLine("                    const y = player.position.y;");
        file.WriteLine("                    const z = player.position.z;");
        file.WriteLine("                    const position = new Vector3(x, y, z);");
        file.WriteLine("");
        file.WriteLine("                    // Directly apply the server position if the position deviates past a certain range (Handle sync issues)");
        file.WriteLine("                    if (Vector3.Distance(position, zepetoPlayer.character.transform.position) > 7) {");
        file.WriteLine("                        zepetoPlayer.character.transform.position = position;");
        file.WriteLine("                    }");
        file.WriteLine("");
        file.WriteLine("                    // Move the character to the target position. ");
        file.WriteLine("                    zepetoPlayer.character.MoveToPosition(position);");
        file.WriteLine("");
        file.WriteLine("                    //Jump if the character state has changed to jump. ");
        file.WriteLine("                    if (player.characterState === CharacterState.JumpIdle || player.characterState === CharacterState.JumpMove)");
        file.WriteLine("                        zepetoPlayer.character.Jump();");
        file.WriteLine("                }");
        file.WriteLine("            }");
        file.WriteLine("        });");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private SendMessageCharacterState(characterState: CharacterState) {");
        file.WriteLine("        // Create the character state message body. ");
        file.WriteLine("        const message: MultiplayMessageCharacterState = {");
        file.WriteLine("            characterState: characterState");
        file.WriteLine("        }");
        file.WriteLine("");
        file.WriteLine("        // Send the character state. ");
        file.WriteLine("        this.multiplayRoom.Send(MultiplayMessageType.CharacterState, message);");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private *SendMessageCharacterTransformLoop(tick: number) {");
        file.WriteLine("        while (true) {");
        file.WriteLine("");
        file.WriteLine("            // Wait For the designated amount of time (tick)");
        file.WriteLine("            yield new WaitForSeconds(tick);");
        file.WriteLine("");
        file.WriteLine("            // Only run if the multiplay room instance exists and the room is connected. ");
        file.WriteLine("            if (this.multiplayRoom != null && this.multiplayRoom.IsConnected) {");
        file.WriteLine("");
        file.WriteLine("                // Cache the userId. ");
        file.WriteLine("                const userId = WorldService.userId;");
        file.WriteLine("");
        file.WriteLine("                // Only run if the player exists in the zepeto players map. ");
        file.WriteLine("                if (ZepetoPlayers.instance.HasPlayer(userId)) {");
        file.WriteLine("");
        file.WriteLine("                    //Cache the character controller. ");
        file.WriteLine("                    const character = ZepetoPlayers.instance.GetPlayer(userId).character;");
        file.WriteLine("");
        file.WriteLine("                    // Send the character transform update message if not idling. (Send when character moves/jumps)");
        file.WriteLine("                    if (character.CurrentState != CharacterState.Idle)");
        file.WriteLine("                        this.SendMessageCharacterTransform(character.transform);");
        file.WriteLine("                }");
        file.WriteLine("            }");
        file.WriteLine("        }");
        file.WriteLine("    }");
        file.WriteLine("");
        file.WriteLine("    private SendMessageCharacterTransform(transform: Transform) {");
        file.WriteLine("        //Cache the local transform position. ");
        file.WriteLine("        const position = transform.localPosition;");
        file.WriteLine("");
        file.WriteLine("        // Create the message body ");
        file.WriteLine("        const message: MultiplayMessageCharacterTransform = {");
        file.WriteLine("            positionX: position.x,");
        file.WriteLine("            positionY: position.y,");
        file.WriteLine("            positionZ: position.z");
        file.WriteLine("        }");
        file.WriteLine("");
        file.WriteLine("        // Send the message to the server. ");
        file.WriteLine("        this.multiplayRoom.Send(MultiplayMessageType.CharacterTransform, message);");
        file.WriteLine("    }");
        file.WriteLine("}");

        file.Close();
    }
}
