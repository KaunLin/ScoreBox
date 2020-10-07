using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class ScoreBoxManager : MonoBehaviour {
	private string path=@"ScoreFile.txt";
	SortedList sortlist;
	private int scoreWidth=6;
	void Start () {
		sortlist = new SortedList ();
		loadFile ();
		showToScoreList ();
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.R)) {
			showBox ();
		}
	}
	public void insertRecord(){
		string name = GameObject.Find ("NameFieldText").GetComponent<Text> ().text;
		string sco = GameObject.Find ("ScoreFieldText").GetComponent<Text> ().text;
		int i;
		IList scoreList = sortlist.GetKeyList();
		IList nameList = sortlist.GetValueList();
		for (i = sortlist.Count - 1; i >= 0; i--){
			if (nameList [i].ToString ().Equals (name)) {
				if (Convert.ToInt32 (scoreList [i].ToString ().Substring (0, scoreWidth)) < Convert.ToInt32 (sco)) {
					sortlist.RemoveAt (i);
					string rec = sco.PadLeft (scoreWidth, '0') + " " + name;
					sortlist.Add (rec, name);
				}
				i = -2;
			}
		}
		if (i == -1) {
			string rec = sco.PadLeft (scoreWidth, '0') + " " + name;
			sortlist.Add (rec, name);
		}
		showToScoreList ();
	}
	public void saveFile(){
		int i;
		StreamWriter scoreWriter;
		FileInfo textFile = new FileInfo (path);
		if (textFile.Exists) {
			textFile.Delete ();
		}
		scoreWriter = textFile.CreateText ();
		IList scoreList = sortlist.GetKeyList ();
		for (i = sortlist.Count - 1; i >= 0; i--) {
			string textline = scoreList [i].ToString ();
			scoreWriter.WriteLine (textline);
		}
		scoreWriter.Close ();
	}
	public void loadFile(){
		string textline = string.Empty;
		StreamReader scoreReader = File.OpenText (path);
		try {
			while ((textline = scoreReader.ReadLine ()) != null) {
				if (!sortlist.Contains (textline)) {
					sortlist.Add (textline, textline.Substring (scoreWidth + 1));
				}
			}
		} catch (Exception e) {
			print ("Reading Error:" + e.Message);
		}
		scoreReader.Close ();
		showToScoreList ();
	}
	void showToScoreList(){
		IList scoreList = sortlist.GetKeyList ();
		string textString = "";
		string textline;
		for (int i = scoreList.Count - 1; i >= 0; i--) {
			textline = scoreList [i].ToString ();
			textString = textString + textline + "\r\n";
		}
		GameObject.Find ("ScoreListText").GetComponent<Text> ().text = textString;
	}
	public void consoleShow(){
		int i;
		string rec = "High Score(Top " + sortlist.Count.ToString () + "):\r\n";
		string textline;
		print (rec);
		IList scoreList = sortlist.GetKeyList ();
		for (i = sortlist.Count - 1; i >= 0; i--) {
			textline = scoreList [i].ToString ();
			print (textline);
		}
	}
	public void hideBox(){
		GameObject.Find ("ControlCenter").GetComponent<Canvas> ().enabled = false;
	}
	public void showBox(){
		GameObject.Find ("ControlCenter").GetComponent<Canvas> ().enabled = true;
	}
}