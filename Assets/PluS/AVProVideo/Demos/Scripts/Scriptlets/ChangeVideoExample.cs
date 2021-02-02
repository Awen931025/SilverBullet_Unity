﻿using UnityEngine;
using RenderHeads.Media.AVProVideo;

//-----------------------------------------------------------------------------
// Copyright 2015-2018 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProVideo.Demos
{
	/// <summary>
	/// A demo that shows how to use scripting to load videos
	/// </summary>
	public class ChangeVideoExample : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private MediaPlayer _mediaPlayer;

		public void LoadVideo(string filePath)
		{
			_mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, filePath, true);
		}

		void OnGUI()
		{
			if (GUILayout.Button("video1"))
			{
				LoadVideo("video1.mp4");
			}

			if (GUILayout.Button("video2"))
			{
				LoadVideo("video2.mp4");
			}
		}
	}
}