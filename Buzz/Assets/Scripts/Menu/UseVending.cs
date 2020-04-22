using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseVending : MonoBehaviour
{

	public CanvasGroup vendingMachineItems;
	public CanvasGroup visitVending;
	public CanvasGroup chooseItem;
	public CanvasGroup notEnoughBone;
	public CanvasGroup alreadyOwn;
	public CanvasGroup oneBoneCost;
	public CanvasGroup fiveBoneCost;
	public CanvasGroup congrats;
	public CanvasGroup congratsWanted;
	public CanvasGroup vendingUI;

	public PowerupCollector pc;
	public SlapperScript slapper;

	CanvasGroup item;
	int numBones;

	private bool isDonut;
	private bool isWhiteClaw;
	private bool isStarbucks;
	private bool isID;

	void Update() {

		numBones = pc.GetBoneCount();

		//resets all panels after user exits vending
		if(vendingUI.interactable) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				
				vendingMachineItems.interactable = false;
				SetPanelFalse(chooseItem);
				SetPanelFalse(notEnoughBone);
				SetPanelFalse(alreadyOwn);
				SetPanelFalse(oneBoneCost);
				SetPanelFalse(fiveBoneCost);
				SetPanelFalse(congrats);
				SetPanelFalse(congratsWanted);
				SetPanelTrue(visitVending);
				item = null;
				isDonut = false;
				isID = false;
				isStarbucks = false;
				isWhiteClaw = false;
			}
		}


		//actual vending process starts HERE, asks user if they want to visit vending
		if(visitVending.interactable) {
			if (Input.GetKeyDown(KeyCode.Y)) {
				ClickedYes();
			} else if (Input.GetKeyDown(KeyCode.N)) {
				SetPanelFalse(vendingUI);
				Time.timeScale = 1f;
			}
		}

		if (fiveBoneCost.interactable) {
			if (Input.GetKeyDown(KeyCode.Y)) {

				if (isDonut) {
					//Debug.Log("came in isDonut");
			    	pc.SetNumBoneFrag(numBones - 5); //the "5" is dependent on cost of donut
			    	pc.ReceiveDonut();
			    	//Debug.Log(item);
			    	isDonut = false;
			    }
			    
			    //Debug.Log(item);
			    if (item != null) {
			    	SetPanelFalse(item);
				    SetPanelFalse(fiveBoneCost);
				    SetPanelTrue(congrats);
			    }
			    item = null;

			} else if (Input.GetKeyDown(KeyCode.N)) {
			    SetPanelFalse(fiveBoneCost);
			    SetPanelTrue(chooseItem);
			    isDonut = false;
			    item = null;
			}
		}

		//if whiteclaw or latte
		if (oneBoneCost.interactable) {
			if (Input.GetKeyDown(KeyCode.Y)) {

			    if (isStarbucks) {
			    	//Debug.Log("came in isStarbucks");
			    	pc.SetNumBoneFrag(numBones - 1); //the "1" is dependent on cost of latte/whiteclaw
			    	pc.ReceiveLatte();
			    	//Debug.Log(item);
			    	isStarbucks = false;

			    }
			    if (isWhiteClaw) {
			    	//Debug.Log("came in isWhiteClaw");
			    	pc.SetNumBoneFrag(numBones - 1); //the "1" is dependent on cost of latte/whiteclaw
			    	pc.ReceiveWhiteClaw();
			    	//Debug.Log(item);
			    	//SetPanelFalse(item);
			    	isWhiteClaw = false;
			    }

			    if (isID) {
			    	pc.SetNumBoneFrag(numBones - 1);
			    	slapper.halfWantedLevel();
			    	isID = false;
			    }

			    //Debug.Log(item);
			    if (item != null) {
			    	SetPanelFalse(item);
				    SetPanelFalse(oneBoneCost);
				    SetPanelTrue(congrats);
			    }
			    
			    item = null;

			} else if (Input.GetKeyDown(KeyCode.N)) {
			    SetPanelFalse(oneBoneCost);
			    SetPanelTrue(chooseItem);
			    isStarbucks = false;
			    isWhiteClaw = false;
			    isID = false;
			    item = null;
			}
		}
	}

	public void ClickedYes() {

		//close prev panel
		SetPanelFalse(visitVending);

        //open choose Item panel
        SetPanelTrue(chooseItem);

        //player can now click on items in the vending machine
        vendingMachineItems.interactable = true;
	}

	//when player clicks on any donut
	public void ClickedDonut() {

		CanvasGroup temp = GetComponent<CanvasGroup>();
		item = temp;
		//Debug.Log(item);
		//close choose item UI
		SetPanelFalse(chooseItem);
		SetPanelFalse(congrats);
		SetPanelFalse(congratsWanted);
		SetPanelFalse(oneBoneCost);

		//player already owns donut in inventory
		if (pc.HasDonut() == true) {

			SetPanelTrue(alreadyOwn);
			//Debug.Log("already own triggered donut");
			item = null;

		} else {
			//player has enough bones for donut
			if (numBones >= 5) {

				SetPanelFalse(alreadyOwn);
				SetPanelFalse(congrats);
				SetPanelFalse(congratsWanted);
				SetPanelFalse(notEnoughBone);
			    //open confirmation UI
			    SetPanelTrue(fiveBoneCost);
			    isDonut = true;
			    isStarbucks = false;
			    isWhiteClaw = false;
			    isID = false;	

			} else {
				//player doesn't have enough bones for donut
			    SetPanelFalse(alreadyOwn);
			    SetPanelTrue(notEnoughBone);
			    item = null;
			}
		}
		
	}

	public void ClickedWhiteClaw() {
		CanvasGroup temp = GetComponent<CanvasGroup>();
		item = temp;
		//Debug.Log(item);
		//close choose item UI
		SetPanelFalse(chooseItem);
		SetPanelFalse(congrats);
		SetPanelFalse(congratsWanted);
		SetPanelFalse(fiveBoneCost);

		//player already owns whiteclaw in inventory
		if (pc.HasWhiteClaw() == true) {

			SetPanelTrue(alreadyOwn);
			//Debug.Log("already own triggered whiteclaw");
			item = null;

		} else {
			//player has enough bones for whiteclaw
			if (numBones >= 1) {

				SetPanelFalse(alreadyOwn);
				SetPanelFalse(congrats);
				SetPanelFalse(congratsWanted);
				SetPanelFalse(notEnoughBone);
			    //open confirmation UI
			    SetPanelTrue(oneBoneCost);
			    isDonut = false;
			    isStarbucks = false;
			    isWhiteClaw = true;	
			    isID = false;		 

			} else {
				//player doesn't have enough bones for whiteclaw
			    SetPanelFalse(alreadyOwn);
			    SetPanelTrue(notEnoughBone);
			    item = null;
			}
		}
	}

	public void ClickedStarbucks() {

		CanvasGroup temp = GetComponent<CanvasGroup>();
		item = temp;
		//Debug.Log(item);
		//close choose item UI
		SetPanelFalse(chooseItem);
		SetPanelFalse(congrats);
		SetPanelFalse(congratsWanted);
		SetPanelFalse(fiveBoneCost);

		//player already owns starbucks in inventory
		if (pc.HasLatte() == true) {

			SetPanelTrue(alreadyOwn);
			//Debug.Log("already own triggered latte");
			item = null;

		} else {
			//player has enough bones for starbucks
			if (numBones >= 1) {

				SetPanelFalse(alreadyOwn);
				SetPanelFalse(congrats);
				SetPanelFalse(congratsWanted);
				SetPanelFalse(notEnoughBone);
			    //open confirmation UI
			    SetPanelTrue(oneBoneCost);
			    isDonut = false;
			    isStarbucks = true;
			    isWhiteClaw = false;
			    isID = false;		    

			} else {
				//player doesn't have enough bones for starbucks
			    SetPanelFalse(alreadyOwn);
			    SetPanelTrue(notEnoughBone);
			    item = null;
			}
		}
	}

	public void ClickedID() {

		CanvasGroup temp = GetComponent<CanvasGroup>();
		item = temp;
		//Debug.Log(item);
		//close choose item UI
		SetPanelFalse(chooseItem);
		SetPanelFalse(congrats);
		SetPanelFalse(congratsWanted);
		SetPanelFalse(fiveBoneCost);

		//player has enough bones for starbucks
			if (numBones >= 1) {

				SetPanelFalse(alreadyOwn);
				SetPanelFalse(congrats);
				SetPanelFalse(congratsWanted);
				SetPanelFalse(notEnoughBone);
			    //open confirmation UI
			    SetPanelTrue(oneBoneCost);
			    isID = true;
			    isDonut = false;
			    isStarbucks = false;
			    isWhiteClaw = false;		    

			} else {
				//player doesn't have enough bones for starbucks
			    SetPanelFalse(alreadyOwn);
			    SetPanelTrue(notEnoughBone);
			    item = null;
			}

	}

	private void SetPanelFalse(CanvasGroup c) {
		c.interactable = false;
		c.blocksRaycasts = false;
		c.alpha = 0f;
	}

	private void SetPanelTrue(CanvasGroup c) {
		c.interactable = true;
		c.blocksRaycasts = true;
		c.alpha = 1f;
	}


}
