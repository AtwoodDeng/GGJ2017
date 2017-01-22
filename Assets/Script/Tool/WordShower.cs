using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class WordShower : MonoBehaviour {

	[SerializeField] string baseStr;
	[SerializeField] Text wordText;
	[SerializeField] Text baseText;

	int[] positions = new int[7];
	int[] values = new int[7];
	int changeLength = 1;

	public void SetBase( string str)
	{
		baseStr = str;
	}

	public void SetPosition( int index , float val )
	{
		positions[index] = (int)(val * baseStr.Length);
	}

	public void SetPosition( int index , int val )
	{
		positions[index] = val;
		
	}

	public void SetValue( int index , float val )
	{
		values[index] = (int)(val * 10);
	}

	public string GetWordResult()
	{
		string res = string.Copy(baseStr);
		char[] resChar = res.ToCharArray();

		for ( int k = 0 ; k < positions.Length; ++k )
		{
			for( int i = positions[k] - changeLength ; i <= positions[k] + changeLength ; i++ )
			{
				if ( i >= 0 && i < res.Length )
					resChar[i] = GetMoveChar( resChar[i] , values[k] );
			}
		}

		return new string(resChar);
	}

	char GetMoveChar( char inChar , int move)
	{
		int ori = Convert.ToInt32(inChar);
		ori += move;
		if ( ori < Convert.ToInt32('A') )
			ori += 26;
		if ( ori > Convert.ToInt32('Z') )
			ori -= 26;
		char res = Convert.ToChar( ori );
		return res;
		
	}

	string AddColor( string ori, int position , string color )
	{
		if ( position < 0 || position >= ori.Length )
			return ori;
		string res = string.Copy(ori);
		res = res.Insert( position + 1 , "</color>");
		res = res.Insert(position , "<color=" + color + ">");
		return res;
	}

	void Update()
	{
		string res = GetWordResult();
//		int pos1 = -1;
//		int pos2 = -1;
//		if ( positions[0] > positions[1] ) {
//			pos1 = positions[0];
//			pos2 = positions[1];
//		}else if ( positions[0] < positions[1] ) {
//			pos2 = positions[0];
//			pos1 = positions[1];
//		}
//		else 
//			pos1 = positions[0];
//
//		res = AddColor( res , pos1 , "red");
//		if ( pos2 > 0 )
//			res = AddColor( res , pos2 , "red");

		wordText.text = res;
		if ( baseText != null )
			baseText.text = baseStr;

	}
}
