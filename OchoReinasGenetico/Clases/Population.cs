using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OchoReinasGenetico.Clases
{
    public class Population
    {
        private Chromosome[] chromosomes;

        public Population(int size)
        {
            this.chromosomes = new Chromosome[size];
            initializePopulation();
        }

        public void calculateFitness()
        {
            for (int i = 0; i < this.chromosomes.Length; i++)
            {
                this.chromosomes[i].calculateFitness();
            }
            sortPopulationBasedOnFitness();
        }

        private class CompareChromosomesFitness : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                Chromosome c1 = (Chromosome)x;
                Chromosome c2 = (Chromosome)y;
                int flag = 0;
                    if (c1.getFitness() > c2.getFitness())
                        flag = -1;
                    else if (c1.getFitness() < c2.getFitness())
                        flag = 1;
                    return flag;
            }
        }

        public void sortPopulationBasedOnFitness()
        {
            CompareChromosomesFitness compare = new CompareChromosomesFitness();
            Array.Sort(this.chromosomes, compare);
        }


        public Chromosome getChromosome(int index)
        {
            return this.chromosomes[index];
        }

        public void setChromosome(int index, Chromosome chromosome)
        {
            this.chromosomes[index] = chromosome;
        }

        public string[] displayPopulation()
        {
            string[] array = new string [this.chromosomes.Length];
            int count = 0;
            foreach (Chromosome chromosome in this.chromosomes)
            {
                array[count] = chromosome.displayChromosome();
                count++;
            }
            return array;
        }

        private void initializePopulation()
        {
            for (int i = 0; i < this.chromosomes.Length; i++)
            {
                this.chromosomes[i] = new Chromosome();
                this.chromosomes[i].generateChromosome();
            }
        }


    }
}
