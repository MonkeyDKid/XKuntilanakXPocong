using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


	public class SceneManagerHelper : MonoBehaviour
	{
		private static SceneManagerHelper _instance;

		private void Start()
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}

		public static void LoadScene(int sceneToLoad)
		{
			string path = SceneUtility.GetScenePathByBuildIndex(sceneToLoad);
			string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

			LoadScene(sceneName);
		}

		public static void LoadScene(string sceneToLoad)
		{
			SceneManager.LoadScene(sceneToLoad);
			SoundBG.CheckBackgroundMusic(sceneToLoad);
		}

		public static void LoadTutorial(string sceneToLoad)
		{
			FlowchartController.StartBlock(sceneToLoad);
			//SoundBG.CheckBackgroundMusic(sceneToLoad);
		}
		public static void StopTutorial()
		{
			FlowchartController.StopBlock();
			//SoundBG.CheckBackgroundMusic(sceneToLoad);
		}
		
		public static void LoadMusic(string sceneToLoad)
		{
			SoundBG.CheckBackgroundMusic(sceneToLoad);
		}
		public static void LoadSoundFX(string sceneToLoad)
		{
			SoundBG.CheckBackgroundMusicPlayOnce(sceneToLoad);
		}

		public static void StopMusic()
		{
			SoundBG.StopBGMusic();
		}

		public static void CekSound(float value)
		{
			//SoundBG.myvolume(value);
		}

			
		private static int _stageIndex;
		public static void LoadScene(string sceneToLoad, int stageIndex)
		{
			_stageIndex = stageIndex;
			LoadScene(sceneToLoad);
		}

			public static void ShowAdditiveScene(string newSceneName)
		{
			_instance.StartCoroutine(DoShowAdditiveScene(newSceneName));
		}

		public static void UnloadAdditiveScene(string newSceneName)
		{
			_instance.StartCoroutine(DoUnloadAdditiveScene(newSceneName));
		}

		private static IEnumerator DoShowAdditiveScene(string newSceneName)
		{
			var asyncLoad = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);

			while (!asyncLoad.isDone)
			{
				yield return null;
			}		
		}


		private static IEnumerator DoUnloadAdditiveScene(string newSceneName)
		{
			var isSceneLoaded = SceneManager.GetSceneByName(newSceneName).isLoaded;
			if (isSceneLoaded)
			{
				var asyncOperation = SceneManager.UnloadSceneAsync(newSceneName);

				while (!asyncOperation.isDone)
				{
					yield return null;
				}

				var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
				foreach (var g in gameObjects)
				{
					g.SetActive(true);
				}

			//	ManagerSingleton.ActiveManager.HookAnimatorBehaviour();
			}
		}
	public static void enableGO(string gameobjectName){
		var gameobject = GameObject.Find (gameobjectName);
		gameobject.SetActive (true);
	}
	}

