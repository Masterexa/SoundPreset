using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using NowhereUnity.Audio;
using System.Reflection;

namespace MasterexaEditor.Audio{

	[CustomEditor(typeof(SoundPreset))]
	public class SoundPresetEditor : Editor
	{
		#region Instance
			#region Fields
				SerializedProperty	usePitch;
				SerializedProperty	loop;
				ReorderableList		clipsList;
			#endregion

			#region Methods
				void OnEnable()
				{
					usePitch	= serializedObject.FindProperty ("usePitch");
					loop		= serializedObject.FindProperty ("loop");
					clipsList	= new ReorderableList (serializedObject, serializedObject.FindProperty ("clips"));

					
					// 要素の高さ		
					//clipsList.elementHeight = 40.0f;
					
					// 項目のコールバック関数
					clipsList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
					{
						var prp = clipsList.serializedProperty.GetArrayElementAtIndex(index);
						var w	= rect.width;
						var h	= rect.height;

						// 項目の表示
						EditorGUI.PropertyField(new Rect(rect.x,rect.y,w,h) ,prp, new GUIContent("Clip"));
					};

					// ヘッダーのコールバック
					clipsList.drawHeaderCallback =(rect)=>
					{
						EditorGUI.LabelField(rect, "Clips");
					};
				}

				/// <summary>
				/// Raises the inspector GU event.
				/// </summary>
				public override void OnInspectorGUI ()
				{
					var sdp = target as SoundPreset;

					EditorGUI.BeginChangeCheck ();
					{
						// Clipsリストの表示
						clipsList.DoLayoutList ();

						// ループチェックの表示
						EditorGUILayout.PropertyField (loop);

						// Pitchの設定の表示
						EditorGUILayout.PropertyField (usePitch);
						if (usePitch.boolValue) {
							EditorGUI.indentLevel++;

							EditorGUILayout.MinMaxSlider (ref sdp.pitchRange.min, ref sdp.pitchRange.max, -3.0f, 3.0f);
							EditorGUILayout.BeginHorizontal ();
							{
								sdp.pitchRange.min = Mathf.Clamp (EditorGUILayout.FloatField (sdp.pitchRange.min), -3.0f, sdp.pitchRange.max);
								sdp.pitchRange.max = Mathf.Clamp (EditorGUILayout.FloatField (sdp.pitchRange.max), sdp.pitchRange.min, 3.0f);
							}
							EditorGUILayout.EndHorizontal ();

							EditorGUI.indentLevel--;
						}

                        // Previewボタン
                        EditorGUILayout.Space();
                        if (GUILayout.Button("Preview"))
                        {
                            PlaySound();
                        }
					}
					EditorGUI.EndChangeCheck ();

					EditorUtility.SetDirty (target);
					serializedObject.ApplyModifiedProperties ();
				}

                /// <summary>
                /// Play 
                /// </summary>
                void PlaySound(){

                    var sdp = target as SoundPreset;

                    var editorAssembly  = typeof(AudioImporter).Assembly;
                    var audioUtilClass  = editorAssembly.GetType("UnityEditor.AudioUtil");

                    var method  = audioUtilClass.GetMethod(
                        "PlayClip",
                        BindingFlags.Static | BindingFlags.Public,
                        null,
                        new System.Type[] { typeof(AudioClip) },
                        null
                    );

                    method.Invoke(null, new object[] { sdp.RandomSelect() });
                }
			#endregion
		#endregion
	}
}