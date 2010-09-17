using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Net;
using NCRVisual.RelationDiagram.Algo;
using System.Xml;

namespace NCRVisual.RelationDiagram
{
    public class DiagramController
    {
        #region constant

        string INPUT_FILE_NAME = "..\\Output\\output.xml";
        string ID_TAG = "UserId";
        string NAME_TAG = "Name";
        string EMAIL_TAG = "Email";
        string VERTEX_TAG = "Vertex";
        string EDGE_TAG = "Edge";
        string START_EDGE_TAG = "Start";
        string END_EDGE_TAG = "End";
        string EDGE_VALUE_TAG = "Value";

        #endregion

        #region private
        private int[][] _input = new int[100][];
        private int _vertexNumber = 0;

        #endregion

        /// <summary>
        /// Event after reading input from data provider
        /// </summary>
        public EventHandler InputReadingComplete;

        /// <summary>
        /// Get or set the LayoutAlgorithm 
        /// </summary>
        public IAlgorithm LayoutAlgorithm { get; set; }

        /// <summary>
        /// Get or set the Entity Collection
        /// </summary>
        public Collection<MailListEntity> entityCollection { get; set; }

        /// <summary>
        /// Create new instance of Diagram controller
        /// </summary>
        public DiagramController()
        {
            LayoutAlgorithm = new FruchtermanAlgorithm();
            entityCollection = new Collection<MailListEntity>();

            //getfile from output.txt
            WebClient client = new WebClient();
            client.OpenReadAsync(new Uri(INPUT_FILE_NAME, UriKind.Relative));
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
        }

        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            using (XmlReader reader = XmlReader.Create(e.Result))
            {
                ReadRelation(reader);
            }

            Collection<Point> tempPoints = LayoutAlgorithm.RunAlgo(_input, _vertexNumber);

            if (this.InputReadingComplete != null)
            {
                this.InputReadingComplete(tempPoints, null);
            };
        }

        private void populateConnection(int[][] matrixInput)
        {
            for (int i = 0; i < _vertexNumber; i++)
            {
                for (int j = 0; j < _vertexNumber; j++)
                {
                    if (matrixInput[i][j] > 0 && i != j)
                    {                        
                        entityCollection[i].Connections.Add(entityCollection[j]);
                    }
                }
            }
        }

        void ReadRelation(XmlReader reader)
        {
            reader.MoveToContent();
            int count = 0;

            //Start reading xml
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == VERTEX_TAG)
                {

                    MailListEntity entity = new MailListEntity();

                    //Read Userid
                    reader.ReadToFollowing(ID_TAG);
                    string id = reader.ReadElementContentAsString();

                    //Read Email
                    reader.ReadToFollowing(EMAIL_TAG);
                    string email = reader.ReadElementContentAsString();

                    //Read Name
                    reader.ReadToFollowing(NAME_TAG);
                    string name = reader.ReadElementContentAsString();

                    entity.UserId = id;
                    entity.UserId = name;
                    entity.Email = email;
                    entity.Name = email;

                    this.entityCollection.Add(entity);

                    //Build matrix for connection
                    _input[count] = new int[100];

                    while (reader.ReadToNextSibling(EDGE_TAG))
                    {
                        reader.ReadToFollowing(START_EDGE_TAG);
                        int start = reader.ReadElementContentAsInt();
                        reader.ReadToFollowing(END_EDGE_TAG);
                        int end = reader.ReadElementContentAsInt();
                        reader.ReadToFollowing(EDGE_VALUE_TAG);
                        int value = reader.ReadElementContentAsInt();

                        _input[start - 1][end - 1] = value;
                        reader.Read();
                    }

                    count++;
                }
            }

            _vertexNumber = count;
            populateConnection(_input);
        }
    }
}
