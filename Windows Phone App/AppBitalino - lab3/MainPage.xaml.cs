using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging; //bitmaps
using Windows.Storage;
using Windows.Storage.Pickers; //fileopenpicker
using Windows.ApplicationModel.Activation;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Popups;
using Windows.Graphics.Imaging;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AppBitalino___lab3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        App Program = App.Current as App;

        public event MetodosSemParametros carregaLista;
        public event MetodosSemParametros carregaLista_registos;
        public event MetodosSemParametros atualizaLista;
        public event MetodosSemParametros atualizaLista_registos;
        public event MetodosSemParametros pedidoID;
        public event MetodosComDate pedidoIdade;
        public event MetodosComDate pedidoIdadeEditar;
        public event MetodosComDate pedidoIdadeVisualizar;
        public event MetodosCom5Strings1Date1image pedidoGuardarUser;
        public event MetodosCom6StringsE1Date pedidoEditarUser;
        public event MetodosComInt pedidoApagarUser;
        public event MetodosComInt PedidoPesquisarID;
        public event MetodosSemParametros PedidoIDRegisto;
        public event MetodosCriarRegisto PedidoGuardarRegisto;
        public event MetodosComString PedidoConetarBitalino;
        public event MetodosComInt PedidoDadosBit;
        public event MetodosComInt pedidoNomeUser;
        public event MetodosComInt pedidoUserParaRegisto;
        public event MetodosComInt pedidoRegistosUser;
        public event MetodosComInt pedidoApagarRegisto;
        public event MetodosComInt pedidoOrdenarLista;
        public event MetodosSemParametros pedidoResetOrdem;
        public event MetodosComIntString pedidoFiltragem;
        public event MetodosComIntString pedidoFiltragem2;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            Program.model.AtualizarDataUtilizadores += Model_AtualizarDataUtilizadores; //carregar utilizadores ficheiro
            Program.model.resposta_gerar_id += Model_resposta_gerar_id;  // gerar id
            Program.model.resposta_calcular_idade += Model_resposta_calcular_idade;  //calcular idade
            Program.model.resposta_criar_user += Model_resposta_criar_user;
            Program.model.resposta_editar_user += Model_resposta_editar_user;
            Program.model.resposta_calcular_idade_editar += Model_resposta_calcular_idade_editar;
            Program.model.resposta_calcular_idade_visualizar += Model_resposta_calcular_idade_visualizar;
            Program.model.respostaApagarUser += Model_respostaApagarUser;
            Program.model.respostaPedidoPorID += Model_respostaPedidoPorID;
            Program.model.respostaGerarIDRegistos += Model_respostaGerarIDRegistos;
            Program.model.AtualizarDataRegistos += Model_AtualizarDataRegistos;
            Program.model.respostaNomeUser += Model_respostaNomeUser;
            Program.model.respostaPedidoUser += Model_respostaPedidoUser;
            Program.model.respostaApagarRegisto += Model_respostaApagarRegisto;
            Program.model.respostaCriarRegisto += Model_respostaCriarRegisto;
            Program.model.respostaFiltragem += Model_respostaFiltragem;
            Program.model.respostaFiltragem2 += Model_respostaFiltragem2;
        }

        private async void Model_respostaFiltragem2()
        {
            gridView_registos_User.Items.Clear();

            foreach (Registo r in Program.model.aux)
            {
                if (r.Estado == 0)
                {
                    StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await myfolder.GetFileAsync(r.Id_utilizador + ".png");
                    var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                    Uri uri = new Uri(file.Path);
                    BitmapImage img = new BitmapImage(new Uri(file.Path));
                    r.imagem_registo = img;

                    gridView_registos_User.Items.Add(r);
                }
            }
        }

        private async void Model_respostaFiltragem()
        {
            gridView_registos.Items.Clear();

            foreach (Registo r in Program.model.aux)
            {
                if (r.Estado == 0)
                {
                    StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await myfolder.GetFileAsync(r.Id_utilizador + ".png");
                    var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                    Uri uri = new Uri(file.Path);
                    BitmapImage img = new BitmapImage(new Uri(file.Path));
                    r.imagem_registo = img;

                    gridView_registos.Items.Add(r);
                }
            }
        }

        private void Model_respostaCriarRegisto()
        {
            if (atualizaLista_registos != null)
                atualizaLista_registos();
        }

        private void Model_respostaApagarRegisto()
        {
            gridView_registos.Items.Clear();

            foreach(Registo r in Program.model.listaRegistos)
            {
                if (r.Estado == 0)
                {
                    gridView_registos.Items.Add(r);
                }
            }
            pivotBit.SelectedItem = pivotRegistos;
        }

        private async void Model_respostaPedidoUser(Utilizador a)
        {
            StorageFolder myfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await myfolder.GetFileAsync(a.Id_utilizador + ".png");
            var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
            Uri uri = new Uri(file.Path);
            BitmapImage img = new BitmapImage(new Uri(file.Path));
            image_PerfilRegisto.Source = img;

            textBlock_nomeRegisto.Text = "Nome: " + a.Nome;
            ID_procurarRegisto.Text = "" + a.Id_utilizador;
            pivotBit.SelectedItem = pivotCriarRegisto;
        }

        private void Model_respostaNomeUser(string s)
        {
            pivotConsultaRegisto.Header = s;
        }

        private async void Model_AtualizarDataRegistos()
        {
            gridView_registos.Items.Clear();

            foreach (Registo r in Program.model.listaRegistos)
            {
                if (r.Estado == 0)
                {
                    StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await myfolder.GetFileAsync(r.Id_utilizador + ".png");
                    var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                    Uri uri = new Uri(file.Path);
                    BitmapImage img = new BitmapImage(new Uri(file.Path));
                    r.imagem_registo = img;

                    gridView_registos.Items.Add(r);
                }
            }
        }

        private void Model_respostaGerarIDRegistos(int a)
        {
            textBlock_IDRegisto.Text = "" + a;
        }

        private void Model_respostaApagarUser()
        {
            gridView_user.Items.Clear();

            foreach (Utilizador user in Program.model.listaUtilizadores)
            {
                if (user.Estado == 0)
                {
                    gridView_user.Items.Add(user);
                }
            }
            pivotBit.SelectedItem = pivotUtilizadores;
        }

        private void Model_resposta_calcular_idade_editar(int a)
        {
            textBox_idade_editar.Text = a.ToString();
        }
        private void Model_resposta_calcular_idade_visualizar(int idade)
        {
            textBlock_idade.Text = Convert.ToString(idade);
            textBox_idade_editar.Text = Convert.ToString(idade);

            pivotBit.SelectedItem = pivotPerfil;

        }

        private void Model_resposta_editar_user(Utilizador user)
        {
            pivotBit.SelectedItem = pivotPerfil;

            textBlock_id.Text = user.Id_utilizador.ToString();
            textBlock_editarId.Text = user.Id_utilizador.ToString();
            textBlock_nome.Text = user.Nome;
            Nome_editar.Text = user.Nome;
            textBlock_genero.Text = user.Genero;
            comboBox_genero_editar.SelectedValuePath = user.Genero;
            textBlock_idade.Text = user.Idade.ToString();
            Date_nascimento_editar.Date = user.DataNascimento.Date;
            textBox_idade_editar.Text = user.Idade.ToString();
            dnascimento.Date = user.DataNascimento;
            textBlock_altura.Text = user.Altura.ToString();
            textBox_altura_editar.Text = user.Altura.ToString();
            textBlock_peso.Text = user.Peso.ToString();
            textBox_peso_editar.Text = user.Peso.ToString();
        }

        private void Model_resposta_criar_user()
        {
            textBox_id.Text = "ID:";
            textBox_nome.Text = "";
            textBox_idade.Text = "";
            textBox_peso.Text = "";
            textBox_altura.Text = "";
            BitmapImage bitmapimage = new BitmapImage();
            Uri uri = new Uri("ms-appx:///perfil.jpg");
            bitmapimage.UriSource = uri;
            image_registo.Source = bitmapimage;

            if (atualizaLista != null)
                atualizaLista();

            pivotBit.SelectedItem = pivotUtilizadores;

        }

        private void gridView_registos_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                Registo r = (Registo)gridView_registos.SelectedItem;
                mostraRegisto(r);
            }
            catch (Exception) { }
        }

        private async void mostraRegisto(Registo r)
        {
            try
            {
                if (pedidoNomeUser != null)
                    pedidoNomeUser(r.Id_utilizador);

                listBox_logsConsultar.Items.Clear();
                StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await myfolder.GetFileAsync(r.Id_utilizador + ".png");
                var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                Uri uri = new Uri(file.Path);
                BitmapImage img = new BitmapImage(new Uri(file.Path));
                image_PerfilRegistoConsultar.Source = img;

                textBlock_IDRegistoConsultar.Text = Convert.ToString(r.Id_Registo);
                textBlock_TipoRegistoConsultar.Text = r.tiposExame;
                textBlock_DataI.Text = r.dataInicio.ToString();
                foreach (FrameRegisto fr in r.Resultados)
                    listBox_logsConsultar.Items.Add(fr.resultado0 + ";" + fr.resultado1 + ";" + fr.resultado2 + ";" + fr.resultado3 + ";" + fr.resultado4 + ";" + fr.resultado5);

                pivotBit.SelectedItem = pivotConsultaRegisto;
            }
            catch { }
            
        }

        private void Model_resposta_calcular_idade(int idade)
        {
            textBox_idade.Text = idade.ToString();
        }

        private void Model_resposta_gerar_id(int id)
        {
            textBox_id.Text = id.ToString();
        }

        private async void Model_AtualizarDataUtilizadores()
        {
            gridView_user.Items.Clear();

            foreach (Utilizador user in Program.model.listaUtilizadores)
            {
                if (user.Estado == 0)
                {
                    StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await myfolder.GetFileAsync(user.Id_utilizador + ".png");
                    var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                    Uri uri = new Uri(file.Path);
                    BitmapImage img = new BitmapImage(new Uri(file.Path));
                    user.imagem_user = img;

                    gridView_user.Items.Add(user);
                } 
            }
            //pivotBit.SelectedItem = pivotUtilizadores;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            
            
            //evento para carregar lista
            if (carregaLista != null)
                carregaLista();

            if (carregaLista_registos != null)
                carregaLista_registos();

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        //aqui temos de por as propriedades dos botoes visiveis ou inviseis para a appbarir mudando conforme as paginas
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //buttons todos invisiveis para depois escolher o que mostrar
            Adicionar.Visibility = Visibility.Collapsed;
            EditAppBarButton.Visibility = Visibility.Collapsed;
            ApagarAppBarButton.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            MostrarUsers.Visibility = Visibility.Collapsed;
            MostrarRegisto.Visibility = Visibility.Collapsed;
            MostrarRegisto_Users.Visibility = Visibility.Collapsed;
            CriarRegisto.Visibility = Visibility.Collapsed;
            CriarRegisto2.Visibility = Visibility.Collapsed;
            EditarRegisto.Visibility = Visibility.Collapsed;
            ApagarRegisto.Visibility = Visibility.Collapsed;
            GuardarUser.Visibility = Visibility.Collapsed;
            GuardarUserEditar.Visibility = Visibility.Collapsed;
            ButtonPesquisar.Visibility = Visibility.Collapsed;
            ButtonConfirmarPesquisa.Visibility = Visibility.Collapsed;
            ButtonOrdenar.Visibility = Visibility.Collapsed;
            ButtonOrdenarDec.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Collapsed;
            GuardarRegisto.Visibility = Visibility.Collapsed;
            

            if (pivotBit.SelectedItem == pivotIniciar)
            {
                Adicionar.Visibility = Visibility.Visible;
                CriarRegisto.Visibility = Visibility.Visible;
                ButtonPesquisar.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //buttons listar utilizadores
            if (pivotBit.SelectedItem == pivotUtilizadores)
            {
                Adicionar.Visibility = Visibility.Visible;
                MostrarRegisto.Visibility = Visibility.Visible;
                ButtonPesquisar.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //buttons listar registos
            if (pivotBit.SelectedItem == pivotRegistos)
            {
                Model_AtualizarDataRegistos();
                CriarRegisto.Visibility = Visibility.Visible;
                MostrarUsers.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //buttons criar utilizador
            if (pivotBit.SelectedItem == pivotCriarUtilizador)
            {
                BackButton.Visibility = Visibility.Visible;
                GuardarUser.Visibility = Visibility.Visible;
                if (pedidoID != null)
                    pedidoID();
            }

            //Visualizar utilizador
            if (pivotBit.SelectedItem == pivotPerfil)
            {
                BackButton.Visibility = Visibility.Visible;
                EditAppBarButton.Visibility = Visibility.Visible;
                ApagarAppBarButton.Visibility = Visibility.Visible;
                MostrarRegisto_Users.Visibility = Visibility.Visible;
            }

            //Editar utilizador
            if (pivotBit.SelectedItem == pivotEditar_Utilizadores)
            {
                GuardarUserEditar.Visibility = Visibility.Visible;
                MostrarRegisto.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //Pesquisa e ordenação
            if (pivotBit.SelectedItem == pivotPesquisar)
            {
                ButtonOrdenarDec.Visibility = Visibility.Visible;
                ButtonOrdenar.Visibility = Visibility.Visible;
                ButtonConfirmarPesquisa.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //Logs
            if (pivotBit.SelectedItem == pivotLogs)
            {
                StopButton.Visibility = Visibility.Visible;
                GuardarRegisto.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
                if (PedidoIDRegisto != null)
                    PedidoIDRegisto();
            }

            //criar registo
            if (pivotBit.SelectedItem == pivotCriarRegisto)
            {
                StartButton.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //consultar Registo
            if(pivotBit.SelectedItem == pivotConsultaRegisto)
            {
                ApagarRegisto.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

            //consultar registos de 1 user
            if (pivotBit.SelectedItem == pivotExames_User)
            {
                Model_AtualizarDataRegistos();
                CriarRegisto2.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }

        }

      
        private void button_iniciar_Click(object sender, RoutedEventArgs e)
        {

            Model_AtualizarDataUtilizadores();
            //So pode ser implementado na View(XAML).
            pivotBit.SelectedItem = pivotUtilizadores; //é o que faz mudar de view, chama o novo item do pivot
            //Basicamente pivotBit.SelectedItem -> pivot selecionado no momento.
            //Que vai ser igual ao "NOME DO PIVOT PARA ONDE QUEREMOS IR.
            //No caso acima é o pivotUtilizadores.
        }

        private void GridViewItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
        }

        private void UserPerfil(Utilizador user)
        {
            textBlock_id.Text = user.Id_utilizador.ToString();
            textBlock_editarId.Text = user.Id_utilizador.ToString();
            textBlock_nome.Text = user.Nome;
            Nome_editar.Text = user.Nome;
            textBlock_genero.Text = user.Genero;
            comboBox_genero_editar.SelectedValuePath = user.Genero;
            Date_nascimento_editar.Date = user.DataNascimento.Date;
            dnascimento.Date = user.DataNascimento;
            textBlock_altura.Text = user.Altura.ToString();
            textBox_altura_editar.Text = user.Altura.ToString();
            textBlock_peso.Text = user.Peso.ToString();
            textBox_peso_editar.Text = user.Peso.ToString();
            image_Perfil.Source = user.imagem_user;
            image_user_editar.Source = user.imagem_user;

            if (pedidoIdadeVisualizar != null)
                pedidoIdadeVisualizar(user.DataNascimento);

        }

        private void GridViewItem_DoubleTapped_1(object sender, DoubleTappedRoutedEventArgs e)
        {

        }

        private void gridView_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditAppBarButton1_Click(object sender, RoutedEventArgs e)
        {
            pivotBit.SelectedItem = pivotEditar_Utilizadores;
        }

        private async void DeleteAppBarButton1_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog();
            cd.Title = "Aviso";
            cd.Content = "Eliminar um utilizador é um processo\r\nirreversível.\r\n\r\nDeseja continuar com a operação?";
            
            cd.PrimaryButtonText = "Confirmar";
            cd.SecondaryButtonText = "Cancelar";
            ContentDialogResult LogoutDialog = await cd.ShowAsync();

            if (LogoutDialog == ContentDialogResult.Primary)
            {
                if (pedidoApagarUser != null)
                    pedidoApagarUser(Convert.ToInt32(textBlock_id.Text));
            }
        }

        private void Criar(object sender, RoutedEventArgs e)
        {
            pivotBit.SelectedItem = pivotCriarUtilizador;
        }


        //BACKBUTTON
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (pivotBit.SelectedItem == pivotIniciar)
            {
                Application.Current.Exit();
            }

            if (pivotBit.SelectedItem == pivotUtilizadores || pivotBit.SelectedItem == pivotRegistos)
            {
                if (pedidoResetOrdem != null)
                    pedidoResetOrdem();
                pivotBit.SelectedItem = pivotIniciar;
            }
            if (pivotBit.SelectedItem == pivotCriarUtilizador)
            {
                pivotBit.SelectedItem = pivotUtilizadores;
            }
            if (pivotBit.SelectedItem == pivotPerfil)
            {
                pivotBit.SelectedItem = pivotUtilizadores;
            }
            if (pivotBit.SelectedItem == pivotEditar_Utilizadores)
            {
                Model_AtualizarDataUtilizadores();
                pivotBit.SelectedItem = pivotPerfil;
            }
            if (pivotBit.SelectedItem == pivotPesquisar)
            {
                Model_AtualizarDataUtilizadores();
                pivotBit.SelectedItem = pivotIniciar;
            }
            if (pivotBit.SelectedItem == pivotCriarRegisto)
            {
                comboBox_duracao.SelectedIndex = -1;
                comboBox_tipo.SelectedIndex = -1;
                comboBox_Frame.SelectedIndex = -1;
                pivotBit.SelectedItem = pivotRegistos;
            }
            if (pivotBit.SelectedItem == pivotLogs)
            {
                textBlock_IDRegisto.Text = "";
                textBlock_TipoRegisto.Text = "";
                listBox_logs.Items.Clear();
                BitmapImage bitmapimage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///avatar.png");
                bitmapimage.UriSource = uri;
                image_PerfilRegisto.Source = bitmapimage;
                textBlock_nomeRegisto.Text = "Nome: ";
                ID_procurarRegisto.Text = "";
                comboBox_duracao.SelectedIndex = -1;
                comboBox_tipo.SelectedIndex = -1;
                comboBox_Frame.SelectedIndex = -1;
                listBox_logs.Items.Clear();
                pivotBit.SelectedItem = pivotCriarRegisto;
            }
            if (pivotBit.SelectedItem == pivotConsultaRegisto)
            {
                listBox_logsConsultar.Items.Clear();
                pivotConsultaRegisto.Header = "Registo";
                pivotBit.SelectedItem = pivotRegistos;
            }
            if (pivotBit.SelectedItem == pivotExames_User)
            {
                if (pedidoResetOrdem != null)
                    pedidoResetOrdem();
                pivotBit.SelectedItem = pivotPerfil;
            }
        }




        //PASSO 1- ABRIR A GALERIA.
        private void button_addfoto_Click(object sender, RoutedEventArgs e)
        {
            //Cria um novo picker.
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            //Define os tipos de ficheiro que vai poder abrir/ visualizar.
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".png");
            //Basicamente abre a imagem.
            fileOpenPicker.ContinuationData["Operate"] = "OpenImage";
            //Segue o evento, deixa escolher
            fileOpenPicker.PickSingleFileAndContinue();
        }

        //PASSO 2 - CARREGAR A IMAGEM DA GALERIA PARA O LOCAL PRETENDIDO.
        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            // assume que vem apenas um ficheiro
            if ((args.Files != null && args.Files.Count == 1))
            {
                IReadOnlyList<StorageFile> files = args.Files;

                if (args.ContinuationData["Operate"] as string == "OpenImage")
                {
                    IRandomAccessStream fileStream = await files[0].OpenAsync(FileAccessMode.Read);

                    /*BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);*/
                    var wbm = new WriteableBitmap(1080, 1920);
                    await wbm.SetSourceAsync(await files[0].OpenAsync(FileAccessMode.Read));

                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    if (folder != null)
                    {
                        StorageFile file = await folder.CreateFileAsync(textBox_id.Text+".png", CreationCollisionOption.ReplaceExisting);
                        using (var storageStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, storageStream);
                            var pixelStream = wbm.PixelBuffer.AsStream();
                            var pixels = new byte[pixelStream.Length];
                            await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)wbm.PixelWidth, (uint)wbm.PixelHeight, 48, 48, pixels);
                            await encoder.FlushAsync();
                        }
                        //rename
                    }


                    image_registo.Source = wbm; //A Source é onde é guardada a foto nas propriedades da imagem.
                    image_user_editar.Source = wbm;
                }
            }
        }

        private void Date_nascimento_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (pedidoIdade != null)
                pedidoIdade(Date_nascimento.Date.Date);
        }

        private void GuardarUser_Click(object sender, RoutedEventArgs e)
        {
            
            //Utilizador novo;
            //DateTime data = Date_nascimento.Date.Date;

            if (textBox_id.Text != "" && textBox_nome.Text != "" && comboBox_genero.SelectedItem.ToString() !="Genero" && textBox_altura.Text != "" && textBox_peso.Text != "")
            {
                //novo = new Utilizador(Convert.ToInt32(textBox_id.Text), textBox_nome.Text, data, comboBox_genero.SelectedItem.ToString(), Convert.ToInt32(textBox_altura.Text), Convert.ToInt32(textBox_peso.Text), image_registo.Source.ToString(), 0);
                if (pedidoGuardarUser != null)
                    pedidoGuardarUser(textBox_id.Text, textBox_nome.Text, Date_nascimento.Date.Date, comboBox_genero.SelectedItem.ToString(), textBox_altura.Text, textBox_peso.Text, image_registo.Source as BitmapImage);
                //pedidoGuardarUser(textBox_id.Text, textBox_nome.Text, Date_nascimento.Date.Date, comboBox_genero.SelectedItem.ToString(), textBox_altura.Text, textBox_peso.Text, image_registo.Source.ToString());
                
            }
        }

        private void gridView_user_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                Utilizador u = (Utilizador)gridView_user.SelectedItem;
                UserPerfil(u);
            }
            catch (Exception){}
            
        }

        private void GuardarUserEditar_Click(object sender, RoutedEventArgs e)
        {
            if (Nome_editar.Text != "" && textBox_altura_editar.Text != "" && textBox_peso_editar.Text != "")
            {
                if (pedidoEditarUser != null)
                    pedidoEditarUser(textBlock_editarId.Text, Nome_editar.Text, Date_nascimento_editar.Date.Date, comboBox_genero_editar.SelectedItem.ToString(), textBox_altura_editar.Text, textBox_peso_editar.Text, image_user_editar.Source.ToString());
            }  
        }

        private void Date_nascimento_editar_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (pedidoIdadeEditar != null)
                pedidoIdadeEditar(Date_nascimento_editar.Date.Date);
        }

        private void MostrarUsers_Click(object sender, RoutedEventArgs e)
        {
            pivotBit.SelectedItem = pivotUtilizadores;
        }

        private void button_registos_Click(object sender, RoutedEventArgs e)
        {
            pivotBit.SelectedItem = pivotRegistos;
        }

        private void MostrarRegisto_Click(object sender, RoutedEventArgs e)
        {
            pivotBit.SelectedItem = pivotRegistos;
        }

        private void button_addfoto_editar_Click(object sender, RoutedEventArgs e)
        {
            //Cria um novo picker.
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            //Define os tipos de ficheiro que vai poder abrir/ visualizar.
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".png");
            //Basicamente abre a imagem.
            fileOpenPicker.ContinuationData["Operate"] = "OpenImage";
            //Segue o evento, deixa escolher
            fileOpenPicker.PickSingleFileAndContinue();
        }

        private void ButtonPesquisar_Click(object sender, RoutedEventArgs e)
        {
            pivotBit.SelectedItem = pivotPesquisar;
        }


        //filtrar
        private void ButtonConfirmarPesquisa_Click(object sender, RoutedEventArgs e)
        {
            //ver forms para aproveitar codigo
            ListaFiltradaComCheckds(checkBoxID.IsChecked, checkBoxNome.IsChecked, checkBoxIdade.IsChecked, checkBoxGenero.IsChecked, checkBoxPeso.IsChecked, checkBoxAltura.IsChecked, ID_procurar.Text, Nome_procurar.Text, Idade_procurar.Text, (string)genero_procurar.SelectedItem, Peso_procurar.Text, Altura_procurar.Text);
        }
        private async void ListaFiltradaComCheckds(bool? isChecked1, bool? isChecked2, bool? isChecked3, bool? isChecked4, bool? isChecked5, bool? isChecked6, string ID, string nome, string idade, string genero, string peso, string altura)
        {
            List<Utilizador> aux = new List<Utilizador>();
            foreach (Utilizador a in Program.model.listaUtilizadores)
                aux.Add(a);

            for (int i = 0; i < aux.Count(); i++)
            {
                if (isChecked1 == true && aux[i].Id_utilizador != Convert.ToInt32(ID))
                {
                    aux.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < aux.Count(); i++)
            {
                if (isChecked2 == true && aux[i].Nome != nome)
                {
                    aux.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < aux.Count(); i++)
            {
                if (isChecked3 == true && aux[i].Idade != Convert.ToInt32(idade))
                {
                    aux.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < aux.Count(); i++)
            {
                if (isChecked4 == true && aux[i].Genero != genero)
                {
                    aux.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < aux.Count(); i++)
            {
                if ((isChecked5 == true && aux[i].Peso != Convert.ToInt32(peso)))
                {
                    aux.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < aux.Count(); i++)
            {
                if ((isChecked6 == true && aux[i].Altura != Convert.ToInt32(altura)))
                {
                    aux.RemoveAt(i);
                    i--;
                }
            }

            if (aux.Count() == 0)
            {
                ContentDialog cd = new ContentDialog();
                cd.Title = "Aviso";
                cd.Content = "Não foram encontrados Utilizadores com os\r\nparâmetros pretendidos!!!";

                cd.PrimaryButtonText = "Confirmar";
                ContentDialogResult LogoutDialog = await cd.ShowAsync();

                if (LogoutDialog == ContentDialogResult.Primary)
                {
                }
            }
            else
            {
                gridView_user.Items.Clear();

                foreach (Utilizador user in aux)
                {
                    if (user.Estado == 0)
                    {
                        StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                        BitmapImage bitmapImage = new BitmapImage();
                        StorageFile file = await myfolder.GetFileAsync(user.Id_utilizador + ".png");
                        var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                        Uri uri = new Uri(file.Path);
                        BitmapImage img = new BitmapImage(new Uri(file.Path));
                        user.imagem_user = img;

                        gridView_user.Items.Add(user);
                    }
                }
                pivotBit.SelectedItem = pivotUtilizadores;
            }
        }
        private void Nome_procurar_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBoxNome.IsChecked = true;
        }
        private void ID_procurar_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBoxID.IsChecked = true;
        }
        private void Idade_procurar_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBoxIdade.IsChecked = true;
        }
        private void genero_procurar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkBoxGenero.IsChecked = true;
        }
        private void Peso_procurar_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBoxPeso.IsChecked = true;
        }
        private void Altura_procurar_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBoxAltura.IsChecked = true;
        }


        //ordenar
        private async void ButtonOrdenar_Click(object sender, RoutedEventArgs e)
        {
            List<Utilizador> aux = new List<Utilizador>();
            foreach (Utilizador a in Program.model.listaUtilizadores)
                aux.Add(a);

            if (checkBoxID.IsChecked == true)
            {
                OrdenaçãoID(aux);
            }

            if(checkBoxNome.IsChecked == true)
            {
                OrdenaçãoNome(aux);
            }

            if (checkBoxIdade.IsChecked == true)
            {
                OrdenaçãoIdade(aux);
            }

            if (checkBoxGenero.IsChecked == true)
            {
                OrdenaçãoGenero(aux);
            }

            if (checkBoxPeso.IsChecked == true)
            {
                OrdenaçãoPeso(aux);
            }

            if (checkBoxAltura.IsChecked ==true)
            {
                OrdenaçãoAltura(aux);
            }

            gridView_user.Items.Clear();

            foreach (Utilizador user in aux)
            {
                if (user.Estado == 0)
                {
                    StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await myfolder.GetFileAsync(user.Id_utilizador + ".png");
                    var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                    Uri uri = new Uri(file.Path);
                    BitmapImage img = new BitmapImage(new Uri(file.Path));
                    user.imagem_user = img;

                    gridView_user.Items.Add(user);
                }
            }
            pivotBit.SelectedItem = pivotUtilizadores;
        }
        private async void ButtonOrdenarDec_Click(object sender, RoutedEventArgs e)
        {
            List<Utilizador> aux = new List<Utilizador>();
            foreach (Utilizador a in Program.model.listaUtilizadores)
                aux.Add(a);

            if (checkBoxID.IsChecked == true)
            {
                OrdenaçãoID(aux);
                aux.Reverse();
            }

            if (checkBoxNome.IsChecked == true)
            {
                OrdenaçãoNome(aux);
                aux.Reverse();
            }

            if (checkBoxIdade.IsChecked == true)
            {
                OrdenaçãoIdade(aux);
                aux.Reverse();
            }

            if (checkBoxGenero.IsChecked == true)
            {
                OrdenaçãoGenero(aux);
                aux.Reverse();
            }

            if (checkBoxPeso.IsChecked == true)
            {
                OrdenaçãoPeso(aux);
                aux.Reverse();
            }

            if (checkBoxAltura.IsChecked == true)
            {
                OrdenaçãoAltura(aux);
                aux.Reverse();
            }

            gridView_user.Items.Clear();

            foreach (Utilizador user in aux)
            {
                if (user.Estado == 0)
                {
                    StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await myfolder.GetFileAsync(user.Id_utilizador + ".png");
                    var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                    Uri uri = new Uri(file.Path);
                    BitmapImage img = new BitmapImage(new Uri(file.Path));
                    user.imagem_user = img;

                    gridView_user.Items.Add(user);
                }
            }
            pivotBit.SelectedItem = pivotUtilizadores;
        }

        public List<Utilizador> OrdenaçãoAltura(List<Utilizador>listaAux)
        {
            Utilizador temp = new Utilizador();

            for (int i = 1; i < listaAux.Count; i++)
            {
                for (int j = 0; j < listaAux.Count - i; j++)
                {
                    if (listaAux[j].Altura > listaAux[j + 1].Altura)
                    {
                        temp = listaAux[j];
                        listaAux[j] = listaAux[j + 1];
                        listaAux[j + 1] = temp;
                    }
                }
            }
            return listaAux;
        }
        public List<Utilizador> OrdenaçãoID(List<Utilizador> listaAux)
        {
            Utilizador temp = new Utilizador();

            for (int i = 1; i < listaAux.Count; i++)
            {
                for (int j = 0; j < listaAux.Count - i; j++)
                {
                    if (listaAux[j].Id_utilizador > listaAux[j + 1].Id_utilizador)
                    {
                        temp = listaAux[j];
                        listaAux[j] = listaAux[j + 1];
                        listaAux[j + 1] = temp;
                    }
                }
            }
            return listaAux;
        }
        public List<Utilizador> OrdenaçãoPeso(List<Utilizador> listaAux)
        {
            Utilizador temp = new Utilizador();

            for (int i = 1; i < listaAux.Count; i++)
            {
                for (int j = 0; j < listaAux.Count - i; j++)
                {
                    if (listaAux[j].Peso > listaAux[j + 1].Peso)
                    {
                        temp = listaAux[j];
                        listaAux[j] = listaAux[j + 1];
                        listaAux[j + 1] = temp;
                    }
                }
            }
            return listaAux;
        }
        public List<Utilizador> OrdenaçãoIdade(List<Utilizador> listaAux)
        {
            Utilizador temp = new Utilizador();

            for (int i = 1; i < listaAux.Count; i++)
            {
                for (int j = 0; j < listaAux.Count - i; j++)
                {
                    if (listaAux[j].Idade > listaAux[j + 1].Idade)
                    {
                        temp = listaAux[j];
                        listaAux[j] = listaAux[j + 1];
                        listaAux[j + 1] = temp;
                    }
                }
            }
            return listaAux;
        }
        public List<Utilizador> OrdenaçãoNome(List<Utilizador> listaAux)
        {
            Utilizador temp = new Utilizador();

            for (int i = 1; i < listaAux.Count; i++)
            {
                for (int j = 0; j < listaAux.Count - i; j++)
                {
                    if (listaAux[j].Nome.CompareTo(listaAux[j + 1].Nome)>0)
                    {
                        temp = listaAux[j];
                        listaAux[j] = listaAux[j + 1];
                        listaAux[j + 1] = temp;
                    }
                }
            }
            return listaAux;
        }
        public List<Utilizador> OrdenaçãoGenero(List<Utilizador> listaAux)
        {
            Utilizador temp = new Utilizador();

            for (int i = 1; i < listaAux.Count; i++)
            {
                for (int j = 0; j < listaAux.Count - i; j++)
                {
                    if (listaAux[j].Genero.CompareTo(listaAux[j + 1].Genero) > 0)
                    {
                        temp = listaAux[j];
                        listaAux[j] = listaAux[j + 1];
                        listaAux[j + 1] = temp;
                    }
                }
            }
            return listaAux;
        }

        private void comboBox_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CriarRegisto_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapimage = new BitmapImage();
            Uri uri = new Uri("ms-appx:///avatar.png");
            bitmapimage.UriSource = uri;
            image_PerfilRegisto.Source = bitmapimage;
            textBlock_nomeRegisto.Text = "Nome: ";
            ID_procurarRegisto.Text = "";
            comboBox_duracao.SelectedIndex = -1;
            comboBox_tipo.SelectedIndex = -1;
            comboBox_Frame.SelectedIndex = -1;

            pivotBit.SelectedItem = pivotCriarRegisto;
        }

        private void button_SelecionarUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PedidoPesquisarID != null)
                    PedidoPesquisarID(Convert.ToInt32(ID_procurarRegisto.Text));
            }
            catch (Exception) { }
            
        }

        //resposta procura ID
        private async void Model_respostaPedidoPorID(Utilizador a)
        {
            if (a.Estado == 0)
            {
                StorageFolder myfolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await myfolder.GetFileAsync(a.Id_utilizador + ".png");
                var image = await Windows.Storage.FileIO.ReadBufferAsync(file);
                Uri uri = new Uri(file.Path);
                BitmapImage img = new BitmapImage(new Uri(file.Path));
                image_PerfilRegisto.Source = img;

                textBlock_nomeRegisto.Text = "Nome: " + a.Nome;
            }
            else
            {
                ContentDialog cd = new ContentDialog();
                cd.Title = "Atenção";
                cd.Content = "Utilizador não encontrado!!!\r\nPretende criar um novo utilizador?";

                cd.PrimaryButtonText = "Ok";
                cd.SecondaryButtonText = "Cancelar";
                ContentDialogResult LogoutDialog = await cd.ShowAsync();

                if (LogoutDialog == ContentDialogResult.Primary)
                {
                    BitmapImage bitmapimage = new BitmapImage();
                    Uri uri = new Uri("ms-appx:///avatar.png");
                    bitmapimage.UriSource = uri;
                    image_PerfilRegisto.Source = bitmapimage;
                    textBlock_nomeRegisto.Text = "Nome: ";
                    ID_procurarRegisto.Text = "";

                    pivotBit.SelectedItem = pivotCriarUtilizador;
                }
            }
        }

        //resposta para ID nao encontrado
        public async void respostaPedidoID()
        {
            ContentDialog cd = new ContentDialog();
            cd.Title = "Atenção";
            cd.Content = "Utilizador não encontrado!!!\r\nPretende criar um novo utilizador?";

            cd.PrimaryButtonText = "Ok";
            cd.SecondaryButtonText = "Cancelar";
            ContentDialogResult LogoutDialog = await cd.ShowAsync();

            if (LogoutDialog == ContentDialogResult.Primary)
            {
                BitmapImage bitmapimage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///avatar.png");
                bitmapimage.UriSource = uri;
                image_PerfilRegisto.Source = bitmapimage;
                textBlock_nomeRegisto.Text = "Nome: ";
                ID_procurarRegisto.Text = "";

                pivotBit.SelectedItem = pivotCriarUtilizador;

            }
        }

        //apenas para nao dar erro
        private void ID_procurarRegisto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(ID_procurarRegisto.Text != "")
            {
                textBlock_TipoRegisto.Text = comboBox_tipo.SelectedItem.ToString();
                pivotBit.SelectedItem = pivotLogs;
                Program.controller.readContinue = true;
                if (PedidoDadosBit != null)
                    PedidoDadosBit(Convert.ToInt32(comboBox_duracao.SelectedItem.ToString()));
            }
            else
            {
                //por aqui mensagem para introduzir ID de utilizador
            }
            
            
        }

        public void atualizaLOGS(string logs)
        {
            listBox_logs.Items.Add(logs);
        }

        private async void button_SelecionarPorta_Click(object sender, RoutedEventArgs e)
        {
            TextBox mac = new TextBox();
            ContentDialog cd = new ContentDialog();
            cd.Title = "Insira o MAC address do seu BITalino";
            cd.Content = mac;
            cd.PrimaryButtonText = "Ok";
            cd.SecondaryButtonText = "Cancelar";
            ContentDialogResult LogoutDialog = await cd.ShowAsync();

            if (LogoutDialog == ContentDialogResult.Primary)
            {
                TextBox_portas.Text = mac.Text;
            }
        }

        private async void erroConexao(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog();
            cd.Title = "Atenção";
            cd.Content = "BITalino não encontrado.\r\nTente outra vez...";

            cd.PrimaryButtonText = "Ok";
            
            ContentDialogResult LogoutDialog = await cd.ShowAsync();

            if (LogoutDialog == ContentDialogResult.Primary)
            {

            }
        }

        private void TextBlock_portas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PedidoConetarBitalino != null)
                PedidoConetarBitalino(TextBox_portas.Text);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Program.controller.readContinue = false;
        }

        private async void GuardarRegisto_Click(object sender, RoutedEventArgs e)
        {
            Program.controller.readContinue = false;
            if (PedidoGuardarRegisto != null)
                PedidoGuardarRegisto(textBlock_IDRegisto.Text, ID_procurarRegisto.Text, textBlock_TipoRegisto.Text, comboBox_Frame.SelectedItem.ToString());

            ContentDialog cd = new ContentDialog();
            cd.Title = "Registo guardado com sucesso!!!";
            cd.PrimaryButtonText = "Ok";

            ContentDialogResult LogoutDialog = await cd.ShowAsync();

            if (LogoutDialog == ContentDialogResult.Primary)
            {
                textBlock_IDRegisto.Text = "";
                textBlock_TipoRegisto.Text = "";
                listBox_logs.Items.Clear();
                BitmapImage bitmapimage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///avatar.png");
                bitmapimage.UriSource = uri;
                image_PerfilRegisto.Source = bitmapimage;
                textBlock_nomeRegisto.Text = "Nome: ";
                ID_procurarRegisto.Text = "";
                comboBox_duracao.SelectedIndex = -1;
                comboBox_tipo.SelectedIndex = -1;
                comboBox_Frame.SelectedIndex = -1;

                pivotBit.SelectedItem = pivotRegistos;
            }
        }

        private void comboBox_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CriarRegisto2_Click(object sender, RoutedEventArgs e)
        {
            if(pedidoUserParaRegisto!=null)
                pedidoUserParaRegisto(Convert.ToInt32(textBlock_id.Text));
        }

        private void MostrarRegisto_Users_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoRegistosUser != null)
                pedidoRegistosUser(Convert.ToInt32(textBlock_id.Text));
            pivotBit.SelectedItem = pivotExames_User;
        }

        private void gridView_registos_User_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                Registo r = (Registo)gridView_registos_User.SelectedItem;
                mostraRegisto(r);
            }
            catch (Exception) { }
        }

        private void ApagarRegisto_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoApagarRegisto != null)
                pedidoApagarRegisto(Convert.ToInt32(textBlock_IDRegistoConsultar.Text));
        }

        private void button_Ordenar_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoOrdenarLista != null)
                pedidoOrdenarLista(comboBox_opcoes.SelectedIndex);
        }

        private void button_Ordenar2_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoOrdenarLista != null)
                pedidoOrdenarLista(comboBox_opcoes2.SelectedIndex);
        }

        private async void button_Filtrar_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox_opcoes.SelectedIndex >= 0)
            {
                if (comboBox_opcoes.SelectedIndex < 2)
                {
                    TextBox filtragem = new TextBox();
                    ContentDialog cd = new ContentDialog();
                    cd.Title = "Filtrar por " + comboBox_opcoes.SelectedItem.ToString();
                    cd.Content = filtragem;
                    cd.PrimaryButtonText = "Ok";
                    cd.SecondaryButtonText = "Cancelar";

                    ContentDialogResult LogoutDialog = await cd.ShowAsync();

                    if (LogoutDialog == ContentDialogResult.Primary)
                    {
                        if (pedidoFiltragem != null)
                            pedidoFiltragem(comboBox_opcoes.SelectedIndex, filtragem.Text);
                    }
                }
                if (comboBox_opcoes.SelectedIndex >= 2)
                {
                    DatePicker data = new DatePicker();
                    ContentDialog cd = new ContentDialog();
                    cd.Title = "Filtrar por " + comboBox_opcoes.SelectedItem.ToString();
                    cd.Content = data;
                    cd.PrimaryButtonText = "Ok";
                    cd.SecondaryButtonText = "Cancelar";

                    ContentDialogResult LogoutDialog = await cd.ShowAsync();

                    if (LogoutDialog == ContentDialogResult.Primary)
                    {
                        if (pedidoFiltragem != null)
                            pedidoFiltragem(comboBox_opcoes.SelectedIndex, data.Date.ToString());
                    }
                }
                
            }
            else
            {
                ContentDialog cd = new ContentDialog();
                cd.Title = "Importante!";
                cd.Content = "Por favor escolha a opção pretendida!";
                cd.PrimaryButtonText = "Ok";

                ContentDialogResult LogoutDialog = await cd.ShowAsync();

                if (LogoutDialog == ContentDialogResult.Primary)
                {
                   
                }
            }
            
        }

        private async void button_Filtrar2_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_opcoes2.SelectedIndex >= 0)
            {
                if (comboBox_opcoes2.SelectedIndex < 2)
                {
                    TextBox filtragem = new TextBox();
                    ContentDialog cd = new ContentDialog();
                    cd.Title = "Filtrar por " + comboBox_opcoes2.SelectedItem.ToString();
                    cd.Content = filtragem;
                    cd.PrimaryButtonText = "Ok";
                    cd.SecondaryButtonText = "Cancelar";

                    ContentDialogResult LogoutDialog = await cd.ShowAsync();

                    if (LogoutDialog == ContentDialogResult.Primary)
                    {
                        if (pedidoFiltragem2 != null)
                            pedidoFiltragem2(comboBox_opcoes2.SelectedIndex, filtragem.Text);
                    }
                }
                if (comboBox_opcoes.SelectedIndex >= 2)
                {
                    DatePicker data = new DatePicker();
                    ContentDialog cd = new ContentDialog();
                    cd.Title = "Filtrar por " + comboBox_opcoes2.SelectedItem.ToString();
                    cd.Content = data;
                    cd.PrimaryButtonText = "Ok";
                    cd.SecondaryButtonText = "Cancelar";

                    ContentDialogResult LogoutDialog = await cd.ShowAsync();

                    if (LogoutDialog == ContentDialogResult.Primary)
                    {
                        if (pedidoFiltragem2 != null)
                            pedidoFiltragem2(comboBox_opcoes2.SelectedIndex, data.Date.ToString());
                    }
                }

            }
            else
            {
                ContentDialog cd = new ContentDialog();
                cd.Title = "Importante!";
                cd.Content = "Por favor escolha a opção pretendida!";
                cd.PrimaryButtonText = "Ok";

                ContentDialogResult LogoutDialog = await cd.ShowAsync();

                if (LogoutDialog == ContentDialogResult.Primary)
                {

                }
            }

        }
    }
}
