using Gma.System.MouseKeyHook;
using SuperSimpleTcp;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

using System;
using System.Windows;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SharpHook.Native;
using System.Diagnostics;
using DataReceivedEventArgs = SuperSimpleTcp.DataReceivedEventArgs;
using SharpHook;
using MouseHookEventArgs = SharpHook.MouseHookEventArgs;
using KeyboardHookEventArgs = SharpHook.KeyboardHookEventArgs;

namespace multiboxApp
{
    public partial class Form1 : Form
    {
        SimpleTcpClient? client;
        SimpleTcpServer? server;

        int ServerWidth;
        int ServerHeight;

        bool _isServer = false;
        bool _isClient = false;

        public bool IsServer { get => _isServer; set => _isServer = value; }
        public bool IsClient { get => _isClient; set => _isClient = value; }

        int skipMouseMoves = 100;
        int mouseMoves = 0;

        public Form1()
        {
            InitializeComponent();
            SharpHook.Hook.MousePressed += Hook_MousePressed;
            SharpHook.Hook.MouseReleased += Hook_MouseReleased;
            SharpHook.Hook.MouseMoved += Hook_MouseMoved;

            SharpHook.Hook.KeyPressed += Hook_KeyPressed;
            SharpHook.Hook.KeyReleased += Hook_KeyReleased;
            

            SharpHook.Hook.RunAsync();

            Console.WriteLine("testing");

        }

        #region hookHandlers

        private bool isSending()
        {
            return 
                IsServer && 
                server != null && 
                server.Connections > 0 && 
                Control.IsKeyLocked(Keys.Scroll);
        }
        private void Hook_KeyReleased(object? sender, KeyboardHookEventArgs e)
        {
            if (isSending())
            {
                string message = $".mx{LastMouseMovement.Data.X}y{LastMouseMovement.Data.Y}.kr{(ushort)e.Data.KeyCode}";
                serverSendMessage(message);
                
            }
        }

        private void Hook_KeyPressed(object? sender, KeyboardHookEventArgs e)
        {
            if(e.Data.KeyCode == KeyCode.VcNumPadMultiply)
            {
                SharpHook.Hook.Dispose();
            }
            if (isSending())
            {
                string message = $".mx{LastMouseMovement.Data.X}y{LastMouseMovement.Data.Y}.kp{(ushort)e.Data.KeyCode}";
                serverSendMessage(message);
            }
        }

        private MouseHookEventArgs LastMouseMovement;
        private void Hook_MouseMoved(object? sender, MouseHookEventArgs e)
        {
            LastMouseMovement = e;
            if (isSending())
            {
                if (++mouseMoves > skipMouseMoves)
                {
                    mouseMoves = 0;
                    string message = $".mx{e.Data.X}y{e.Data.Y}";
                    serverSendMessage(message);
                    //serverSendMessage(".T" + e.EventTime.Millisecond.ToString());
                    //appendToOutput($"timeOffset {e.EventTime.Millisecond - DateTime.Now.Millisecond}");
                }
            }
        }
        private void Hook_MouseReleased(object? sender, MouseHookEventArgs e)
        {
            if (isSending())
            {
                string message = $".mr{(ushort)e.Data.Button}x{e.Data.X}y{e.Data.Y}";
                serverSendMessage(message);
            }
        }

        private void Hook_MousePressed(object? sender, MouseHookEventArgs e)
        {
            if (isSending())
            {
                string message = $".mp{(ushort)e.Data.Button}x{e.Data.X}y{e.Data.Y}";
                serverSendMessage(message);
            }
        }
        #endregion


        #region client
        private void clientBtn_Click(object sender, EventArgs e)
        {
            IsClient= true;
            IsServer= false;

            destoryServer();
            clientBtn.Enabled = false;
            serverBtn.Enabled = false;
            //Connect to server
            client = new SimpleTcpClient(ipAddressBox.Text, Convert.ToInt32(portBox.Text));

            client.Events.Connected += ClientConnected;
            client.Events.Disconnected += ClientDisconnected;
            client.Events.DataReceived += ClientDataReceived;

            appendToOutput("starting client");
            client.Connect();
        }

