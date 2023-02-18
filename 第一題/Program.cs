using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 第一題
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //讀取csv
            var text = File.ReadAllLines(@"..\..\..\product.csv").Skip(1).ToList();
            //csv內容加入Class Product
            List<Product> list = new List<Product>();
            foreach (var line in text)
            {
                list.AddRange(CreateList(line));
            }
            //分群顯示與偵測輸出入
            string detect;
            do
            {
                Console.WriteLine("1. 計算所有商品的總價格\r\n2. 計算所有商品的平均價格\r\n3. 計算商品的總數量\r\n4. 計算商品的平均數量\r\n5. 找出哪一項商品最貴\r\n6. 找出哪一項商品最便宜\r\n7. 計算產品類別為 3C 的商品總價\r\n8. 計算產品類別為飲料及食品的商品價格\r\n9. 找出所有商品類別為食品，而且商品數量大於 100 的商品\r\n10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品\r\n11. 呈上題，請計算該類別底下所有商品的平均價格\r\n12. 依照商品價格由高到低排序\r\n13. 依照商品數量由低到高排序\r\n14. 找出各商品類別底下，最貴的商品\r\n15. 找出各商品類別底下，最貴的商品\r\n16. 找出價格小於等於 10000 的商品\r\n17. 製作一頁 4 筆總共 5 頁的分頁選擇器");
                Console.Write("輸入[1-17]之間的數字選擇功能(輸入[-1]離開):");
                detect = Console.ReadLine();
                Console.Clear();
                var counter = list.Select(x => x.商品數量).ToList();
                var price = list.Select(x => x.價格).ToList();
                decimal total = 0;
                switch (detect)
                {
                    case "1":
                        for (int i = 0; i < counter.Count; i++)
                        {
                            total += counter[i] * price[i];
                        }
                        Console.WriteLine($"所有商品的總價格 ${total}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        for (int i = 0; i < counter.Count; i++)
                        {
                            total += counter[i] * price[i];
                        }
                        Console.WriteLine($"所有商品的平均價格 {total/ counter.Sum()}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine($"商品的總數量 {list.Select(x => x.商品數量).Sum()}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine($"商品的平均數量 {list.Select(x => x.商品數量).Average()}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine($"商品最貴 {list.OrderByDescending(x => x.價格).GroupBy(x => x.價格).First().Select(x => x.商品名稱).First()}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "6":
                        Console.WriteLine($"商品最便宜 {list.OrderBy(x => x.價格).GroupBy(x => x.價格).First().Select(x => x.商品名稱).First()}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7":
                        var counter7 = list.Where(x => x.商品類別 == "3C").Select(x => x.商品數量).ToList();
                        var price7 = list.Where(x => x.商品類別 == "3C").Select(x => x.價格).ToList();
                        for (int i = 0; i < counter7.Count; i++)
                        {
                            total += counter7[i] * price7[i];
                        }
                        Console.WriteLine($"3C 的商品總價 {total}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "8":
                        var counter8 = list.Where(x => x.商品類別 == "飲料" || x.商品類別 == "食品").Select(x => x.商品數量).ToList();
                        var price8 = list.Where(x => x.商品類別 == "飲料" || x.商品類別 == "食品").Select(x => x.價格).ToList();
                        for (int i = 0; i < counter8.Count; i++)
                        {
                            total += counter8[i] * price8[i];
                        }
                        Console.WriteLine($"飲料及食品的商品價格 {total}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "9":
                        Console.WriteLine($"類別為食品，且數量大於100 {String.Join(" ", list.Where(x => x.商品類別 == "食品" && x.商品數量 > 100).Select(x => x.商品名稱).ToList().Distinct())}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "10":
                        var max10 = list.Where(x => x.價格 > 1000).GroupBy(x => x.商品類別);
                        foreach (var item in max10)
                        {
                            Console.WriteLine($"商品的價格是大於 1000 {item.Select(x => x.商品類別).First()} {String.Join(" ", item.Select(x => x.商品名稱).ToList().Distinct())}");
                        }
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "11":
                        var avg11 = list.Where(x => x.價格 > 1000).GroupBy(x => x.商品類別);
                        foreach (var item in avg11)
                        {
                            var counter11 = item.Select(x => x.商品數量).ToList();
                            var price11 = item.Select(x => x.價格).ToList();
                            for (int i = 0; i < counter11.Count; i++)
                            {
                                total += counter11[i] * price11[i];
                            }
                            Console.WriteLine($"該類別底下所有商品的平均價格 {item.Select(x => x.商品類別).First()} {total/ counter11.Sum()}");
                        }
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "12":
                        Console.WriteLine($"商品價格由高到低 {String.Join(" ", list.OrderByDescending(x => x.價格).Select(x => x.商品名稱).ToList().Distinct())}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "13":
                        Console.WriteLine($"商品數量由低到高 {String.Join(" ", list.OrderBy(x => x.商品數量).Select(x => x.商品名稱).ToList().Distinct())}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "14":
                        var max14 = list.OrderByDescending(x => x.價格).GroupBy(x => x.商品類別);
                        foreach (var item in max14)
                        {
                            Console.WriteLine($"各商品類別底下，最貴的商品 {String.Join(" ", item.GroupBy(x => x.價格).First().Select(x => x.商品類別).ToList().Distinct())} {String.Join(" ", item.GroupBy(x => x.價格).First().Select(x => x.商品名稱).ToList().Distinct())}");
                        }
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "15":
                        var min15 = list.OrderBy(x => x.價格).GroupBy(x => x.商品類別);
                        foreach (var item in min15)
                        {
                            Console.WriteLine($"各商品類別底下，最便宜的商品 {String.Join(" ", item.GroupBy(x => x.價格).First().Select(x => x.商品類別).ToList().Distinct())} {String.Join(" ", item.GroupBy(x => x.價格).First().Select(x => x.商品名稱).ToList().Distinct())}");
                        }
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "16":
                        Console.WriteLine($"價格小於等於 10000 的商品 {String.Join(" ", list.Where(x => x.價格 <= 10000).Select(x => x.商品名稱).ToList().Distinct())}");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "17":
                        string detect17;
                        do
                        {
                            Console.WriteLine("輸入[1-5]選擇頁數(輸入[-1]回到選單):");
                            detect17 = Console.ReadLine();
                            Console.Clear();
                            var display1 = list.OrderBy(x => x.商品編號).Take(4);
                            var display2 = list.OrderBy(x => x.商品編號).Skip(4).Take(4);
                            var display3 = list.OrderBy(x => x.商品編號).Skip(4).Skip(4).Take(4);
                            var display4 = list.OrderBy(x => x.商品編號).Skip(4).Skip(4).Skip(4).Take(4);
                            var display5 = list.OrderBy(x => x.商品編號).Skip(4).Skip(4).Skip(4).Skip(4).Take(4);
                            switch (detect17)
                            {
                                case "1":
                                    foreach (var arr in display1)
                                    {
                                        Console.WriteLine($"商品編號:{arr.商品編號}");
                                        Console.WriteLine($"商品名稱:{arr.商品名稱}");
                                        Console.WriteLine($"商品數量:{arr.商品數量}");
                                        Console.WriteLine($"價格:{arr.價格}");
                                        Console.WriteLine($"商品類別{arr.商品類別}\n");
                                    }
                                    Console.WriteLine("\n按下任意鍵繼續");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "2":
                                    foreach (var arr in display2)
                                    {
                                        Console.WriteLine($"商品編號:{arr.商品編號}");
                                        Console.WriteLine($"商品名稱:{arr.商品名稱}");
                                        Console.WriteLine($"商品數量:{arr.商品數量}");
                                        Console.WriteLine($"價格:{arr.價格}");
                                        Console.WriteLine($"商品類別{arr.商品類別}\n");
                                    }
                                    Console.WriteLine("\n按下任意鍵繼續");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "3":
                                    foreach (var arr in display3)
                                    {
                                        Console.WriteLine($"商品編號:{arr.商品編號}");
                                        Console.WriteLine($"商品名稱:{arr.商品名稱}");
                                        Console.WriteLine($"商品數量:{arr.商品數量}");
                                        Console.WriteLine($"價格:{arr.價格}");
                                        Console.WriteLine($"商品類別{arr.商品類別}\n");
                                    }
                                    Console.WriteLine("\n按下任意鍵繼續");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "4":
                                    foreach (var arr in display4)
                                    {
                                        Console.WriteLine($"商品編號:{arr.商品編號}");
                                        Console.WriteLine($"商品名稱:{arr.商品名稱}");
                                        Console.WriteLine($"商品數量:{arr.商品數量}");
                                        Console.WriteLine($"價格:{arr.價格}");
                                        Console.WriteLine($"商品類別{arr.商品類別}\n");
                                    }
                                    Console.WriteLine("\n按下任意鍵繼續");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "5":
                                    foreach (var arr in display5)
                                    {
                                        Console.WriteLine($"商品編號:{arr.商品編號}");
                                        Console.WriteLine($"商品名稱:{arr.商品名稱}");
                                        Console.WriteLine($"商品數量:{arr.商品數量}");
                                        Console.WriteLine($"價格:{arr.價格}");
                                        Console.WriteLine($"商品類別{arr.商品類別}\n");
                                    }
                                    Console.WriteLine("\n按下任意鍵繼續");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "-1":
                                    break;
                                default:
                                    Console.WriteLine("輸入錯誤，查無指令");
                                    Console.WriteLine("\n按下任意鍵繼續");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                            }
                        } while (detect17 != "-1");
                        break;
                    case "-1":
                        break;
                    default:
                        Console.WriteLine("輸入錯誤，查無指令");
                        Console.WriteLine("\n按下任意鍵繼續");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (detect != "-1");
        }
        static List<Product> CreateList(string csvLine)
        {
            string[] values = csvLine.Split(',');
            return new List<Product>()
            {
                new Product{ 商品編號 = Convert.ToString(values[0]),
                    商品名稱 = Convert.ToString(values[1]),
                    商品數量 = Convert.ToInt32(values[2]),
                    價格 = Convert.ToDecimal(values[3]),
                    商品類別 = Convert.ToString(values[4]),
                    }
            };
        }
    }
}
