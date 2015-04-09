using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventuz.OSC;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    public class OscConnection : INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the osc target ip address
        /// </summary>
        private string _ip = "127.0.0.1";
        public string IP
        {
            get { return _ip; }
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    if (_oscConnect)
                        this.InitOscUdpWriter();
                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IP"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the osc target port
        /// </summary>
        private int _port = 12000;
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    if (_oscConnect)
                        this.InitOscUdpWriter();
                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Port"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the osc identifier
        /// </summary>
        private string _identifier;
        public string Identifier
        {
            get { return _identifier; }
            set
            {
                _identifier = value;
                // notify any bound elements that the text has changed
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Identifier"));
                }
            }
        }

        /// <summary>
        /// Gets or sets if osc is connected
        /// </summary>
        private bool _oscConnect = false;
        public bool OscConnect
        {
            get { return _oscConnect; }
            set
            {
                if (_oscConnect != value)
                {
                    _oscConnect = value;
                    if (_oscConnect)
                    {
                        this.InitOscUdpWriter();
                    }
                    else
                    {
                        this.ResetOscUdpWriter();
                    }
                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("OscConnect"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets current osc status
        /// </summary>
        private string _status = "Offline";
        public string Status { 
            get { return _status; } 
            set {
                _status = value;

                // notify any bound elements that the text has changed
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            } 
        }

        public UdpWriter OscUdpWriter {get; private set; }

        public OscConnection()
        {
            this.OscConnect = false;
            this.Status = "Offline";
        }

        /// <summary>
        /// Initiate OSC UdpWriter
        /// </summary>
        private void InitOscUdpWriter()
        {
            if (this.OscUdpWriter != null)
                this.ResetOscUdpWriter();
            if (this.IP != null && this.IP != "" && this.Port > 0)
            {
                OscUdpWriter = new UdpWriter(this.IP, this.Port);
                this.Status = "Connected to " + this.IP + ":" + this.Port.ToString();
            }
        }

        /// <summary>
        /// Reset OSC UdpWriter
        /// </summary>
        private void ResetOscUdpWriter()
        {
            if (this.OscUdpWriter != null)
            {
                this.OscUdpWriter.Dispose();
                this.OscUdpWriter = null;
                this.Status = "Offline";
            }
        }
    }
}