        private void ClientConnected(object? sender, ConnectionEventArgs e)
        {
            appendToOutput($"*** Server {e.IpPort} connected");
        }

        private void ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            appendToOutput($"*** Server {e.IpPort} disconnected");
        }

        // Messages in the form of
        // kr67
        // kp67
        // mr1x425y539
        // mp1x425y539
        // mx425y539
        // serverdim:w1920h1080

        private void handleRecievedData(string data)
        {

            var mes = data.Split('.', StringSplitOptions.RemoveEmptyEntries);
            foreach (var m in mes)
            {
                handleRecievedMessage(m);
            }
        }
        private void handleRecievedMessage(string message)
        {
            if (message[0] == 'T')
            {
                var mili = int.Parse(message.Substring(1));
                appendToOutput($"timeOffset {(mili - DateTime.Now.Millisecond).ToString()}");
            }
            if (message[0] == 'm')
            {
                ushort mousebtn;
                mousebtn = ushort.Parse(message[2].ToString());

                var x = message.IndexOf('x');
                var y = message.IndexOf('y');
                var xpos = short.Parse(message.Substring((x + 1), y - (x + 1)));
                var ypos = short.Parse(message.Substring(y + 1));
                xpos = (short)(xpos * ((float)Screen.PrimaryScreen.Bounds.Width / ServerWidth));
                ypos = (short)(ypos * ((float)Screen.PrimaryScreen.Bounds.Height / ServerHeight));

                appendToOutput($"Moving Mouse to {xpos},{ypos}", Color.Blue);
                appendToOutput($"my width: {Screen.PrimaryScreen.Bounds.Width}, server: {ServerWidth}");
                
                
                SharpHook.Simulator.SimulateMouseMovement(xpos, ypos);
                
                if (message[1] == 'p')
                {
                    appendToOutput($"Mouse Press at:x{xpos}y{ypos}");
                    SharpHook.Simulator.SimulateMousePress((MouseButton)mousebtn);
                }
                if (message[1] == 'r')
                {
                    appendToOutput($"Mouse Release at:x{xpos}y{ypos}");
                    SharpHook.Simulator.SimulateMouseRelease((MouseButton)mousebtn);
                }
            }
            else if (message[0] == 'k')
            {
                ushort.TryParse(message.Substring(2), out ushort key);
                var keycode = (KeyCode)key;
                if (message[1] == 'p')
                {
                    appendToOutput($"Key Press ${Enum.GetName(typeof(KeyCode), keycode)}");
                    SharpHook.Simulator.SimulateKeyPress(keycode);
                }
                if (message[1] == 'r')
                {
                    appendToOutput($"Key Press ${Enum.GetName(typeof(KeyCode), keycode)}");
                    SharpHook.Simulator.SimulateKeyRelease(keycode);
                }
            }
            if (message.StartsWith("serverdim:"))
            {
                var w = message.IndexOf('w');
                var h = message.IndexOf('h');

                ServerWidth = int.Parse(message.Substring((w + 1), h - (w + 1)));
                ServerHeight = int.Parse(message.Substring(h + 1));
            }
        }

        private void ClientDataReceived(object? sender, DataReceivedEventArgs e)
        {
            var data = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);

            try
            {
                handleRecievedData(data);
            }
            catch (Exception ex)
            {
                appendToOutput($"BAD RECIEVED MESSAGE Error: {ex.Message}");
            }
            appendToOutput($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
            

        }

        private void ClientSendMessage(string message)
        {
            if (client != null)
            {
                client.Send(message);
            }
            else
            {
                appendToOutput("null client!", Color.Red);
            }
        }
        #endregion





