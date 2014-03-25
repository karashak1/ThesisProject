using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork {
    class NetworkLayer {
        public double[] layer { get; private set; }

        public NetworkLayer previous { get; private set; }

        public NetworkLayer next { get;  private set; }

        public NetworkLayer(int neuronCount){
            layer = new double[neuronCount];
        }
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
