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
        string EDGE_CONTENT_TAG = "Content";
        string EDGE_DATE_TAG = "Date";
        string EDGE_SUBJECT_TAG = "Subject";

        #endregion

        #region private
        private int[][] _input = new int[100][];
        #endregion

        /// <summary>
        /// Event after reading input from data provider
        /// </summary>
        public event EventHandler InputReadingComplete;

        /// <summary>
        /// Event after running layoutAlgorithm
        /// </summary>
        public event EventHandler AlgoRunComplete;

        /// <summary>
        /// Get or set the Entity Collection
        /// </summary>
        public Collection<MailListEntity> entityCollection { get; set; }

        /// <summary>
        /// Number of vertex
        /// </summary>
        public int VertexNumber = 0;

        public Point TopLeft;
        public Point LowRight;

        /// <summary>
        /// Create new instance of Diagram controller
        /// </summary>
        public DiagramController()
        {
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

            if (this.InputReadingComplete != null)
            {
                this.InputReadingComplete(null, null);
            };
        }

        private void populateConnection(int[][] matrixInput)
        {
            for (int i = 0; i < VertexNumber; i++)
            {
                for (int j = 0; j < VertexNumber; j++)
                {
                    if (matrixInput[i][j] > 0 && i != j)
                    {
                        entityCollection[i].Connections.Add(new Connection(entityCollection[i], entityCollection[j]));
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

                        while (reader.ReadToNextSibling(EDGE_CONTENT_TAG))
                        {
                            reader.ReadToFollowing(EDGE_DATE_TAG);
                            string time = reader.ReadElementContentAsString();
                            //entity.SendDate.Add(time);
                            reader.ReadToFollowing(EDGE_SUBJECT_TAG);
                            //entity.MessageSubject.Add(reader.ReadElementContentAsString());
                        }
                        reader.Read();
                    }

                    this.entityCollection.Add(entity);
                    count++;
                }
            }

            VertexNumber = count;
            populateConnection(_input);
        }

        public void RunAlgo(IAlgorithm algorithm)
        {
            double maxX = 0;
            double minX = 0;
            double maxY = 0;
            double minY = 0;

            Collection<Point> tempPoints = algorithm.RunAlgo(_input, VertexNumber);

            foreach (Point p in tempPoints)
            {
                if (p.X > maxX)
                {
                    maxX = p.X;
                }

                if (p.X < minX)
                {
                    minX = p.X;
                }

                if (p.Y > maxY)
                {
                    maxY = p.Y;
                }

                if (p.X < minY)
                {
                    minY = p.Y;
                }
            }

            this.TopLeft = new Point(minX, minY);
            this.LowRight = new Point(maxX, maxY);
            if (this.AlgoRunComplete != null)
            {
                this.AlgoRunComplete(tempPoints, null);
            };
        }
    }
}
