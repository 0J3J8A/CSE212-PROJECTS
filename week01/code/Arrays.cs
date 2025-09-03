public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        double[] multiples = new double[length]; // Step 1: Create an array of doubles, the size would be 'length' in order to hold the multiples.

        for (int i = 0; i < length; i++) // Step 2: Use a loop to fill the array.
                                         //For each position i in the array (from 0 to length-1):
                                         //Calculate the multiple by multiplying (i + 1) by the 'number'.
                                         //Store this calculated multiple in the array at index i.

        {
            multiples[i] = number * (i + 1); // Calculate the multiple and store it
        }

        // After the loop
        return multiples; // Step 3: Create a return that contains all multiples.
    }


    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // NOTE: We want to move the last 'amount' elements to the front of the list.

        // Step 1: Use GetRange to copy the last 'amount' elements from the list
        List<int> lastElements = data.GetRange(data.Count - amount, amount); // Get the last amount of the list

        // Step 2: Use RemoveRange to remove those 'amount' elements from the end of the list. 
        data.RemoveRange(data.Count - amount, amount); // Remove those elements from the end

        // Step 3: Use InsertRange to insert those saved elements in Stept 1 and 2 at the beginning ( using the index 0).
        data.InsertRange(0, lastElements);
    }

}