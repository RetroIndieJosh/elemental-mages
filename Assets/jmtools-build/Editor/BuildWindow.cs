// This code is part of the JM Tools Build System library maintained by Joshua McLean (http://mrjoshuamclean.com)
// It is released for free under the MIT open source license (LICENSE.txt)

#if UNITY_EDITOR

namespace JoshuaMcLean.BuildSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class BuildWindow : EditorWindow
    {
        private const int BUILD_BUTTON_WIDTH = 75;

        readonly private BuildTarget[] m_supportedBuildTargets = {
            BuildTarget.StandaloneLinux,
            BuildTarget.StandaloneLinux64,
            BuildTarget.StandaloneOSX,
            BuildTarget.StandaloneWindows,
            BuildTarget.StandaloneWindows64
        };

        [SerializeField] private bool m_developmentBuild = false;
        [SerializeField] private bool m_allLowercase = false;
        [SerializeField] private string m_buildPath = "build";
        [SerializeField] private string m_butlerPath = "";
        [SerializeField] private string m_executableName = "";
        [SerializeField] private List<string> m_extraFileList = new List<string>();
        [SerializeField] private string m_itchChannel = "";
        [SerializeField] private string m_itchUserName = "";
        [SerializeField] private bool m_noWhitespace = false;
        [SerializeField] private List<Object> m_sceneList = new List<Object>();
        [SerializeField] private List<BuildTarget> m_selectedBuildTargetList = new List<BuildTarget>();

        public bool AllLowercase {
            get { return m_allLowercase; }
        }

        public string BuildDirectory {
            get { return m_buildPath; }
        }

        public string ButlerPath {
            get { return m_butlerPath; }
        }

        public string ExecutableName {
            get { return m_executableName; }
        }

        public string[] ExtraFileList {
            get { return m_extraFileList.ToArray(); }
        }

        public string ItchChannel {
            get { return m_itchChannel; }
        }

        public string ItchUserName {
            get { return m_itchUserName; }
        }

        public bool NoWhitespace {
            get { return m_noWhitespace; }
        }

        private void Build( BuildTarget a_buildTarget ) {
            if ( string.IsNullOrEmpty( m_buildPath ) ) {
                Debug.LogError( $"Build path cannot be empty. Abort build for {a_buildTarget}." );
                return;
            }

            var buildPath = GetBuildFilePathFor( a_buildTarget );

            var fileList = new List<string>();
            foreach ( var filePath in m_extraFileList )
                fileList.Add( ProcessPath( filePath ) );

            var scenePathList = new List<string>();
            var sceneSettingsList = new List<EditorBuildSettingsScene>();
            foreach ( var scene in m_sceneList ) {
                var scenePath = AssetDatabase.GetAssetPath( scene );
                scenePathList.Add( scenePath );

                var sceneSettings = new EditorBuildSettingsScene( scenePath, true );
                sceneSettingsList.Add( sceneSettings );
            }
            EditorBuildSettings.scenes = sceneSettingsList.ToArray();

            var buildOptions = BuildOptions.None;
            if ( m_developmentBuild ) buildOptions |= BuildOptions.AllowDebugging | BuildOptions.Development;
            BuildTools.Build( a_buildTarget, buildPath, scenePathList.ToArray(), fileList.ToArray(), buildOptions );
        }

        private bool BuildExists( BuildTarget a_target ) {
            var path = GetBuildDirPathFor( a_target );
            path = ProcessPath( path );
            return Directory.Exists( path );
        }

        private string GetBuildDirPathFor( BuildTarget a_target ) {
            var dir = $"{m_buildPath}/{GetBuildNameFor( a_target )}";
            return ProcessPath( dir );
        }

        private string GetBuildFilePathFor( BuildTarget a_target ) {
            var path = $"{GetBuildDirPathFor( a_target )}/{ExecutableName}";
            if ( a_target == BuildTarget.StandaloneWindows || a_target == BuildTarget.StandaloneWindows64 )
                path += ".exe";
            if ( a_target == BuildTarget.StandaloneOSX )
                path += ".app";
            return ProcessPath( path );
        }

        private string GetBuildNameFor( BuildTarget a_target ) {
            var platform = BuildTools.GetBuildTargetReadableName( a_target );
            var buildName = $"{Application.companyName} - {Application.productName} {Application.version} - {platform}";
            return ProcessPath( buildName );
        }

        private void GuiBuildOptions() {
            GUILayout.Label( "Build Options", EditorStyles.boldLabel );
            m_developmentBuild = GUILayout.Toggle( m_developmentBuild, "Development Build" );

            m_noWhitespace = GUILayout.Toggle( NoWhitespace,
                new GUIContent( "No Whitespace",
                    "Remove all whitespace from paths while creating build. Does not affect included Resources or " +
                    "game files." ) );
            m_allLowercase = GUILayout.Toggle( AllLowercase,
                new GUIContent( "All Lowercase",
                    "Convert paths to lowercase while creating build. Does not affect included Resources or game " +
                    "files." ) );
        }

        private void GuiBuildTargetEntry( BuildTarget a_buildTarget ) {
            var path = GetBuildDirPathFor( a_buildTarget );

            GUILayout.BeginHorizontal();

            var buildTargetPrev = m_selectedBuildTargetList.Contains( a_buildTarget );
            var label = BuildTools.GetBuildTargetReadableName( a_buildTarget );
            var buildTarget = GUILayout.Toggle( buildTargetPrev, label );
            if ( buildTarget != buildTargetPrev ) {
                if ( buildTarget ) m_selectedBuildTargetList.Add( a_buildTarget );
                else m_selectedBuildTargetList.Remove( a_buildTarget );
            }

            var layout = GUILayout.Width( BUILD_BUTTON_WIDTH );
            if ( GUILayout.Button( "Build", layout ) )
                Build( a_buildTarget );

            GUI.enabled = BuildExists( a_buildTarget );
#if JM_BUILD_ZIP
            if ( GUILayout.Button( "Zip", layout ) )
                Zip( a_buildTarget );
#endif 
            if ( GUILayout.Button( "Upload", layout ) )
                Upload( a_buildTarget );
            GUI.enabled = true;

            GUILayout.EndHorizontal();
        }

        private void GuiBuildTargets() {
            GUILayout.Label( "Build Targets", EditorStyles.boldLabel );

            var allSelectedPrev = m_supportedBuildTargets.Except( m_selectedBuildTargetList ).Count() == 0;
            var allSelected = GUILayout.Toggle( allSelectedPrev, "(all)" );
            if ( allSelected != allSelectedPrev ) {
                m_selectedBuildTargetList.Clear();
                if ( allSelected ) m_selectedBuildTargetList.AddRange( m_supportedBuildTargets );
            }

            foreach ( var buildTarget in m_supportedBuildTargets )
                GuiBuildTargetEntry( buildTarget );

            if ( m_selectedBuildTargetList.Count > 0 ) {
                GUILayout.Label( "Build Selected", EditorStyles.boldLabel );
                GUILayout.BeginHorizontal();
                if ( GUILayout.Button( "Build" ) ) {
                    foreach ( var target in m_selectedBuildTargetList )
                        Build( target );
                }

                foreach ( var target in m_selectedBuildTargetList ) {
                    if ( BuildExists( target ) == false ) {
                        GUI.enabled = false;
                        break;
                    }
                }

#if JM_BUILD_ZIP
                if ( GUILayout.Button( "Zip" ) ) {
                    foreach ( var target in m_selectedBuildTargetList )
                        Zip( target );
                }
#endif
                if ( GUILayout.Button( "Upload" ) ) {
                    foreach ( var target in m_selectedBuildTargetList )
                        Upload( target );
                }

                GUI.enabled = true;

                GUILayout.EndHorizontal();

                foreach ( var target in m_selectedBuildTargetList ) {
                    var path = GetBuildFilePathFor( target );

                    var absolutePath = Application.dataPath;
                    var len = absolutePath.Length - "/Assets".Length;
                    absolutePath = $"{absolutePath.Substring( 0, len )}/{path}/";

                    if ( BuildExists( target ) )
                        GuiLink( absolutePath, path );
                    else
                        GuiLink( null, Color.red, path );

                    var itchTarget = ItchTargetFor( target );
                    GUILayout.Label( $"---------> {itchTarget}" );
                }
            }
        }

        private void GuiFileList() {
            GUILayout.Label( "Extra Files", EditorStyles.boldLabel );

            GuiListEdit( m_extraFileList, "File", delegate ( string a_fileName ) {
                return File.Exists( a_fileName );
            } );

            if ( GUILayout.Button( "Add File" ) )
                m_extraFileList.Add( "New File" );
        }

        private void GuiGameInfo() {
            GUILayout.Label( "Game Info", EditorStyles.boldLabel );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Name" );
            GUILayout.Label( $"{Application.productName}" );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Version" );
            GUILayout.Label( $"{Application.version}" );
            GUILayout.EndHorizontal();

            GUILayout.Label( "(To change, go to Edit -> Project Settings -> Player)" );

            m_buildPath = EditorGUILayout.TextField( "Build Path (no ending /)", m_buildPath );
            m_executableName = EditorGUILayout.TextField( "Executable Name (no ext)", ExecutableName );
        }

        private void GuiItch() {
            GUILayout.Label( "itch.io", EditorStyles.boldLabel );

            m_butlerPath = EditorGUILayout.TextField(
                new GUIContent( "Butler Path", "Leave as \"butler\" if in your system path" ),
                m_butlerPath );
            m_itchUserName = EditorGUILayout.TextField(
                new GUIContent( "Username", "Your login name on itch.io (not your email)" ),
                m_itchUserName );
            m_itchChannel = EditorGUILayout.TextField(
                new GUIContent( "Channel", "The part of the game URL after itch.io/" ),
                m_itchChannel );

            GuiLink( $"https://{m_itchUserName}.itch.io/{m_itchChannel}" );
        }

        private void GuiLink( string a_url, Color a_color, string a_label = null ) {
            var linkStyle = new GUIStyle();
            linkStyle.SetAllColors( a_color );
            EditorGUILayout.LabelField( a_label ?? a_url, linkStyle );

            var rect = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );
            if ( string.IsNullOrEmpty( a_url ) ) return;

            if ( rect.Contains( Event.current.mousePosition ) && Event.current.type == EventType.MouseUp ) {
                if ( a_url.StartsWith( "http" ) || a_url.StartsWith( "www" ) )
                    Application.OpenURL( a_url );
                else
                    EditorUtility.RevealInFinder( a_url );
            }
        }

        private void GuiLink( string a_url, string a_label = null ) {
            GuiLink( a_url, Color.blue, a_label );
        }

        private void GuiListEdit( List<string> a_list, string a_label, System.Func<string, bool> a_isValidFunc = null ) {
            var toRemove = new List<int>();
            for ( var i = 0; i < a_list.Count; ++i ) {
                GUILayout.BeginHorizontal();
                var val = a_list[i];

                var fileFieldStyle = new GUIStyle( EditorStyles.textField );
                if ( a_isValidFunc != null ) {
                    if ( a_isValidFunc.Invoke( a_list[i] ) == false )
                        fileFieldStyle.SetAllColors( Color.red );
                }

                val = GUILayout.TextField( val, fileFieldStyle );
                a_list[i] = val;

                if ( GUILayout.Button( "Remove", GUILayout.Width( 75 ) ) )
                    toRemove.Add( i );

                GUILayout.EndHorizontal();
            }

            foreach ( var i in toRemove )
                a_list.RemoveAt( i );
        }

        private void GuiSceneList() {
            GUILayout.Label( "Scenes", EditorStyles.boldLabel );

            var toRemove = new List<int>();
            for ( var i = 0; i < m_sceneList.Count; ++i ) {
                GUILayout.BeginHorizontal();
                var val = m_sceneList[i];

                val = EditorGUILayout.ObjectField( val, typeof( Object ), false );
                if ( val is SceneAsset == false )
                    val = null;
                m_sceneList[i] = val;

                if ( GUILayout.Button( "Remove", GUILayout.Width( 75 ) ) )
                    toRemove.Add( i );

                GUILayout.EndHorizontal();
            }

            foreach ( var i in toRemove )
                m_sceneList.RemoveAt( i );

            /*
            GuiListEdit( m_sceneList, "Scene", delegate(string a_scenePath ) {
                return File.Exists( $"{Application.dataPath}/{a_scenePath}.unity" );
            } );
            */

            if ( GUILayout.Button( "Add Scene" ) )
                m_sceneList.Add( null );
        }

        private const string SETTINGS_DIR = "Assets/jm-tools-settings";
        private const string SETTINGS_FILE_NAME = "BuildSettings.asset";
        private string SettingsPath {
            get {
                return $"{SETTINGS_DIR}/{SETTINGS_FILE_NAME}";
            }
        }

        private void OnLostFocus() {
            Save();
        }

        private void OnDisable() {
            Save();
        }

        private void OnFocus() {
            Save();
        }

        public void Save() {
            var data = JsonUtility.ToJson( this );
            var asset = new TextAsset( data );
            if ( Directory.Exists( SETTINGS_DIR ) == false )
                Directory.CreateDirectory( SETTINGS_DIR );
            //Debug.Log( $"Save settings to {SettingsPath}" );
            AssetDatabase.CreateAsset( asset, SettingsPath );
            AssetDatabase.SaveAssets();
        }

        private void OnEnable() {
            var data = AssetDatabase.LoadAssetAtPath<TextAsset>( SettingsPath );
            if ( data == null ) return;
            JsonUtility.FromJsonOverwrite( data.text, this );
        }

        private Vector2 m_scrollPos = Vector2.zero;

        private void OnGUI() {
            m_scrollPos = GUILayout.BeginScrollView( m_scrollPos, false, false );
            GuiGameInfo();
            GuiItch();

            GuiSceneList();
            GuiFileList();

            GuiBuildOptions();
            GuiBuildTargets();
            GUILayout.EndScrollView();
        }

        private void OnInspectorUpdate() {
            Repaint();
        }

        private string ProcessPath( string a_path ) {
            if ( m_noWhitespace ) a_path = a_path.Replace( " ", "" );
            if ( m_allLowercase ) a_path = a_path.ToLower();
            return a_path;
        }

        private string ItchNameFor( BuildTarget a_buildTarget ) {
            switch( a_buildTarget) {
                case BuildTarget.StandaloneLinux: return "linux";
                case BuildTarget.StandaloneLinux64: return "linux-64";

                case BuildTarget.StandaloneOSX: return "osx";

                case BuildTarget.StandaloneWindows: return "windows";
                case BuildTarget.StandaloneWindows64: return "windows-64";

                default: return "";
            }
        }

        private string ItchTargetFor( BuildTarget a_buildTarget ) {
            var system = ItchNameFor( a_buildTarget );
            return $"{ItchUserName}/{ItchChannel}:{system}";
        }

        private void Upload( BuildTarget a_buildTarget ) {
            var path = GetBuildDirPathFor( a_buildTarget );

            var system = a_buildTarget.ToString().ToLower().Replace( " ", "-" );
            var itchTarget = $"{ItchUserName}/{ItchChannel}:{system}";

            BuildTools.Upload( a_buildTarget, path, itchTarget );
        }

#if JM_BUILD_ZIP
        private void Zip( BuildTarget a_buildTarget ) {
            var path = GetBuildDirPathFor( a_buildTarget );

            var zipPath = $"{path}/{GetBuildNameFor( a_buildTarget )}.zip";
            zipPath = ProcessPath( zipPath );

            BuildTools.Zip( a_buildTarget, path, zipPath );
        }
#endif 

        [MenuItem( "Window/JM Tools/Build Window" )]
        static public void ShowWindow() {
            var window = GetWindow( typeof( BuildWindow ), false, "Build (JM Tools)" );
            window.minSize = new Vector2( 350, 100 );
        }
    }
}

#endif // UNITY_EDITOR
