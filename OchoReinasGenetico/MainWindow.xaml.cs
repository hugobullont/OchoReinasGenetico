using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OchoReinasGenetico.Clases;
using System.Threading;

namespace OchoReinasGenetico
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image queenImage;
        private int population;
        private GeneticAlgo geneticAlgo;
        private int generation;

        private string generationString = "Generaciones : \n";

        public MainWindow()
        {
            InitializeComponent();
            queenImage = new Image();
            queenImage.Source = new BitmapImage(new Uri(@"D:\queen.png"));
            generation = 1;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.population = int.Parse(PopulationTextBox.Text);

            geneticAlgo = new GeneticAlgo(this.population);

            this.ejecutarAlgoritmo();
        }

        private void imprimirReina(int fila, int columna)
        {
            Button button = (Button)ChessGrid.Children
            .Cast<UIElement>()
            .First(e => Grid.GetRow(e) == fila && Grid.GetColumn(e) == columna);
            button.Content = queenImage;
        }

        private void agregarCromosoma(Chromosome chromo)
        {
            this.generationString = this.generationString + generation +": " + chromo.displayChromosome() + "\n";
            this.imprimirConsola();
        }

        private void imprimirConsola()
        {
            textConsole.Text = this.generationString;
        }

        private void ejecutarAlgoritmo()
        {
            while(geneticAlgo.getFittestChromosome().getFitness() < 0)
            {
                geneticAlgo.naturalSelection();
                this.buscarPosicionesReinas(geneticAlgo.getFittestChromosome());
                this.agregarCromosoma(geneticAlgo.getFittestChromosome());
                generation++;
            }
            this.done();
        }

        private void done()
        {
            this.generationString = this.generationString + "TERMINADO";
            this.imprimirConsola();
        }

        private void buscarPosicionesReinas(Chromosome chromo)
        {
            for(int i=0;i<8;i++)
            {
                this.imprimirReina(chromo.getGenes(i), i);
            }
        }
    }
}
