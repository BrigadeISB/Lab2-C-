using System;
using System.Collections.Generic;

class Program
{
    const int SIZE = 11;

    // Функція для знаходження вершини з найменшою відстанню, яка ще не була відвідана
    static int FindMinDistanceVertex(double[] dist, bool[] visited)
    {
        double minDist = double.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < SIZE; ++i)
        {
            if (!visited[i] && dist[i] < minDist)
            {
                minDist = dist[i];
                minIndex = i;
            }
        }

        return minIndex;
    }

    static void OutResult(int startVertex, double[] dist, List<int>[] path)
    {
        // Виводимо результати
        Console.WriteLine($"Distances and paths to all vertices from the vertex {startVertex + 1}: ");
        for (int i = 0; i < SIZE; ++i)
        {
            Console.Write($"Top {i + 1}: ");
            if (dist[i] != double.MaxValue)
            {
                Console.Write($"distance - {dist[i]}; \t way - ");
                for (int j = 0; j < path[i].Count; ++j)
                {
                    Console.Write(path[i][j] + 1); // Додати 1 до індексів для нумерації з 1
                    if (j < path[i].Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("impossible to achieve");
            }
        }
    }

    // Функція для виведення найкоротших шляхів до всіх вершин з використанням алгоритму Дейкстри
    static void Dijkstra(double[,] graph, int startVertex)
    {
        double[] dist = new double[SIZE]; // Масив для зберігання найкоротших відстаней до вершин
        bool[] visited = new bool[SIZE]; // Масив, що вказує, чи була відвідана вершина
        List<int>[] path = new List<int>[SIZE]; // Масив для зберігання шляхів до вершин

        // Ініціалізуємо масиви
        for (int i = 0; i < SIZE; ++i)
        {
            dist[i] = double.MaxValue; // Встановлюємо відстань до всіх вершин на початку як нескінченність
            visited[i] = false; // Жодна вершина ще не відвідана
            path[i] = new List<int>(); // Ініціалізуємо список для шляху до вершини
        }

        // Відстань до стартової вершини завжди 0
        dist[startVertex] = 0;
        path[startVertex].Add(startVertex); // Шлях до початкової вершини

        // Знаходимо найкоротший шлях для кожної вершини
        for (int count = 0; count < SIZE - 1; ++count)
        {
            int u = FindMinDistanceVertex(dist, visited); // Знаходимо вершину з найменшою відстанню
            visited[u] = true; // Позначаємо вершину як відвідану

            // Оновлюємо відстані до сусідніх вершин, якщо вони ще не відвідані і відстань до них через поточну вершину коротша
            for (int v = 0; v < SIZE; ++v)
            {
                if (!visited[v] && graph[u, v] != -1 && dist[u] != double.MaxValue && dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                    path[v] = new List<int>(path[u]); // Копіюємо шлях до вершини u
                    path[v].Add(v); // Додаємо вершину v до шляху
                }
            }
        }

        // Виводимо результати
        Console.WriteLine($"Distances and paths to all vertices from the vertex {startVertex + 1}:");
        for (int i = 0; i < SIZE; ++i)
        {
            Console.Write($"Top {i + 1}: ");
            if (dist[i] != double.MaxValue)
            {
                    
                Console.Write($"\ncost - {dist[i]};\t Way - ");
                for (int j = 0; j < path[i].Count; ++j)
                {
                    //string tmp_vench = ((path[i][j] + 1) != 6 ? "Маршрутка" : "Фунiкулер");
                    Console.Write(path[i][j] + 1); // Додати 1 до індексів для нумерації з 1
                    if (j < path[i].Count - 1)
                    {
                        //Console.Write($"-> {tmp_vench} -> ");
                        Console.Write($" -> ");
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("impossible to achieve");
            }
        }
    }

    static void Main()
    {
        double[,] graph = {
            { -1, -1, -1, 15, -1, -1, 15, -1, -1, -1, -1 },
            { -1, -1, 15, -1, -1, 23, -1, -1, -1, -1, 23 },
            { -1, 15, -1, -1, 15, 23, -1, -1, 15, -1, -1 },
            { 15, -1, -1, -1, -1, -1, -1, 15, 15, -1, -1 },
            { -1, -1, 15, -1, -1, -1, -1, 15, 15, 15, -1 },
            { -1, 23, 23, -1, -1, -1, -1, -1, -1, -1, 23 },
            { 15, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, 15, 15, -1, -1, -1, -1, 15, -1 },
            { -1, -1, 15, 15, 15, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, 15, -1, -1, 15, -1, -1, -1 },
            { -1, 15, -1, -1, -1, 23, -1, -1, -1, -1, -1 }
        };

        // Застосовуємо алгоритм Дейкстри для знаходження найкоротших шляхів від вершини 1
        Dijkstra(graph, 6);
    }
}
