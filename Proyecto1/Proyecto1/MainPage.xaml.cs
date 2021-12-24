using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Proyecto1.clases;
using Proyecto1.model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Proyecto1
{
    public partial class MainPage : ContentPage
    {
        double dlatitud, dlongitud;
        ValidarDatos validar = new ValidarDatos();
        byte[] _image;
        public MainPage()
        {
            InitializeComponent();
            locationGPS();


        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            locationGPS();
        }

        public async void locationGPS()
        {

            var location = CrossGeolocator.Current;
            location.DesiredAccuracy = 50;

            if (!location.IsGeolocationEnabled || !location.IsGeolocationAvailable)
            {

                await DisplayAlert("Warning", " GPS no esta activo", "ok");

            }
            else
            {
                if (!location.IsListening)
                {
                    await location.StartListeningAsync(TimeSpan.FromSeconds(10), 1);


                }
                location.PositionChanged += (posicion, args) =>
                {
                    var ubicacion = args.Position;
                    latitud.Text = ubicacion.Latitude.ToString();
                    dlatitud = Convert.ToDouble(latitud.Text);
                    longitud.Text = ubicacion.Longitude.ToString();
                    dlongitud = Convert.ToDouble(longitud.Text);
                };

            }








        }






        public async void guardarUbicacion()
        {

            double _latitud = ((latitud.Text) == null || latitud.Text == "") ? 14.01 : Convert.ToDouble(latitud.Text);
            double _longitud = ((longitud.Text) == null || longitud.Text == "") ? -88.01 : Convert.ToDouble(longitud.Text);

            string _descripcion = (descripcion_larga.Text == "") ? null : descripcion_larga.Text;
            string _descripcion_corta = (descripcion_corta.Text == "") ? null : descripcion_corta.Text;


            if (validar.validarUbicacion(_descripcion))
            {
                await DisplayAlert("Alerta", "Debe describir la ubicacion", "Ok");
                return;
            }
            if (validar.validarDescripcionCorta(_descripcion_corta))
            {
                await DisplayAlert("Alerta", "Debe escribir una ubicacion corta", "Ok");
                return;
            }
            else
            {


                try
                {
                    Crud crud = new Crud();
                    Conexion conn = new Conexion();
                    var ubicacion = new Ubicacion()
                    {
                        id = 0,
                        latitude = Convert.ToDouble(latitud.Text),
                        longitude = Convert.ToDouble(longitud.Text),
                        descripcion_ubicacion = _descripcion,
                        descripcion_corta = _descripcion_corta,
                        foto = _image
                        
                    };

                    conn.Conn().CreateTable<Ubicacion>();
                    conn.Conn().Insert(ubicacion);
                    await DisplayAlert("Success", "Ubicacion Guardada", "Ok");
                    descripcion_corta.Text = "";
                    descripcion_larga.Text = "";
                    await Navigation.PushAsync(new ListaUbicacion());
                }
                catch (SQLiteException e)
                {
                    await DisplayAlert("Warning", "Error al guardar", "Ok");

                }


            }



        }
        private async void buttoncamera_Clicked(object sender, EventArgs e)
        {
            var camera = new StoreCameraMediaOptions();
            camera.PhotoSize = PhotoSize.Full;
            camera.Name = "img";
            camera.Directory = "MiApp";


            var foto = await CrossMedia.Current.TakePhotoAsync(camera);


            if (foto != null)
            {

                imagefile.Source = ImageSource.FromStream(() => {

                    return foto.GetStream();



                });
                imagefile.IsVisible = true;
                using (MemoryStream memory = new MemoryStream())
                {

                    Stream stream = foto.GetStream();
                    stream.CopyTo(memory);
                    _image = memory.ToArray();
                }
            }
        }
        private void Salvar_Clicked(object sender, EventArgs e)
        {
            guardarUbicacion();
        }

        private async void cerrar_Clicked(object sender, EventArgs e)
        {
            
            Boolean af = await DisplayAlert("Cerrar Sesion", "Desea cerrar sesion", "Ok", "Cancel");
            if(af){
                Preferences.Clear();
                await Navigation.PushAsync(new LoginUser(), true);

                NavigationPage.SetHasNavigationBar(this, false);
            }
           
        }

        private async void Salvadas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaUbicacion());
        }
    }
}