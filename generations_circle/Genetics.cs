using System;
using System.Drawing;

namespace generations_circle
{
    public delegate void GeneticsActionInfoHandler(string info);

    public class Genetics
    {
        public event GeneticsActionInfoHandler ActionInfo;

        private Graphics g;
        Random rnd = new Random();

        public static float CROSS_RATE = 1.0f;
        public float MUTATION_RATE = 0.02f;
        public float MAX_MUTATION = 0.3f;
        public float MIN_MUTATION = 0.01f;
        public static int POPULATION_SIZE = 80;
        public static int GENERATIONS_NUM = 1500;
        public double[][] population;
        public int[][] zaznam;

        int[] ptsX = {127, 41, 54, 76, 82, 125, 30, 128, 68, 34, 29, 95, 30, 34, 128, 93, 45, 29,
        105, 40, 39, 106, 55, 120, 117, 129, 128, 92, 125, 125, 69, 117, 32, 125, 91, 116, 108, 127,
        58, 88, 119, 51, 123, 126, 100, 60, 29, 37, 121, 45, 77, 96, 30, 119, 111, 30, 112, 129, 109,
        108, 84, 130, 54, 127, 65, 47, 30, 79, 121, 92, 101, 92, 111, 45, 129, 128, 107, 52, 116, 54,
        84, 109, 108, 48, 39, 42, 48, 117, 44, 31, 29, 128, 33, 86, 129, 110, 118, 101, 129, 63, 69,
        82, 88, 80, 133, 129, 103, 70, 121, 129, 70, 92, 78, 118, 133, 115, 97, 89, 122, 134, 86, 119,
        71, 93, 125, 135, 107, 75, 127, 79, 135, 108, 70, 68, 103, 73, 70, 83, 95, 135, 100, 73, 134,
        125, 77, 72, 93, 122, 133, 131, 88, 121, 120, 136, 71, 77, 131, 95, 119, 130, 95, 111, 75, 96,
        135, 136, 115, 113, 76, 79, 129, 133, 74, 120, 123, 87, 126, 82, 135, 74, 119, 81, 131, 125, 76,
        124, 132, 71, 92, 122, 136, 132, 129, 120, 113, 136, 136, 70, 69, 99, 31, 82, 153, 125, 23, 47,
        131, 75, 25, 149, 20, 21, 20, 156, 27, 60, 45, 32, 141, 78, 26, 123, 47, 104, 157, 77, 148, 21,
        139, 32, 65, 88, 37, 30, 127, 27, 35, 149, 134, 113, 98, 51, 127, 143, 84, 46, 106, 21, 106, 156,
        137, 130, 156, 21, 116, 64, 25, 65, 126, 26, 27, 89, 122, 21, 84, 122, 19, 39, 157, 29, 139, 80,
        142, 140, 143, 64, 22, 152, 158, 74, 151, 156, 119, 152, 82, 24, 20, 138, 49, 134, 47, 122, 27,
        128, 151, 114, 135, 35, 120, 96};

        int[] ptsY = {89, 67, 141, 50, 149, 81, 92, 110, 149, 120, 104, 146, 89, 77, 90, 148, 63, 105,
        142, 69, 69, 58, 144, 72, 66, 103, 107, 51, 80, 78, 50, 131, 85, 119, 149, 133, 59, 114, 54, 50,
        128, 140, 123, 119, 145, 146, 97, 73, 127, 63, 51, 51, 105, 131, 138, 88, 61, 97, 59, 137, 150,
        95, 57, 87, 54, 62, 92, 150, 128, 148, 54, 51, 138, 135, 109, 92, 141, 142, 134, 142, 50, 60, 58,
        61, 128, 133, 62, 67, 134, 84, 97, 110, 119, 148, 93, 61, 69, 55, 94, 52, 65, 35, 31, 87, 74, 81,
        95, 52, 90, 83, 72, 29, 38, 32, 48, 93, 95, 92, 88, 51, 32, 91, 50, 30, 87, 59, 95, 82, 85, 39, 60,
        29, 55, 59, 28, 45, 70, 34, 29, 58, 27, 45, 53, 38, 84, 47, 95, 89, 76, 45, 92, 89, 33, 57, 51, 39,
        43, 94, 89, 80, 29, 93, 42, 95, 60, 62, 31, 94, 83, 87, 41, 75, 42, 89, 89, 32, 38, 35, 68, 43, 90,
        88, 79, 36, 83, 87, 46, 77, 30, 35, 61, 46, 81, 90, 94, 65, 60, 50, 61, 28, 39, 146, 54, 136, 59,
        131, 24, 10, 106, 46, 64, 59, 88, 66, 49, 16, 130, 39, 122, 145, 47, 136, 134, 144, 68, 144, 112,
        75, 30, 38, 143, 145, 121, 42, 133, 107, 35, 46, 26, 12, 145, 135, 133, 38, 146, 130, 143, 68, 13,
        86, 30, 132, 85, 72, 16, 14, 103, 13, 22, 104, 50, 9, 137, 84, 9, 138, 77, 125, 75, 44, 34, 146, 34,
        123, 36, 142, 91, 53, 74, 11, 51, 91, 138, 50, 145, 52, 86, 30, 133, 28, 131, 16, 47, 22, 105, 141,
        28, 119, 17, 10};

