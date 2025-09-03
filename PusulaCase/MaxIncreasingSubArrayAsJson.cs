using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PusulaCase
{
    public class MaxIncreasingSubArray
    {
        public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return JsonSerializer.Serialize(new List<int>());
            }

            List<int> maxSubarray = new List<int>();

            List<int> currentSubarray = new List<int>();

            int numberSum = 0;

            int maxSum = 0;

            for (int i = 0; i< numbers.Count; i++) { 

                if (i == 0 || numbers[i] > numbers[i - 1])
                {
                    currentSubarray.Add(numbers[i]);
                    numberSum += numbers[i];

                }
                else
                {
                    if (numberSum > maxSum)
                    {
                        maxSum = numberSum;
                        maxSubarray = new List<int>(currentSubarray);
                    }
                    currentSubarray.Clear();
                    currentSubarray.Add(numbers[i]);
                    numberSum = numbers[i];
                }
            }
            if (numberSum > maxSum)
            {
                maxSubarray = new List<int>(currentSubarray);
            }
            return JsonSerializer.Serialize(maxSubarray);


        }
    }
}
