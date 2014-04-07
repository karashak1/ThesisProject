using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NeuralNetwork {
    class NetworkLayer {
        public Neuron[] layer { get; private set; }

        public NetworkLayer(int neuronCount){
            layer = new Neuron[neuronCount];
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

        public Neuron(int size) {
            weights = new double[size + 1];
        }

        public double calculateOutput(double[] input) {
            double output = 0;
            for (int x = 0; x < weights.Length; x++) {
                output += weights[x] * input[x];
            }
            return output;
        }

        public double[] weights { get; private set; }
    }

    class ANNetwork {
        public IList<NetworkLayer> layers { get; private set; }

        private NetworkLayer input { get; private set; }

        private NetworkLayer output { get; private set; }

        public ANNetwork() {

        }

        public void addLayer(NetworkLayer layer) {
            layers.Add(layer);
        }

    }


}
