using Proyecto1.clases;
using Proyecto1.model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrarse : ContentPage
    {
        public Registrarse()
        {
            InitializeComponent();
        }

        private void Salvar_Clicked(object sender, EventArgs e)
        {
            Guardar();
        }
        public async void Guardar()
        {
            
            if (string.IsNullOrEmpty(_usuario.Text))
            {
                await DisplayAlert("Lista", "Usuario vacio", "ok");
                return;
            }
            if (string.IsNullOrEmpty(_nombre.Text))
            {
                await DisplayAlert("Lista", "nombre vacio", "ok");
                return;
            }
            if (string.IsNullOrEmpty(_password.Text))
            {
                await DisplayAlert("Lista", "contrasenia vacia", "ok");
                return;
            }
            try
            {
                registro.IsRunning = true;

                Conexion conn = new Conexion();
                var login = new Login()
                {
                    id = 0,
                    usuario = _usuario.Text,
                    password =_password.Text,
                    nombre = _nombre.Text

                };

                conn.Conn().CreateTable<Login>();
                conn.Conn().Insert(login);
                await DisplayAlert("Success", "Usuario Guardado", "Ok");
                

            }
            catch (SQLiteException e)
            {
                await DisplayAlert("Warning", "Error al guardar", "Ok");

            }
            registro.IsRunning = false;
        }
    }
}