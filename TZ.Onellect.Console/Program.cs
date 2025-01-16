using Flurl.Http;
using Microsoft.Extensions.Configuration;
using TZ.Onellect.Console;

#region Connection

var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("Parameters.json", optional: true, reloadOnChange: true);

var temp = builder.Build();

var strConnection = temp["SendTo"];

if (string.IsNullOrEmpty(strConnection))
    Console.WriteLine("Connection string is empty");

#endregion

#region Main code

List<Func<List<int>, List<int>>> sortMethods = [BubbleSort, CoctailShakerSort, InsertionSort, GnomeSort, TreeSort];

Random rand = new();
var count = rand.Next(20, 101);
List<int> nums = [];

Console.WriteLine("\nSource array:");
for (int i = 0; i < count; i++)
{
    nums.Add(rand.Next(-100, 100));

    Console.Write(nums[i] + " ");
}

var method = sortMethods[rand.Next(sortMethods.Count)];

var methodResult = method.Invoke(nums);

Console.WriteLine("\n\nSorted:");

foreach (var i in methodResult)
{
    Console.Write(i + " ");
}

Console.WriteLine("\n");

if (!string.IsNullOrEmpty(strConnection))
{
    try
    {
        var responce = await strConnection
        .PostJsonAsync(methodResult)
        .ReceiveString();

        Console.WriteLine($"Responce: {responce}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

Console.WriteLine("Press any key to exit");
Console.ReadKey();

#endregion

#region Sorting algorithms

List<int> BubbleSort(List<int> arr)
{
    for (int write = 0; write < arr.Count; write++)
    {
        for (int sort = 0; sort < arr.Count - 1; sort++)
        {
            if (arr[sort] > arr[sort + 1])
                Swap(arr, sort, sort + 1);
        }
    }

    return arr;
}

List<int> CoctailShakerSort(List<int> arr)
{
    int left = 0,
         right = arr.Count - 1;
    while (left < right)
    {
        for (int i = left; i < right; i++)
        {
            if (arr[i] > arr[i + 1])
                Swap(arr, i, i + 1);
        }
        right--;
        for (int i = right; i > left; i--)
        {
            if (arr[i - 1] > arr[i])
                Swap(arr, i - 1, i);
        }
        left++;
    }

    return arr;
}

List<int> InsertionSort(List<int> arr)
{
    for (int i = 1; i < arr.Count; i++)
    {
        int k = arr[i];
        int j = i - 1;

        while (j >= 0 && arr[j] > k)
        {
            arr[j + 1] = arr[j];
            arr[j] = k;
            j--;
        }
    }

    return arr;
}

List<int> GnomeSort(List<int> arr)
{
    int i = 1;
    int j = 2;
    while (i < arr.Count)
    {
        if (arr[i - 1] < arr[i])
        {
            i = j;
            j += 1;
        }
        else
        {
            Swap(arr, i - 1, i);
            i -= 1;
            if (i == 0)
            {
                i = j;
                j += 1;
            }
        }
    }

    return arr;
}

List<int> TreeSort(List<int> arr)
{
    var treeNode = new TreeNode(arr[0]);
    for (int i = 1; i < arr.Count; i++)
    {
        treeNode.Insert(new TreeNode(arr[i]));
    }

    return treeNode.Transform();
}

#endregion

#region Support methods

static void Swap(List<int> array, int i, int j)
{
    (array[j], array[i]) = (array[i], array[j]);
}

#endregion