        public Genetics(Graphics g)
        {
            this.g = g;

            InitArrays();
        }

        public void InitArrays()
        {
            population = new double[POPULATION_SIZE][];
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                population[i] = new double[9];
            }

            zaznam = new int[GENERATIONS_NUM][];
            for (int i = 0; i < GENERATIONS_NUM; i++)
            {
                zaznam[i] = new int[POPULATION_SIZE];
            }
        }

        private void RaiseActionInfo(string txt)
        {
            if (ActionInfo != null)
            {
                ActionInfo(txt);
            }
        }

        #region paint

        public void PaintAll()
        {

            PaintPopulation();

            PaintBestIndividual();

            PaintDots();

            PaintPopulationGraph();
        }

        public void PaintSinglePopulationGraph(int i)
        {
            for (int j = 1; j < POPULATION_SIZE; j++)
            {
                if (zaznam[i][j] > 0) g.DrawEllipse(Pens.LightGray, i, 450 - zaznam[i][j], 1, 1);
            }
            if (zaznam[i][0] > 0)
            {
                g.DrawEllipse(Pens.Red, i, 450 - zaznam[i][0], 1, 1);
            }

            g.DrawEllipse(Pens.Green, i, 400 - (MUTATION_RATE * 100), 1, 1);
        }

        public void PaintPopulationGraph()
        {
            for (int i = 0; i < GENERATIONS_NUM; i++)
            {
                for (int j = 1; j < POPULATION_SIZE; j++)
                {
                    if (zaznam[i][j] > 0) g.DrawEllipse(Pens.LightGray, i, 450 - zaznam[i][j], 1, 1);
                }
                if (zaznam[i][0] > 0)
                {
                    g.DrawEllipse(Pens.Red, i, 450 - zaznam[i][0], 1, 1);
                }

                g.DrawEllipse(Pens.Green, i, 400 - (MUTATION_RATE * 100), 1, 1);
            }
        }

        public void PaintPopulation()
        {
            for (int i = 1; i < POPULATION_SIZE; i++)
            {
                g.DrawEllipse(Pens.Gray, (int)(population[i][0] - population[i][2]), (int)(population[i][1] - population[i][2]), (int)(population[i][2] * 2), (int)(population[i][2] * 2));
                g.DrawEllipse(Pens.Gray, (int)(population[i][3] - population[i][5]), (int)(population[i][4] - population[i][5]), (int)(population[i][5] * 2), (int)(population[i][5] * 2));
                g.DrawEllipse(Pens.Gray, (int)(population[i][6] - population[i][8]), (int)(population[i][7] - population[i][8]), (int)(population[i][8] * 2), (int)(population[i][8] * 2));
            }
        }

        public void PaintBestIndividual()
        {
            g.DrawEllipse(Pens.Red, (int)(population[0][0] - population[0][2]), (int)(population[0][1] - population[0][2]), (int)(population[0][2] * 2), (int)(population[0][2] * 2));
            g.DrawEllipse(Pens.Red, (int)(population[0][3] - population[0][5]), (int)(population[0][4] - population[0][5]), (int)(population[0][5] * 2), (int)(population[0][5] * 2));
            g.DrawEllipse(Pens.Red, (int)(population[0][6] - population[0][8]), (int)(population[0][7] - population[0][8]), (int)(population[0][8] * 2), (int)(population[0][8] * 2));
        }

        public void PaintDots()
        {
            for (int i = 0; i < ptsX.Length; i++)
            {
                g.FillRectangle(Brushes.Black, ptsX[i], ptsY[i], 1, 1);
            }
        }

        #endregion

        #region genetics

        public double Fitness(double[] individuum)
        {
            double d1, d2, d3, dx, dy, x;
            double quality = 0.0f;
            double sigma = 15.0;
            for (int i = 0; i < ptsX.Length; i++)
            {
                dx = (ptsX[i] - individuum[0]);
                dy = (ptsY[i] - individuum[1]);
                d1 = Math.Abs(Math.Sqrt(dx * dx + dy * dy) - individuum[2]);
                dx = (ptsX[i] - individuum[3]);
                dy = (ptsY[i] - individuum[4]);
                d2 = Math.Abs(Math.Sqrt(dx * dx + dy * dy) - individuum[5]);
                dx = (ptsX[i] - individuum[6]);
                dy = (ptsY[i] - individuum[7]);
                d3 = Math.Abs(Math.Sqrt(dx * dx + dy * dy) - individuum[8]);
                x = Math.Min(Math.Min(d1, d2), d3);
                quality += Math.Exp(-(x * x) / (2.0 * sigma * sigma));
            }
            return quality;
        }

