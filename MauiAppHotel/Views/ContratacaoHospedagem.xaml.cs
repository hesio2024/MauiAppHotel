using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
	App PropriedadesApp;
	public ContratacaoHospedagem()
	{
		InitializeComponent();

		PropriedadesApp = (App)Application.Current;

		pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

		dtpck_chechin.MinimumDate = DateTime.Now;
		dtpck_chechin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day);

		dtpck_chechout.MinimumDate = dtpck_chechin.Date.AddDays(1);
		dtpck_chechout.MaximumDate = dtpck_chechin.Date.AddMonths(6);
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			Hospedagem h = new Hospedagem
			{
				QuartoSelecionado = (Quarto) pck_quarto.SelectedItem,
				QntAdultos = Convert.ToInt32(stp_adultos.Value),
				QntCriancas = Convert.ToInt32(stp_crianças.Value),
				DataCheckIn = dtpck_chechin.Date,
				DataCheckOut = dtpck_chechout.Date,

			};

			await Navigation.PushAsync(new HospedagemContratada()
			{
				BindingContext = h,
			});

		} catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		
    }

    private void dtpck_chechin_DateSelected(object sender, DateChangedEventArgs e)
    {
		DatePicker elemento = sender as DatePicker;

		DateTime data_selecionada_checkin = elemento.Date;

		dtpck_chechout.MinimumDate = data_selecionada_checkin.AddDays(1);
		dtpck_chechout.MaximumDate = data_selecionada_checkin.AddMonths(6);
    }
}