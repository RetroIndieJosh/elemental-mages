// This code is part of the JM Tools Build System library maintained by Joshua McLean (http://mrjoshuamclean.com)
// It is released for free under the MIT open source license (LICENSE.txt)

#if UNITY_EDITOR

namespace JoshuaMcLean.BuildSystem
{
    using UnityEditor;
    using UnityEngine;

    static public class ButlerInterface
    {
        public static void Push( string args ) {
            Debug.Log( $"[Butler] Pushing {args}" );
            Run( "push", args );
        }

        public static void Run( string a_command, string a_args ) {
            var buildWindow = EditorWindow.GetWindow( typeof( BuildWindow ) ) as BuildWindow;
            Debug.Log( $"Execute: {buildWindow.ButlerPath} {a_command} {a_args}" );

            var info = new System.Diagnostics.ProcessStartInfo( buildWindow.ButlerPath, $"{a_command} {a_args}" );
            var process = System.Diagnostics.Process.Start( info );

            process.OutputDataReceived += ( object s, System.Diagnostics.DataReceivedEventArgs e ) => {
                Debug.Log( $"[Butler] {e.Data}" );
            };
            process.ErrorDataReceived += ( object s, System.Diagnostics.DataReceivedEventArgs e ) => {
                Debug.LogError( $"[Butler] {e.Data}" );
            };

            process.WaitForExit();
        }
    }
}

#endif // UNITY_EDITOR
