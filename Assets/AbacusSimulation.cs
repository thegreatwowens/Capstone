using UnityEngine;
using TMPro;

public class AbacusSimulation : MonoBehaviour
{
    // Abacus beads
    public GameObject beadPrefab;
    public int rows = 4;
    public int beadsPerRow = 10;
    public float beadSpacing = 1.0f;
    public float rowSpacing = 1.5f;
  
    // Abacus values
    private int[,] values;
    private int selectedRow = 0;
    private int selectedBead = 0;

    // UI elements
    public TextMeshProUGUI resultText;
    public GameObject beadParent;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the abacus
        InitializeAbacus();
    }

    // Update is called once per frame
    void Update()
    {
        // Perform abacus calculations here if needed
    }

    // Initialize the abacus
    private void InitializeAbacus()
    {
        // Create a 2D array to hold the abacus values
        values = new int[rows, beadsPerRow];

        // Calculate the starting position
        Vector3 startPos = new Vector3(-(beadsPerRow - 1) * beadSpacing * 0.5f, 0.0f, (rows - 1) * rowSpacing * 0.5f);

        // Create the beads
        for (int row = 0; row < rows; row++)
        {
            for (int bead = 0; bead < beadsPerRow; bead++)
            {
                // Calculate the position of the bead
                Vector3 beadPos = startPos + new Vector3(bead * beadSpacing, 0.0f, -row * rowSpacing);

                // Create the bead game object
                GameObject newBead = Instantiate(beadPrefab, beadPos, Quaternion.identity);

                // Set the bead's parent to the current room object
                newBead.transform.SetParent(beadParent.transform);

                // Set the bead's value in the array
                values[row, bead] = 0;
            }
        }
    }

    // Move the selected bead up
    public void MoveBeadUp()
    {
        if (selectedRow > 0)
        {
            selectedRow--;
            UpdateSelectedBead();
        }
    }

    // Move the selected bead down
    public void MoveBeadDown()
    {
        if (selectedRow < rows - 1)
        {
            selectedRow++;
            UpdateSelectedBead();
        }
    }

    // Move the selected bead left
    public void MoveBeadLeft()
    {
        if (selectedBead > 0)
        {
            selectedBead--;
            UpdateSelectedBead();
        }
    }

    // Move the selected bead right
    public void MoveBeadRight()
    {
        if (selectedBead < beadsPerRow - 1)
        {
            selectedBead++;
            UpdateSelectedBead();
        }
    }

    // Increment the value of the selected bead
    public void IncrementValue()
    {
        values[selectedRow, selectedBead]++;
        UpdateSelectedBead();
        UpdateResult();
    }

    // Decrement the value of the selected bead
    public void DecrementValue()
    {
        if (values[selectedRow, selectedBead] > 0)
        {
            values[selectedRow, selectedBead]--;
            UpdateSelectedBead();
            UpdateResult();
        }
    }

    // Multiply the value of the selected bead by a factor
    public void MultiplyValue(int factor)
    {
        values[selectedRow, selectedBead] *= factor;
        UpdateSelectedBead();
        UpdateResult();
    }

    // Divide the value of the selected bead by a divisor
    public void DivideValue(int divisor)
    {
        values[selectedRow, selectedBead] /= divisor;
        UpdateSelectedBead();
        UpdateResult();
    }

    // Update the position and appearance of the selected bead
    private void UpdateSelectedBead()
    {
        // Reset the previous selected bead's appearance
        GameObject previousBead = transform.GetChild(selectedRow * beadsPerRow + selectedBead).gameObject;
        previousBead.GetComponent<Renderer>().material.color = Color.white;

        // Update the selected bead's position
        selectedRow = Mathf.Clamp(selectedRow, 0, rows - 1);
        selectedBead = Mathf.Clamp(selectedBead, 0, beadsPerRow - 1);
        GameObject selectedBeadObj = transform.GetChild(selectedRow * beadsPerRow + selectedBead).gameObject;
        selectedBeadObj.GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Update the result text with the current abacus value
    private void UpdateResult()
    {
        int totalValue = 0;

        // Calculate the total value of the abacus
        for (int row = 0; row < rows; row++)
        {
            for (int bead = 0; bead < beadsPerRow; bead++)
            {
                totalValue += values[row, bead] * (int)Mathf.Pow(10, rows - row - 1);
            }
        }

        // Update the result text
        resultText.text = totalValue.ToString();
    }
}