        #region Server
        private void serverBtn_Click(object sender, EventArgs e)
        {
            // instantiate
            if (!IsServer)
            {
                IsClient = false;
                IsServer = true;
                server = new SimpleTcpServer(ipAddressBox.Text, Convert.ToInt32(portBox.Text));
                destoryClient();
                clientBtn.Enabled = false;
                serverBtn.Enabled = false;

                // set events
                server.Events.ClientConnected += ServerConnected;
                server.Events.ClientDisconnected += ServerDisconnected;
                server.Events.DataReceived += ServerDataReceived;

                // let's go!
                server.Start();

                appendToOutput("starting server");
                // once a client has connected...
                server.Send("[ClientIp:Port]", "Hello, world!");
            }
        }



        void ServerConnected(object? sender, ConnectionEventArgs e)
        {

            appendToOutput($"\n[{e.IpPort}] client connected");
            serverSendMessage($".serverdim:w{Screen.PrimaryScreen.Bounds.Width}h{Screen.PrimaryScreen.Bounds.Height}");
            
        }

        void ServerDisconnected(object? sender, ConnectionEventArgs e)
        {
            appendToOutput($"[{e.IpPort}] client disconnected: {e.Reason}");
        }

        void ServerDataReceived(object? sender, DataReceivedEventArgs e)
        {
            appendToOutput($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }

        #endregion




        #region Form
        void appendToOutput(string text, Color? color = null, RichTextBox? targetBox = null)
        {
            var box = targetBox ?? outputBox;
            var col = color ?? Color.Black;
            if (outputBox.InvokeRequired)
            { 
                outputBox.Invoke(new Action<string, Color?, RichTextBox?>(appendToOutput), text, color, box);
            }
            else
            {
                int start = box.TextLength;
                box.AppendText(text + "\n");
                int end = box.TextLength;

                // Textbox may transform chars, so (end-start) != text.Length
                box.Select(start, end - start);
                {
                    box.SelectionColor = col;
                    // could set box.SelectionBackColor, box.SelectionFont too.
                }
                box.SelectionLength = 0; // clear
                box.SelectionStart = end;
                box.ScrollToCaret();
            }
            
        }

        private void stop_Click(object sender, EventArgs e)
        {
            destoryClient();
            destoryServer();
            IsServer = false;
            IsClient = false;
            clientBtn.Enabled = true;
            serverBtn.Enabled = true;
        }

        private void sendMessageBtn_Click(object sender, EventArgs e)
        {
            appendToOutput(messageBox.Text);
            if (IsClient)
            {
                ClientSendMessage(messageBox.Text);
            }
            else if (IsServer)
            {
                serverSendMessage(messageBox.Text);
            }
            else
            {
                appendToOutput("Start a Server or a Client before sending messages");
            }
            messageBox.Text = string.Empty;
        }

        private void serverSendMessage(string message)
        {
            if (server != null)
            {
                foreach (var cli in server.GetClients())
                {
                    server.Send(cli, message);
                }
            }
            else
            {
                appendToOutput("null server!", Color.Red);
            }
            
        }

        private void serverSendMessage(byte[] message)
        {
            if (server != null)
            {
                foreach (var cli in server.GetClients())
                {
                    server.SendAsync(cli, message);
                }
            }
            else
            {
                appendToOutput("null server!", Color.Red);
            }

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop_Click(sender, e);
            SharpHook.Hook.Dispose();
            
            System.Environment.Exit(0);
            //Process.GetCurrentProcess().Kill();
            //var _ = 1 + 1;
            //System.Windows.Forms.Application.Exit();
        }
        #endregion




        private void destoryClient()
        {
            if (client != null)
            {
                //client.Disconnect();
                //client.Dispose();
                client = null;
            }

        }

        private void destoryServer()
        {
            if (server != null)
            {
                server.Stop();
                server.Dispose();
                server = null;
            }
        }


    }
}