        public int PickRandomIndividuum(double[] arFitness, double sumFitness)
        {
            double sum = 0.0;
            double fitnessPoint = rnd.Next(0, (int)sumFitness);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                sum += arFitness[i];
                if (sum > fitnessPoint) return i;
            }
            return 0;
        }

        #endregion

        public void Start()
        {
            // inicializujeme populaci
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                for (int j = 0; j < 9; j++) population[i][j] = rnd.Next(0, 100);
            }

            double newBestFitness = 0.0f;

            double[] arFitness = new double[POPULATION_SIZE];
            for (int iGeneration = 0; iGeneration < GENERATIONS_NUM; iGeneration++)
            {
                double bestFitness = 0.0f;
                int bestFitnessIdx = 0;

                double[][] newPopulation = new double[POPULATION_SIZE][];
                for (int i = 0; i < POPULATION_SIZE; i++) newPopulation[i] = new double[9];

                double minFitness = double.MaxValue;

                // vypocteme fitness vsech jedincu v populaci
                for (int i = 0; i < POPULATION_SIZE; i++)
                {
                    arFitness[i] = Fitness(population[i]);
                    if (arFitness[i] > bestFitness)
                    {
                        bestFitness = arFitness[i];
                        bestFitnessIdx = i;

                        //nainicializujem si hodnotu
                        if (newBestFitness == 0)
                        {
                            newBestFitness = bestFitness;
                        }
                    }

                    if (arFitness[i] < minFitness) minFitness = arFitness[i];
                    zaznam[iGeneration][i] = (int)Math.Round(arFitness[i] * 0.66);
                }
                // upravime hodnoty fitness pro lepsi selekci od 0 do bestFitness-minFitness
                double sumFitness = 0.0;
                for (int i = 0; i < POPULATION_SIZE; i++)
                {
                    arFitness[i] -= minFitness;
                    sumFitness += arFitness[i];
                }
                // vytvoreni nove populace, ktera nahradi stavajici
                for (int iIndividuum = 0; iIndividuum < POPULATION_SIZE; iIndividuum += 2)
                {
                    // prirozeny vyber
                    int individuumIdx1 = PickRandomIndividuum(arFitness, sumFitness);
                    int individuumIdx2 = PickRandomIndividuum(arFitness, sumFitness);
                    if (rnd.NextDouble() < CROSS_RATE)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (rnd.NextDouble() < 0.5)
                            {
                                newPopulation[iIndividuum][i] = population[individuumIdx1][i];
                                newPopulation[iIndividuum + 1][i] = population[individuumIdx2][i];
                            }
                            else
                            {
                                newPopulation[iIndividuum][i] = population[individuumIdx2][i];
                                newPopulation[iIndividuum + 1][i] = population[individuumIdx1][i];
                            }
                        }
                        // mutace
                        for (int child = 0; child < 2; child++)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                if (rnd.NextDouble() < MUTATION_RATE)
                                {
                                    double randomValue = 8.0 * Math.Sqrt(-2 * Math.Log(rnd.NextDouble())) * Math.Sin(2 * Math.PI * rnd.NextDouble());
                                    newPopulation[iIndividuum + child][i] += randomValue;
                                    if (newPopulation[iIndividuum + child][i] < 0) newPopulation[iIndividuum + child][i] = 0;
                                    else if (newPopulation[iIndividuum + child][i] > 100) newPopulation[iIndividuum + child][i] = 100;
                                }
                            }
                        }
                    }
                    else
                    { // neni krizeni
                        for (int i = 0; i < 9; i++)
                        {
                            newPopulation[iIndividuum][i] = population[individuumIdx1][i];
                            newPopulation[iIndividuum + 1][i] = population[individuumIdx2][i];
                        }
                    }
                }

                //kontrola ci sme nasli lepsieho jedinca, ak nie zvysime mutaci

                for (int i = 0; i < POPULATION_SIZE; i++)
                {
                    if (Fitness(newPopulation[i]) > newBestFitness) newBestFitness = Fitness(newPopulation[i]);
                }

                if (newBestFitness > bestFitness)
                {
                    MUTATION_RATE = MIN_MUTATION;
                }
                else
                {
                    MUTATION_RATE += 0.01f;
                }

                if (MUTATION_RATE > MAX_MUTATION)
                {
                    MUTATION_RATE = MIN_MUTATION;
                }

                // elitismus - zachovani nejlepsiho
                for (int i = 0; i < 9; i++)
                {
                    newPopulation[0][i] = population[bestFitnessIdx][i];
                }

                population = newPopulation;

                RaiseActionInfo("-Generation " + iGeneration + ", Fitness: " + bestFitness + " before Fitness: " + newBestFitness + Environment.NewLine);

                PaintPopulation();

                PaintBestIndividual();

                PaintDots();

                PaintSinglePopulationGraph(iGeneration);

                //Thread.Sleep(1000);
            }
        }
    }
}
