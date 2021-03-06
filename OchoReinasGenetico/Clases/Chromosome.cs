﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OchoReinasGenetico.Clases
{
    public class Chromosome
    {
        private int[] genes;
        private int fitness;
        public int LENGTH = 8;

        public Chromosome()
        {
            this.genes = new int[this.LENGTH];
            this.fitness = -100;
        }

        public void generateChromosome()
        {
            Random random = new Random();
            for(int i = 0; i < this.LENGTH; i++)
            {
                this.genes[i] = random.Next(this.LENGTH);
            }
        }

        public void calculateFitness()
        {
            int conflicts = 0;
            for (int i = 0; i<this.LENGTH; i++)
            {
                conflicts = conflicts + getVerticalHorizontalConflicts(i) + getDiagonalConflicts(i);
            }
            this.fitness = -1 * conflicts;
        }

        public int getFitness()
        {
            return this.fitness;
        }

        public int getGenes(int index)
        {
            return this.genes[index];
        }

        public void setGenes(int index, int gene)
        {
            this.genes[index] = gene;
        }

        public string displayChromosome()
        {
            string g = "[ ";
            for(int i=0;i<8;i++)
            {
                g = g + getGenes(i).ToString() + " ";
            }
            g = g + "]";
            return g;
        }

        private int getVerticalHorizontalConflicts(int index)
        {
            int conflicts = 0;
            for (int i = 0; i < this.LENGTH; i++)
            {
                if (i == index)
                    continue;
                if (this.genes[i] == this.genes[index])
                    conflicts++;
            }
            return conflicts;
        }

        private int getDiagonalConflicts(int index)
        {
            int conflicts = 0;
            int queenRow = index;
            int queenCol = this.genes[index];
            int offset = (queenRow >= queenCol) ? queenCol : queenRow;
            int topLeftRow = queenRow - offset;
            int topLeftCol = queenCol - offset;
            while (topLeftCol < this.LENGTH && topLeftRow < this.LENGTH
                    && topLeftCol >= 0 && topLeftRow >= 0)
            {
                if (topLeftCol == this.genes[topLeftRow] &&
                        topLeftCol != queenCol && topLeftRow != queenRow)
                    conflicts++;
                topLeftCol++;
                topLeftRow++;
            }

            offset = (queenRow >= (this.LENGTH - queenCol - 1)) ? (this.LENGTH - queenCol - 1) : queenRow;
            int topRightRow = queenRow - offset;
            int topRightCol = queenCol + offset;
            while (topRightCol < this.LENGTH && topRightRow < this.LENGTH
                    && topRightCol >= 0 && topRightRow >= 0)
            {
                if (topRightCol == this.genes[topRightRow] &&
                        topRightCol != queenCol && topRightRow != queenRow)
                    conflicts++;
                topRightCol--;
                topRightRow++;
            }

            return conflicts;
        }
    }
}
