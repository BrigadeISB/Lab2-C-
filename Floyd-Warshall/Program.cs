using System;
using System.Collections.Generic;

class Program
{
    static readonly int numVertices = 11;

    static void FloydWarshall(List<List<double>> graph, List<List<int>> next)
    {
        for (int k = 0; k < numVertices; k++)
        {
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    if (graph[i][k] != -1 && graph[k][j] != -1 &&
                        (graph[i][j] == -1 || graph[i][k] + graph[k][j] < graph[i][j]))
                    {
                        graph[i][j] = graph[i][k] + graph[k][j];
                        next[i][j] = k;
                    }
                }
            }
        }
    }

    static void PrintPath(int i, int j, List<List<int>> next)
    {
        if (next[i][j] == -1)
        {
            Console.Write(j + 1 + " ");
            return;
        }

        Console.Write(next[i][j] + 1 + " -> ");
        PrintPath(next[i][j], j, next);
    }

    static void OutputResult(int startVertex, List<List<double>> graph, List<List<int>> next, int price)
    {
        Console.WriteLine($"\nМiнiмальний шлях для обходу всiх вершин, починаючи з {startVertex} вершини:");
        for (int j = 0; j < numVertices; j++)
        {
            if (j != startVertex)
            {
                Console.Write($"{startVertex + 1} -> {j + 1}: ");
                if (graph[startVertex][j] != -1)
                {
                    Console.Write($"{startVertex + 1} ->");
                    PrintPath(startVertex, j, next);
                }
                else
                {
                    Console.Write("Немає шляху");
                }
                Console.WriteLine($"\t\t || Вiдстань: {graph[startVertex][j]} km \t вартiсть - {graph[startVertex][j] * price} UAH");
            }
        }
    }

    static void Main()
    {
        // iнiцiалiзацiя графу з вагами ребер
        List<List<double>> graph = new List<List<double>>
        {
            new List<double>{ -1, -1, -1, 0.5, -1, -1, 3, -1, -1, -1, -1 },
            new List<double>{ -1, -1, 0.5, -1, -1, 0.4, -1, -1, -1, -1, 0.3 },
            new List<double>{ -1, 0.5, -1, -1, 0.5, 0.2, -1, -1, 0.5, -1, -1 },
            new List<double>{ 0.5, -1, -1, -1, -1, -1, -1, 0.5, 0.5, -1, -1 },
            new List<double>{ -1, -1, 0.5, -1, -1, -1, -1, 0.4, 0.5, 0.3, -1 },
            new List<double>{ -1, 0.4, 0.2, -1, -1, -1, -1, -1, -1, -1, 0.5 },
            new List<double>{ 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new List<double>{ -1, -1, -1, 0.5, 0.4, -1, -1, -1, -1, 0.6, -1 },
            new List<double>{ -1, -1, 0.5, 0.5, 0.5, -1, -1, -1, -1, -1, -1 },
            new List<double>{ -1, -1, -1, -1, 0.3, -1, -1, 0.6, -1, -1, -1 },
            new List<double>{ -1, 0.3, -1, -1, -1, 0.5, -1, -1, -1, -1, -1 }
        };

        // iнiцiалiзацiя матрицi next для вiдстеження шляху
        List<List<int>> next = new List<List<int>>();
        for (int i = 0; i < numVertices; i++)
        {
            next.Add(new List<int>());
            for (int j = 0; j < numVertices; j++)
            {
                next[i].Add(-1);
            }
        }

        FloydWarshall(graph, next);

        // Виведення мiнiмальних вiдстаней для кожної пари вершин
        Console.WriteLine("Мiнiмальнi вiдстанi мiж вершинами:");
        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                Console.Write(graph[i][j] + "\t");
            }
            Console.WriteLine();
        }

        int startVertex = 6; // Вершина, з якої починається обхiд
        int price = 6; // цiна за 1 км у грн/км

        OutputResult(startVertex, graph, next, price);
    }
}
