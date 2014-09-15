using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PSM.Environment;
using UnityEngine.PSM.Services;

public class InAppPurchaseSample : MonoBehaviour
{
	private InAppPurchaseDialog dialog;

	// ダイアログの処理状態（false=待機中, true=処理中）
    // State of dialog (false: waiting, true: running)
	private static bool dialogIsBusy;

	// 全アイテムのチェック状態を格納するフラグ
    // Flag to store the selected item
	private static bool[] itemIsSelected;

	// スクロールビュー位置情報
    // Position of scroll view
	private Vector2 scrollViewVector = Vector2.zero;

    public GUISkin mySkin;

    public int offset = Screen.height / 25;
    public const int FONT_SIZE = 20;

    private bool doesErrorDialogOpen = false;

	void Start ()
	{
        // InAppPurchaseDialogを初期化する
        // Instantiate InAppPurchaseDialog class
        dialog = new InAppPurchaseDialog();
        dialogIsBusy = false;

        // ProductList数分のフラグを用意する
        // Create the flags
        int count = dialog.ProductList.Count;
        itemIsSelected = new bool[count];
	}

	void Update ()
	{
		// InAppPurchase処理中
        // Running InAppPurchaseDialog
		if (dialogIsBusy == true)
		{
			// InAppPurchase処理完了
            // Finished procedure of dialog
			if (dialog.State == CommonDialogState.Finished)
			{
				dialogIsBusy = false;

				// 処理結果の表示
                // Show the result
                if (dialog.Result != CommonDialogResult.OK)
                {
                    String message = "Dialog result is \"" + dialog.Result.ToString() + "\"";
                    Debug.Log(message);
                    ShowErrorMessageDialog(message);
                }

				// 選択中アイテムリストのクリア
                // Clear the item list
				Array.Clear(itemIsSelected, 0, itemIsSelected.Length);
			}
		}
	}

