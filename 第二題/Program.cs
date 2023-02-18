using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第二題
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string again = "y";
            do
            {
                bool IsWin = false;
                Console.Clear();
                Console.WriteLine("歡迎來到 1A2B 猜數字的遊戲～");
                //電腦輸入
                List<int> temp_list = new List<int> { };
                temp_list.Clear();
                Random rnd = new Random();  //產生亂數初始值
                int temp_num;
                for (int i=0; i<4; i++)
                {
                    do
                    {
                        temp_num = new Random().Next(1, 10);
                    } while (temp_list.Contains(temp_num));
                    temp_list.Add(temp_num);
                }
                //答案測試用
                Console.WriteLine(String.Join(" ", temp_list));
                var computer = CreateList(temp_list);
                do
                {
                    int A = 0;
                    int B = 0;
                    string guess_line = "0123";
                    char[] check = new char[4];
                    //玩家輸入
                    var player = CreateList(temp_list);
                    player.Clear();
                    do
                    {
                        Console.Write("------\n請輸入 4 個數字：");
                        guess_line = Console.ReadLine();
                        check = guess_line.ToCharArray();
                    } while (guess_line.Count()!=4 || check[0] == check[1] || check[0] == check[2] || check[0] == check[3] || check[1] == check[2] || check[1] == check[3] || check[2] == check[3]);
                    temp_list.Clear();
                    foreach (char c in guess_line)
                    {
                        temp_list.Add(Convert.ToInt32(c - '0'));
                    }
                    player = CreateList(temp_list);
                    //計分
                    List<int> lista = new List<int> { };
                    List<int> listb = new List<int> { };
                    foreach (var c in computer)
                    {
                        lista.Add(c.Num1);
                        lista.Add(c.Num2);
                        lista.Add(c.Num3);
                        lista.Add(c.Num4);
                    }
                    foreach (var c in player)
                    {
                        listb.Add(c.Num1);
                        listb.Add(c.Num2);
                        listb.Add(c.Num3);
                        listb.Add(c.Num4);
                    }
                    var union_list = lista.Where(t1 => listb.Contains(t1)).ToList();
                    //Console.WriteLine($"union_list {String.Join(" ",union_list)}");
                    B = union_list.Count;
                    for (int i=0; i<4; i++)
                    {
                        if (lista[i] == listb[i])
                        {
                            A++;
                            B--;
                        }
                    }
                    Console.WriteLine($"判定結果是{A}A{B}B");
                    if (A == 4)
                    {
                        IsWin = true;
                        Console.WriteLine("恭喜你！猜對了！！");
                    }
                } while (!IsWin);
                Console.WriteLine("------\n你要繼續玩嗎？(y/n):");
                again = Console.ReadLine();
            } while (again == "y" || again == "Y");
        }
        static List<Numbers> CreateList(List<int> list)
        {
            return new List<Numbers>()
            {
                new Numbers{ Num1 = list[0], Num2 = list[1], Num3 = list[2], Num4 = list[3],
                    }
            };
        }
    }
}
