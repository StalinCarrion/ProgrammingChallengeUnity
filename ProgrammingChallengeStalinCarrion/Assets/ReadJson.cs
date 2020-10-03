using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;
using UnityEngine.UI;

public class ReadJson : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public TextMeshProUGUI textColumn;
    public TextMeshProUGUI textCelds;
    public Transform positionFather;
    private string jsonText;
    private JsonData itemData;
    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

        jsonText = File.ReadAllText(Application.dataPath + "/StreamingAssets/JsonChallenge.json");
        
        ShowAllData();
    }

    public void ReloadData()
    {
        //Destroy(GameObject.FindWithTag("Data"));
        DestroyWithTag("Data");
        jsonText = File.ReadAllText(Application.dataPath + "/StreamingAssets/JsonChallenge.json");
        ShowAllData();
    }
    

   public void GetNumGridLayoutGroup(int numColum)
    {
        gridLayoutGroup.constraintCount = numColum;
    }
    public void ShowAllData()
    {
        itemData = JsonMapper.ToObject(jsonText);

        int nColum = itemData["ColumnHeaders"].Count;
        int nData = itemData["Data"].Count;

        GetNumGridLayoutGroup(nColum);
        GetNameColum(nColum);
        GetNameData(nData, nColum);

    }

    public void GetNameColum(int numColumn)
    {
        for (int i = 0; i < numColumn; i++)
        {
            Debug.Log(itemData["ColumnHeaders"][i]);
            TextMeshProUGUI textData = Instantiate(textColumn);

            textData.transform.SetParent(positionFather, false);

            textData.text = itemData["ColumnHeaders"][i].ToString();
        }
    }

    public void GetNameData(int numData, int numColum)
    {
        string nameColum;
        for (int i = 0; i < numData; i++)
        {
            for (int j = 0; j < numColum; j++)
            {
                TextMeshProUGUI textDataCeld = Instantiate(textCelds);
                nameColum = itemData["ColumnHeaders"][j].ToString();
                textDataCeld.transform.SetParent(positionFather, false);
                textDataCeld.text = itemData["Data"][i][nameColum].ToString();

                Debug.Log(itemData["Data"][i][nameColum]);
            }
        }
    }

    void DestroyWithTag(string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

}