	void OnGUI()
	{
        GUI.skin = mySkin;

		Rect rct_button = new Rect();

		// ボタンの表示
        // Show buttons
		{
			int bw = Screen.width / 5;
			int bh = Screen.height / 10;

			rct_button = new Rect(bw / 2, Screen.height - bh, bw * 4, bh);
			GUI.BeginGroup(rct_button);

			Rect rect = new Rect(0, 0, bw, bh);

			if (GUI.Button(rect, "GetProductInfo"))
			{
                if (doesErrorDialogOpen == false)
                {
                    try
                    {
                        dialog.GetProductInfo(GetSelectedItems());
                        dialogIsBusy = true;
                    }
                    catch (Exception exception)
                    {
                        ShowErrorMessageDialog(exception.Message);
                        Debug.Log(exception);
                    }
                }
			}

			rect.x += bw;

			if (GUI.Button(rect, "GetTicketInfo"))
			{
                if (doesErrorDialogOpen == false)
                {
                    try
                    {
                        dialog.GetTicketInfo();
                        dialogIsBusy = true;
                    }
                    catch (Exception exception)
                    {
                        ShowErrorMessageDialog(exception.Message);
                        Debug.Log(exception);
                    }
                }
			}

			rect.x += bw;

			if (GUI.Button(rect, "Purchase"))
			{
                if (doesErrorDialogOpen == false)
                {
                    try
                    {
                        dialog.Purchase(GetSelectedItem());
                        dialogIsBusy = true;
                    }
                    catch (Exception exception)
                    {
                        ShowErrorMessageDialog(exception.Message);
                        Debug.Log(exception);
                    }
                }
			}

			rect.x += bw;

			if (GUI.Button(rect, "Consume"))
			{
                if (doesErrorDialogOpen == false)
                {
                    try
                    {
                        dialog.Consume(GetSelectedItem());
                        dialogIsBusy = true;
                    }
                    catch (Exception exception)
                    {
                        ShowErrorMessageDialog(exception.Message);
                        Debug.Log(exception);
                    }
                }
			}

			GUI.EndGroup();
		}

		// リストビューの表示
        // Show list view
		if ((itemIsSelected != null) && (itemIsSelected.Length > 0))
		{
			int vw = (int)(Screen.width * 0.9f);
			int vh = (int)(Screen.height - offset - rct_button.height);

			int ih = 50;

			Rect rect = new Rect((Screen.width - vw) / 2, offset, vw, vh);

			scrollViewVector = GUI.BeginScrollView(rect, scrollViewVector, new Rect(0, 0, vw, ih * (itemIsSelected.Length + 1)), false, false);

            // Column Label
            int columnXPos = 80;
            int[] columnXPosList = { columnXPos,
                                     columnXPos += (FONT_SIZE * 2),
                                     columnXPos += (FONT_SIZE * 6),
                                     columnXPos += (FONT_SIZE * 20),
                                     columnXPos += (FONT_SIZE * 7),
                                     columnXPos += (FONT_SIZE * 3)
                                   };

            GUI.Label(new Rect(columnXPosList[1], 0, vh, (FONT_SIZE * 6)),  "Label");
            GUI.Label(new Rect(columnXPosList[2], 0, vh, (FONT_SIZE * 20)), "Name");
            GUI.Label(new Rect(columnXPosList[3], 0, vh, (FONT_SIZE * 8)),  "Price");
            GUI.Label(new Rect(columnXPosList[4], 0, vh, (FONT_SIZE * 6)),  "Ticket");

            // Items
			for (int i = 0; i < itemIsSelected.Length; i++)
			{
				var info = dialog.ProductList[i];

				string label = info.Label;
				string name = info.Name;
				string price = "";
                string ticket = "";

				InAppPurchaseTicketType type  = info.TicketType;

                if (info.Price != "")
                {
                    price = info.Price;
                }
                else
                {
                    price = "-";
                }

                if (dialog.IsTicketInfoComplete == true)
                {
                    if (type == InAppPurchaseTicketType.Normal)
                    {
                        ticket = info.IsTicketValid ? "YES" : "No";
                    }
                    else
                    {
                        ticket = info.ConsumableTicketCount.ToString();
                    }
                }
                else
                {
                    ticket = "-";
                }

                // check box
				itemIsSelected[i] = GUI.Toggle(new Rect(0, (ih * (i + 1)), 40, 40), itemIsSelected[i], "");

                // Index
                GUI.Label(new Rect(columnXPosList[0], (ih * (i + 1)), vh, (FONT_SIZE * 2)), i.ToString());

                // Label
                GUI.Label(new Rect(columnXPosList[1], (ih * (i + 1)), vh, (FONT_SIZE * 6)), label);

                // Name
                GUI.Label(new Rect(columnXPosList[2], (ih * (i + 1)), vh, (FONT_SIZE * 20)), name);

                // Price
                GUI.Label(new Rect(columnXPosList[3], (ih * (i + 1)), vh, (FONT_SIZE * 7)), price);

                // Ticket
                GUI.Label(new Rect(columnXPosList[4], (ih * (i + 1)), vh, (FONT_SIZE * 3)), ticket);
			}

			GUI.EndScrollView();
		}
	}

    void ShowErrorMessageDialog(String message)
    {
        ErrorMessageDialog errMessageDialog = this.gameObject.AddComponent<ErrorMessageDialog>();
        errMessageDialog.message = message;
        errMessageDialog.mySkin = mySkin;
        errMessageDialog.Terminated += OnErrorDialogTerminated;
        doesErrorDialogOpen = true;
    }

    void OnErrorDialogTerminated(object sender, EventArgs e)
    {
        doesErrorDialogOpen = false;
    }

	// 選択中のアイテムをString配列で返却
    // Returns the string list of selected item
	string[] GetSelectedItems()
	{
		var items = new List<string>();

		for (int i = 0; i < itemIsSelected.Length; i++)
		{
			if (itemIsSelected[i])
				items.Add(dialog.ProductList[i].Label);
		}

		return (items.Count == 0) ? null : items.ToArray();
	}

	// 選択中のアイテムをStringで返却
    // Returns selectem item string 
	string GetSelectedItem()
	{
		var items = GetSelectedItems();

        if ((items == null) || (items.Length != 1))
        {
            throw new System.Exception("Please select 1 item\n");
        }

		return items[0];
	}
}

public class ErrorMessageDialog : MonoBehaviour
{
    public String message { private get; set; }

    public GUISkin mySkin { private get; set; }

    public event EventHandler<EventArgs> Terminated;

    void OnGUI()
    {
        GUI.skin = mySkin;
        GUI.depth = -1;

        float sw = Screen.width;
        float sh = Screen.height;

        GUI.Box(new Rect(sw / 5, sh / 3, sw * 3 / 5, sh / 3), message);

        if (GUI.Button(new Rect(sw * 2 / 5, sh / 2, sw / 5, sh / 10), "OK"))
        {
            Destroy(this);
            Terminated(this, EventArgs.Empty);
        }
    }
}

