//Instalar el paquete Newtonsoft.Json
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_Consumir_API
{
    public partial class Interfaz : Form
    {
        //Declaracion de variables
        string url = "https://ipapi.co/";
        string ip = string.Empty;
        string formato = "/json/";
        string respuesta = string.Empty;
        string direccion = string.Empty;
        public Interfaz()
        {
            InitializeComponent();
        }

        private void Interfaz_Load(object sender, EventArgs e)
        {

        }
        public async Task<string> GetHTTP()
        {
            //Solicitar y obtener la respuesta de la API
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(direccion);

            Request.UserAgent = "ipapi.co/#c-sharp-v1.03";
            HttpWebResponse response = (HttpWebResponse)Request.GetResponse();

            //Leer la respuesta
            StreamReader sr = new StreamReader(response.GetResponseStream());
            //Regresar la respuesta
            return await sr.ReadToEndAsync();
        }

        private async void BT_Buscar_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {   //Comprobar que la textbox de IP contenga datos
                if (TBX_IP.Text != string.Empty)
                {
                    ip = TBX_IP.Text;
                    direccion = url + ip + formato;
                    respuesta = await GetHTTP();

                    //Deserializar el archivo json
                    var j = JsonConvert.DeserializeObject<Atributos>(respuesta);

                    //Rellenar los textbox con la informacion deserializada del archivo json
                    TBX_Version.Text = j.version;
                    TBX_Continente.Text = j.continent_code;
                    TBX_Pais.Text = j.country_name;
                    TBX_Region.Text = j.region;
                    TBX_Ciudad.Text = j.city;
                    TBX_CP.Text = j.postal;
                    TBX_Latitud.Text = j.latitude;
                    TBX_Longitud.Text = j.longitude;
                    TBX_Moneda.Text = j.currency_name;
                }
            }
            catch
            {
                MessageBox.Show("IP no valida");
            }
        }
    }
}
