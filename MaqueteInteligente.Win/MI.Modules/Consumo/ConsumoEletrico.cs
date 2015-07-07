using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MI.Modules.Consumo
{
    public static class ConsumoEletrico
    {
        public static Dictionary<string, float> Consumo
        {
            get
            {
                return Consumo == null ? PegarConsumo() : Consumo;
            }
        }

        private static Dictionary<string, float> PegarConsumo()
        {
            if (File.Exists("Eletricidade.consumo"))
                return JsonConvert
                       .DeserializeObject<Dictionary<string, float>>
                       (File.ReadAllText("Eletricidade.consumo"));

            return new Dictionary<string, float>();
        }

        public static void SerializarDados()
        {
            try
            {
                if (!File.Exists("Eletricidade.consumo"))
                    File.Create("Eletricidade.consumo");

                File.WriteAllText("Eletricidade.consumo", JsonConvert.SerializeObject(Consumo));
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Maquete Inteligente",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void AplicarConsumo(string MM_YYYY, float Valor)
        {
            if (Consumo.ContainsKey(MM_YYYY))
                Consumo[MM_YYYY] += Valor;
            else
                Consumo.Add(MM_YYYY, Valor);
        }
    }
}
