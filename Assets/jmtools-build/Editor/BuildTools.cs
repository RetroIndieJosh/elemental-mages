// This code is part of the JM Tools Build System library maintained by Joshua McLean (http://mrjoshuamclean.com)
// It is released for free under the MIT open source license (LICENSE.txt)

#if UNITY_EDITOR

namespace JoshuaMcLean.BuildSystem
{
    using System.IO;
#if JM_BUILD_ZIP
    using System.IO.Compression;
#endif
    using UnityEditor;
    using UnityEngine;

    public class BuildTools
    {
        public static void Build( BuildTarget a_target, string a_path, string[] a_scenes, BuildOptions a_options ) {
            Build( a_target, a_path, a_scenes, null, a_options );
        }

        public static void Build( BuildTarget a_target, string a_path, string[] a_scenes, string[] a_extraFiles = null,
            BuildOptions a_options = BuildOptions.None ) {
            /*
            if ( string.IsNullOrEmpty( buildWindow.ExecutableName ) ) {
                Debug.LogError( $"Executable name must be set in Build window." );
                return;
            }

            if ( CheckFile( buildWindow.ExecutableName ) == false ) return;

            if ( a_path.Contains( "." ) ) {
                Debug.LogError(
                    $"Executable name cannot include periods. Don't worry, we'll add the .exe for Windows builds." );
                return;
            }
            */

            Debug.Log( $"Building for {a_target} with {a_scenes.Length} scenes to {a_path}" );
            BuildPipeline.BuildPlayer( a_scenes, a_path, a_target, a_options );

            if ( a_extraFiles == null ) return;
            Debug.Log( $"Copying {a_extraFiles.Length} extra files to {a_path}" );
            CopyFiles( a_path, a_extraFiles );
        }

        public static bool CheckFile( string a_filePath ) {
            var invalidChars = Path.GetInvalidPathChars();
            foreach ( var ch in invalidChars ) {
                if ( a_filePath.Contains( "" + ch ) ) {
                    Debug.LogError( $"Filename cannot contain {ch.ToString().ToLiteral()} [{a_filePath}]" );
                    return false;
                }
            }
            return true;
        }

        public static void Upload( BuildTarget a_system, string a_buildPath, string a_itchTarget ) {
            if ( Directory.Exists( a_buildPath ) == false ) {
                Debug.LogError( $"Directory for {a_system} build does not exist ({a_buildPath}). Aborting upload." );
                return;
            }

            ButlerInterface.Push( $"{a_buildPath} {a_itchTarget} --userversion {Application.version}" );
        }

#if JM_BUILD_ZIP
        public static void Zip( BuildTarget a_target, string a_buildPath, string a_zipPath ) {
            if ( Directory.Exists( a_buildPath ) == false ) {
                Debug.LogError( $"Directory [{a_buildPath}] doesn't exist; aborting zip" );
                return;
            }

            if ( File.Exists( a_zipPath ) )
                File.Delete( a_zipPath );

            ZipFile.CreateFromDirectory( a_buildPath, a_zipPath );
            Debug.Log( $"Zipped {a_buildPath} to {a_zipPath}" );
        }
#endif // JM_BUILD_ZIP

        private static void CopyFiles( string a_targetPath, string[] a_fileList ) {
            if ( a_fileList == null ) return;
            foreach ( var sourceFilePath in a_fileList ) {
                if ( CheckFile( sourceFilePath ) == false ) continue;
                var targetFilePath = $"{a_targetPath}/{sourceFilePath}";
                FileUtil.ReplaceFile( sourceFilePath, targetFilePath );
            }
        }

        static public string GetBuildTargetReadableName( BuildTarget a_target ) {
            switch ( a_target ) {
                case BuildTarget.StandaloneLinux: return "Linux 32";
                case BuildTarget.StandaloneLinux64: return "Linux 64";
                case BuildTarget.StandaloneOSX: return "Mac OS X";
                case BuildTarget.StandaloneWindows: return "Windows 32";
                case BuildTarget.StandaloneWindows64: return "Windows 64";
                default: return a_target.ToString();
            }
        }
    }
}

#endif 
