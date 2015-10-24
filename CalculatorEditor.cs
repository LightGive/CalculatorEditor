using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// 電卓のエディタ拡張
/// </summary>
public class CalculatorEditor : EditorWindow
{
	//ボタンのサイズ
	private const float BUTTON_WIDTH = 40.0f;
	private const float BUTTON_HEIGHT = 35.0f;

	private string calcStr = "";
	private bool writePermit = true;

	[MenuItem("Tools/Calclator")]
	static void OpenWindow()
	{
		CalculatorEditor window = GetWindow<CalculatorEditor>();
		var sizeWidth = (BUTTON_WIDTH * 4) + 20;
		var sizeHeight = 250;
		window.position = new Rect(300, 50, sizeWidth, sizeHeight);
		window.Show();
	}

	/// <summary>
	/// 表示処理
	/// </summary>
	void OnGUI()
	{

		GUILayout.Label("Calculator", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		GUILayout.Box(calcStr == "" ? "0" : calcStr, GUILayout.Width((BUTTON_WIDTH * 4) + 10));

		if (!writePermit)
			GUI.color = Color.cyan;
		if (GUILayout.Button("C", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			writePermit = true;
			calcStr = "";
		}

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("7", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "7";
		}
		if (GUILayout.Button("8", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "8";

		}
		if (GUILayout.Button("9", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "9";

		}
		if (GUILayout.Button("/", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (inputCheck() != 1)
				return;
			calcStr += "/";
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("4", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "4";

		}
		if (GUILayout.Button("5", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "5";

		}
		if (GUILayout.Button("6", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "6";

		}
		if (GUILayout.Button("*", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (inputCheck() != 1)
				return;
			calcStr += "*";
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("1", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "1";

		}
		if (GUILayout.Button("2", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "2";

		}
		if (GUILayout.Button("3", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "3";

		}
		if (GUILayout.Button("-", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (inputCheck() != 1)
				return;
			calcStr += "-";
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("0", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (!writePermit)
				return;
			calcStr += "0";

		}
		if (GUILayout.Button(".", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (inputCheck() != 1)
				return;
			calcStr += ".";
		}
		if (GUILayout.Button("=", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			char Operator = ' ';
			string formNumCopy = string.Empty;
			double leftValue = 0.0;
			double rightValue = 0.0;
			bool First_Found = true;
			if (calcStr == "")
				return;
			for (int i = 0; i < calcStr.Length; i++)
			{
				var charTmp = calcStr[i];
				if (char.IsDigit(charTmp) == true || charTmp == '.')
				{
					formNumCopy += charTmp;
					if (i == calcStr.Length - 1)
					{
						rightValue = double.Parse(formNumCopy);
						leftValue = calc(leftValue, rightValue, Operator);
						formNumCopy = string.Empty;
					}
				}
				else if (charTmp == '+' || charTmp == '-' || charTmp == '*' || charTmp == '/')
				{
					if (First_Found == true)
					{
						leftValue = double.Parse(formNumCopy);
						formNumCopy = string.Empty;
						First_Found = false;
					}
					else
					{
						rightValue = double.Parse(formNumCopy);
						leftValue = calc(leftValue, rightValue, Operator);
						formNumCopy = string.Empty;
					}
					Operator = charTmp;
				}
			}
			calcStr = leftValue.ToString();
			writePermit = false;
		}
		if (GUILayout.Button("+", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
		{
			if (inputCheck() != 1)
				return;
			calcStr += "+";
		}
		EditorGUILayout.EndHorizontal();
	}

	/// <summary>
	/// 計算する
	/// </summary>
	/// <param name="x">1つめの数字</param>
	/// <param name="y">2つめの数字</param>
	/// <param name="op">演算子</param>
	/// <returns>答え</returns>
	double calc(double x, double y, char op)
	{
		double ans = 0.0;
		switch (op)
		{
			case '+':
				ans = x + y;
				break;
			case '-':
				ans = x - y;
				break;
			case '*':
				ans = x * y;
				break;
			case '/':
				ans = x / y;
				break;
		}

		return ans;
	}

	/// <summary>
	/// 入力チェック
	/// </summary>
	/// <returns></returns>
	int inputCheck()
	{
		if (calcStr == "")
		{
			return -1;
		}
		else
		{
			var len = calcStr.Length;
			var op = calcStr[len - 1];
			if (op == '+' || op == '-' || op == '*' || op == '/' || op == '.')
				return -1;
			else
			{
				if (calcStr.Contains(","))
					return -1;
				else
					return 1;
			}
		}
	}
}
