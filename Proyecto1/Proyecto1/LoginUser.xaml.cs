using Proyecto1.clases;
using Proyecto1.model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUser : ContentPage
    {
        Crud crud = new Crud();
        public LoginUser()
        {
            InitializeComponent();
           
        }



        public async void Validar()
        {
           
            try
            {
                var List = await crud.getReadUsuarios();


                if (string.IsNullOrEmpty(_usuario.Text))
                {
                    await DisplayAlert("Lista", "Usuario vacio", "ok");
                    return;
                }
                if (string.IsNullOrEmpty(_password.Text))
                {
                    await DisplayAlert("Lista", "contrasenia vacia", "ok");
                    return;
                }
                else
                {
                    registro.IsRunning = true;
                    foreach ( var data in List){
                        if (data.usuario.ToLower().Equals(_usuario.Text.ToLower()) && data.password.ToLower().Equals(_password.Text.ToLower()))
                        {
                            Preferences.Set("usuario", _usuario.Text);
                            Preferences.Set("password", _password.Text);
                            await Navigation.PushAsync(new MainPage());
                        }
                       
                    }
                   
                  
                }
            }
            catch (SQLiteException e)
            {
                await DisplayAlert("Login", "Usuario No existe", "ok");


            }
            registro.IsRunning = false;
        }
           

        private void IniciarSesion(object sender, EventArgs e)
        {
            Validar();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrarse());
        }
    }
}