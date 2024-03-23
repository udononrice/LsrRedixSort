using System;
using System.Collections.Generic;

namespace LeetCodeConsole
{
    class Program
    {
        // Части кода с комментарием необходимо вынести в методы, но в данном случае для примера они не вынесены.
        public static int[] RedixSort(int[] nums)
        {
            var groups = new List<List<int>>();
            for (int i = 0; i < 10; i++)
            {
                groups.Add(new List<int>());
            }

            int length = GetMaxLength(nums);

            for(int step = 0; step < length; step++)
            {
                // Распределение элементов по корзинам.
                foreach (var item in nums)
                {
                    var i = item;
                    var value = i % (int)Math.Pow(10, step + 1) / (int)Math.Pow(10, step);
                    groups[value].Add(item);
                }

                int[] numsSecond = new int[nums.Length];

                // Сборка элементов.
                int count = 0;
                foreach (var group in groups)
                {
                    for (int i = 0; i < group.Count; i++)
                    {
                        numsSecond[count] = group[i];
                        if (count < numsSecond.Length) count++;
                    }
                }

                nums = numsSecond;

                // Очистка корзин.
                foreach (var group in groups)
                {
                    group.Clear();
                }
            }
            return nums;
        }

        private static int GetMaxLength(int[] nums)
        {
            int length = 0;
            foreach (var item in nums)
            {
                if (item < 0)
                {
                    throw new ArgumentException("Поразрядная сортировка поддерживает только целые числа от 0 до 9");
                }

                // var l = Convert.ToInt32(Math.Log10(item) + 1);
                // т.к. при значении 0 выходит ошибка и получается значение -infinity, пришлось сделать по другому.
                var l = item.GetHashCode().ToString().Length;
                if (l > length)
                {
                    length = l;
                }
            }
            return length;
        }

        public static void Main()
        {
            Random random = new Random();
            int[] nums = new int[10];

            for (int i = 0; i < nums.Length; i++)
                nums[i] = random.Next(100);

            nums = RedixSort(nums);

            foreach (int e in nums)
                Console.Write($"{e} ");
        }
    }
}
