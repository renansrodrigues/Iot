// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This application uses the Azure IoT Hub device SDK for .NET
// For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples

using System;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace simulatedDevice
{
    class SimulatedDevice
    {
        private static DeviceClient s_deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        private const string s_connectionString = "HostName=IoTHub-Fiap.azure-devices.net;DeviceId=CameraPortaria;SharedAccessKey=UeCRy81pc+BmYE13lHgv0LYrChuKB+dmpH1o4rY3c9U=";

        // Async method to send simulated telemetry
        private static async void SendDeviceToCloudMessagesAsync()
        {
            

                Dados ds = new Dados();

            while (true)
            {
               
                  ds.DataRegistro = DateTime.Now;
                  ds.NomeDispositvo ="CameraPortaria";
                  ds.NumeroPortaria =10;
                  ds.NomePorteiro ="Enzo";

                var messageString = JsonConvert.SerializeObject(ds);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

              
                // Send the tlemetry message
                await s_deviceClient.SendEventAsync(message).ConfigureAwait(false);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000).ConfigureAwait(false);
            }
        }

        private static void Main()
        {
            Console.WriteLine("IoT Hub Quickstarts - Simulated device. Ctrl-C to exit.\n");

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
    }

    public class Dados
    {
         public string NomeDispositvo { get; set; }
         public int NumeroPortaria { get; set; }
        
         public string  NomePorteiro { get; set; }
        public DateTime DataRegistro { get; set; }   

    }
}
