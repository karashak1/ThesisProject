using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NeuralNetwork {
    class NetworkLayer {
        public Neuron[] layer { get; private set; }

        public NetworkLayer(int inputSize, int neuronCount){
            layer = new Neuron[neuronCount];
            Random ran = new Random();
            for (int x = 0; x < layer.Length; x++)
                layer[x] = new Neuron(inputSize+1, ran);
        }

        public double[] calculateOutput(double[] input) {
            double[] output = new double[layer.Length];
            for (int x = 0; x < layer.Length; x++) {
                output[x] = layer[x].calculateOutput(input);
            }
            return output;
        }

        public static double SigmoidFunction(double k) {
            double x;
            x = 1.0 / (1.0 + Math.Exp(-k));
            return x;
        }
    }

    class Neuron {

        public Neuron(int size, Random ran) {
            weights = new double[size];
            for (int x = 0; x < weights.Length; x++)
                weights[x] = ran.NextDouble();
        }

        public double calculateOutput(double[] input) {
            double output = 0;
            for (int x = 0; x < weights.Length-1; x++) {
                output += weights[x] * input[x];
            }
            output += (-weights[weights.Length-1]) * input[input.Length-1];
            return output;
        }

        public double[] weights { get; private set; }
    }

    class ANNetwork {
        public List<NetworkLayer> layers { get; private set; }
        public NetworkLayer input { get; private set; }
        public NetworkLayer output { get; private set; }
        

        public ANNetwork() {
            layers = new List<NetworkLayer>();
            input = output = null;
        }

        public void addLayer(NetworkLayer layer) {
            layers.Add(layer);
        }

    }


}
