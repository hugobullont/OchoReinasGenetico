﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OchoReinasGenetico.Clases
{

    public class GeneticAlgo
    {
        private Population population;
        private int populationSize;
        public static int ELITE_CHROMOSOME = 1;
        public static int TOURNAMENT_SELECTION_SIZE = 5;
        public static double MUTATE_RATE = 0.1;

        public GeneticAlgo(int populationSize)
        {
            this.populationSize = populationSize;
            this.population = new Population(populationSize);
         
        }
        public void naturalSelection()
        {
            this.population.calculateFitness();
            this.population = generateNextGeneration();
            this.population.calculateFitness();
        }
        public Chromosome getFittestChromosome()
        {
            return this.population.getChromosome(0);
        }

        private Chromosome tournamentSelection()
        {
            Population tournamentPopulation = new Population(TOURNAMENT_SELECTION_SIZE);
            Random random = new Random();
            for (int i = 0; i < TOURNAMENT_SELECTION_SIZE; i++)
            {
                tournamentPopulation.setChromosome(i, this.population.getChromosome(random.Next(this.populationSize)));
            }
            tournamentPopulation.sortPopulationBasedOnFitness();
            return tournamentPopulation.getChromosome(0);
        }
        private Chromosome crossOver(Chromosome parent1, Chromosome parent2)
        {
            Chromosome offspring = new Chromosome();
            Random random = new Random();
            int marker = random.Next(parent1.LENGTH);
            for (int i = 0; i < parent1.LENGTH; i++)
            {
                if (i < marker)
                    offspring.setGenes(i, parent1.getGenes(i));
                else offspring.setGenes(i, parent2.getGenes(i));
            }
            return offspring;
        }

        private Chromosome mutate(Chromosome chromosome)
        {
            Random random = new Random();
            for (int i = 0; i < chromosome.LENGTH; i++)
            {
                if (random.NextDouble() <= MUTATE_RATE)
                {
                    chromosome.setGenes(i, random.Next(chromosome.LENGTH));
                }
            }
            return chromosome;
        }

        private Population generateNextGeneration()
        {
            Population nextGenerationPopulation = new Population(this.populationSize);
            for (int i = 0; i < ELITE_CHROMOSOME; i++)
            {
                nextGenerationPopulation.setChromosome(i, mutate(this.population.getChromosome(i)));
            }
            for (int i = ELITE_CHROMOSOME; i < this.populationSize; i++)
            {
                Chromosome parent1 = tournamentSelection();
                Chromosome parent2 = tournamentSelection();
                nextGenerationPopulation.setChromosome(i, mutate(crossOver(parent1, parent2)));
            }
            return nextGenerationPopulation;
        }
    }
}


