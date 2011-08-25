using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections;
using OpenTK;
using OpenTK.Graphics;

namespace Game
{
	public class Terrain
	{
		// Heightmap
		private float[,] Heightmap = null;
		private int Width = 0;
		private int Depth = 0;

		/*
		// Texturen
		private Texture Tex0 = null;
		private Texture Tex1 = null;
		private Texture Tex2 = null;
		*/
		// VertexBuffer
		public VertexBuffer Vertexbuffer = null;

		// IndexBuffer
		public IndexBuffer Indexbuffer = null;

		// Vertices
		private int NumberOfVertices = 0;
		private PositionNormalMultiTextured[] Vertices;
		private float XScale = 1.0f;
		private float YScale = 1.0f;
		private float ZScale = 1.0f;

		public struct PositionNormalMultiTextured
		{
			public Vector3 Position;
			public Vector3 Normal;
			public float Tu0, Tv0;
			public float Tu1, Tv1;
			public float Tu2, Tv2;
			public static readonly VertexFormats Format = VertexFormats.PositionNormal | VertexFormats.Texture1 | VertexFormats.Texture2 | VertexFormats.Texture3;

			public PositionNormalMultiTextured(float x, float y, float z, float nx, float ny, float nz, float tu0, float tv0, float tu1, float tv1, float tu2, float tv2)
			{
				this.Position = new Vector3(x,y,z);
				this.Normal = new Vector3(nx,ny,nz);
				this.Tu0 = tu0;
				this.Tv0 = tv0;
				this.Tu1 = tu1;
				this.Tv1 = tv1;
				this.Tu2 = tu2;
				this.Tv2 = tv2;
			}

			public PositionNormalMultiTextured(Vector3 position, Vector3 normal, Vector2 tex0, Vector2 tex1, Vector2 tex2)
			{
				this.Position = position;
				this.Normal = normal;
				this.Tu0 = tex0.X;
				this.Tv0 = tex0.Y;
				this.Tu1 = tex1.X;
				this.Tv1 = tex1.Y;
				this.Tu2 = tex2.X;
				this.Tv2 = tex2.Y;
			}
		}

		// Indices
		private int NumberOfIndices = 0;
		private int[] Indices;

		// GeoMipMapping
		private TerrainBlock[] tBlocks = null;

		#endregion

		#region Konstruktoren

		// Standardkonstruktor
		public Terrain(Device device)
		{
			// Schnittstelle
			dev = device;	
		}

		#endregion

		#region Eigenschaften

		// GeoMipMapping Ein/Aus
		public bool GeoMipMapping = false;

		#endregion

		#region Methoden
		
		// VertexBuffer erzeugen
		// 0: True  1: False
		private int CreateVertexBuffer()
		{
			try
			{
				Vertexbuffer = new VertexBuffer(typeof(PositionNormalMultiTextured), NumberOfVertices, dev, Usage.Dynamic | Usage.WriteOnly, PositionNormalMultiTextured.Format, Pool.Default);
				Vertexbuffer.SetData(Vertices, 0, LockFlags.None);

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		// IndexBuffer erzeugen
		// 0: True  1: False
		private int CreateIndexBuffer()
		{
			try
			{
				Indexbuffer = new IndexBuffer(typeof(int), Indices.Length, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer.SetData(Indices, 0, LockFlags.None);

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		// Vertexliste erzeugen
		// 0: True  1: False (Vertexberechnung)  2: False (Normalenberechnung)  3: False (Vertexbuffer)
		private int CreateVertices()
		{
			// Deklarationen
			short[,] VertexPosition = {{0,0},{1,-1},{1,0},{1,1},{0,1},{-1,1},{-1,0},{-1,-1,},{0,-1}};
			short[,] Faces = {{0,1,8},{0,2,1},{0,4,2},{4,3,2},{0,5,4},{0,6,5},{0,8,6},{8,7,6}};

			Vector3[] TempVertices = new Vector3[9];
			Vector3[] Normals      = new Vector3[8];

			try
			{
				// Vertexberechnung
				NumberOfVertices = Width * Depth;

				Vertices = new PositionNormalMultiTextured[NumberOfVertices];

				for (int x=0; x<Width; x++)
				{
					for (int z=0; z<Depth; z++)
					{
						Vertices[z+x*Depth] = new PositionNormalMultiTextured(x*XScale,Heightmap[x,z]*YScale,z*ZScale,0.0f,1.0f,0.0f,1.0f/Width*x,1.0f/Depth*z,1.0f/Width*x,1.0f/Depth*z,x/20.0f,z/20.0f);
					}
				}
			}
			catch
			{
				return 1;
			}

			try
			{
				// Normalenberechnung
				for (int x=0; x<Width; x++)
				{
					for (int z=0; z<Depth; z++)
					{
						// Vertices
						for (int a=0; a<9; a++)
						{	
							try
							{
								TempVertices[a].X = XScale*(x + VertexPosition[a,0]);
								TempVertices[a].Z = ZScale*(z + VertexPosition[a,1]);
								TempVertices[a].Y = YScale*(Heightmap[x + VertexPosition[a,0],z + VertexPosition[a,1]]);
							}
							catch 
							{
								TempVertices[a].X = 0;
								TempVertices[a].Y = 0;
								TempVertices[a].Z = 0;
							}
						}

						// Normalen
						for (int a=0; a<8; a++)
						{
							Normals[a] = Vector3.Cross(Vector3.Normalize(Vector3.Subtract(TempVertices[Faces[a,0]],TempVertices[Faces[a,1]])),
								         Vector3.Normalize(Vector3.Subtract(TempVertices[Faces[a,1]],TempVertices[Faces[a,2]])));
							Normals[a] = Vector3.Normalize(Normals[a]);
						}

						// Vertexnormale
						for (int a=1; a<8; a++)
						{
							Normals[0] = Vector3.Add(Normals[0],Normals[a]);
						}

						Vertices[z+x*Depth].Normal.X = Normals[0].X;
						Vertices[z+x*Depth].Normal.Y = Normals[0].Y;
						Vertices[z+x*Depth].Normal.Z = Normals[0].Z;
					}
				}
			}
			catch
			{
				return 2;
			}

			// Vertexbuffer erzeugen
			if (CreateVertexBuffer() == 1) return 3;

			return 0;
		}

		// Indexliste erzeugen
		// 0: True  1: False (Indexberechnung)  2: False (Indexbuffer)
		private int CreateIndices()
		{
			
			// Anzahl der Indices
			NumberOfIndices = (Width-1)*(Depth-1)*6;

			// Indices berechnen
			Indices = new int[NumberOfIndices];
				
			try
			{
				int counter = 0;

				for (int x=0; x<Width-1; x++)
				{
					for (int y=0; y<Depth-1; y++)
					{
						Indices[counter+0] = Convert.ToInt32( x    * Depth + (y+1));
						Indices[counter+1] = Convert.ToInt32( x    * Depth +  y   );
						Indices[counter+2] = Convert.ToInt32((x+1) * Depth +  y   ); 
						Indices[counter+3] = Convert.ToInt32( x    * Depth + (y+1));
						Indices[counter+4] = Convert.ToInt32((x+1) * Depth +  y   );
						Indices[counter+5] = Convert.ToInt32((x+1) * Depth + (y+1));

						counter = counter + 6;
					}
				}
			}
			catch
			{
				return 1;
			}

			// Indexbuffer erzeugen
			if (CreateIndexBuffer() == 1) return 2;

			return 0;
		}

		// Terrainblocks erzeugen (GeoMipMapping)
		// 0: True  1: False
		private int CreateTerrainBlocks()
		{
			try
			{
				// Deklarationen
				int startVertex = 0;
				int counter1 = 0;
				int counter2 = 0;
				PositionNormalMultiTextured[] verts = new PositionNormalMultiTextured[289];

				// Initialisierung
				tBlocks = new TerrainBlock[(((Width-1)/16))*((Depth-1)/16)];

				for (int x=0; x<Width-1; x+=16)
				{
					for (int z=0; z<Depth-1; z+=16)
					{
						startVertex = z+x*Depth;
						counter1 = 0;

						for (int u=0; u<17; u++)
						{
							for (int v=0; v<17; v++)
							{
								verts[counter1] = Vertices[startVertex+v+u*(Depth)];
								counter1 ++;
							}
						}

						tBlocks[counter2] = new TerrainBlock(dev,verts,3);
						counter2 ++;
					}
				}

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		// Distanz zweier Vektoren im Quadrat
		// 0: True  1: False
		private int DistanceQuad(Vector3 a, Vector3 b, out float d)
		{
			d = 0.0f;
			
			try
			{
				d = (float) ((a.X-b.X)*(a.X-b.X)+(a.Y-b.Y)*(a.Y-b.Y)+(a.Z-b.Z)*(a.Z-b.Z));

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		// Terrain normalisieren
		// 0: True  1: False
		private int NormalizeTerrain()
		{
			try
			{
				// Minimum und Maximum finden
				float min = Heightmap[0,0];
				float max = Heightmap[0,0];

				for (int x=0; x<Width; x++)
				{
					for (int z=0; z<Depth; z++)
					{
						if (Heightmap[x,z] < min)
						{
							min = Heightmap[x,z];
						}
						else if (Heightmap[x,z] > max)
						{
							max = Heightmap[x,z];
						}
					}
				}

				// Normalisieren
				for (int x=0; x<Width; x++)
				{
					for (int z=0; z<Depth; z++)
					{
						Heightmap[x,z] = (Heightmap[x,z]-min)/(max-min);
					}
				}

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		// Terrain aus Heightmap laden
		// 0: True  1: False (Pfad)  2: False (Normalisierung)  3: False (Vertices - Vertexberechnung)  4: False (Vertices - Normalenberechnung)  
		// 5: False (Vertices - Vertexbuffer)  6: False (Indices - Indexberechnung)  7: False (Indices - Indexbuffer)  8: False (Terrainblocks - Blockberechnung)
		public int LoadHeightmap(string file)
		{
			if (File.Exists(file))
			{
				// Deklarationen
				int error = 0;

				// Heightmap laden
				Bitmap map = Bitmap.FromFile(file) as Bitmap;	
				Width = map.Width;
				Depth = map.Height;

				// Heightmap in Array speichern (0.0f...1.0f)
				Heightmap = new float[Width,Depth];

				for (int x=0; x<Width; x++)
				{
					for (int z=0; z<Depth; z++)
					{
						Heightmap[x,z] = map.GetPixel(x,z).GetBrightness();
					}
				}

				if (NormalizeTerrain() == 1) return 2;
				
				error = CreateVertices();
				if      (error == 1) return 3;
				else if (error == 2) return 4;
				else if (error == 3) return 5;

				error = CreateIndices();
				if      (error == 1) return 6;
				else if (error == 2) return 7;

				error = CreateTerrainBlocks();
				if      (error == 1) return 8;

				return 0;
			}
			else
			{
				return 1;
			}
		}

		// Textur laden
		// 0: True  9: False (Pfad)  10: False (Stage)
		public int LoadTexture(string file, int stage)
		{
			if (File.Exists(file))
			{
				// Laden der Texturen
				switch (stage)
				{
					case 0 : Tex0 = TextureLoader.FromFile(dev, file); break;
					case 1 : Tex1 = TextureLoader.FromFile(dev, file); break;
					case 2 : Tex2 = TextureLoader.FromFile(dev, file); break;
					default: return 10;
				}
				
				return 0;
			}	
			else
			{
				return 9;
			}
		}

		// Terrain skalieren
		// 0: True  11: False
		public int ScaleTerrain(float x, float y, float z)
		{
			try
			{
				XScale *= x;
				YScale *= y;
				ZScale *= z;

				return 0;
			}
			catch
			{	
				return 1;
			}
		}

		// Terrain Blocks neu berechnen
		// 0: True  12: False (Blockupdate)  13: False (Distanz)
		public int UpdateTerrainBlocks(Vector3 camera)
		{
			// Deklarationen
			int length = tBlocks.Length;
			int lengthSqrt = (int) Math.Sqrt(tBlocks.Length);
			int stage = 0;
			float d = 0;

			try
			{
				// Level of Detail
				for (int x=0; x<length; x++)
				{
					if (DistanceQuad(camera,tBlocks[x].CenterVertex, out d) == 1) return 13;
			
					if      (d < 2500)  stage = 3;
					else if (d < 5000)  stage = 2;
					else if (d < 10000) stage = 1;
					else                stage = 0;

					tBlocks[x].Stage = stage;
				}

				return 0;
			}
			catch
			{
				return 12;
			}
		}

		// Terrain zeichnen
		// 0: True  14: False (Einstellungen)  15: False (GeoMipMapping)  16: False (Normal)
		public int DrawTerrain(float x, float y, float z)
		{
			try
			{
				// Einstellungen
				dev.Transform.World = Matrix.Scaling(XScale,YScale,ZScale) * Matrix.Translation(x,y,z);
				dev.SetTexture(0,Tex0);
				dev.TextureState[0].ColorArgument0 = TextureArgument.TextureColor;
				dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
				dev.SetTexture(1,Tex1);
				dev.TextureState[1].ColorArgument0 = TextureArgument.Current;
				dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
				dev.TextureState[1].ColorOperation = TextureOperation.Modulate;
				dev.SetTexture(2,Tex2);
				dev.TextureState[2].ColorArgument0 = TextureArgument.Current;
				dev.TextureState[2].ColorArgument1 = TextureArgument.TextureColor;
				dev.TextureState[2].ColorOperation = TextureOperation.Modulate;
			}
			catch
			{
				return 24;
			}

			if (GeoMipMapping)
			{
				try
				{
					// Rendern der Terrainblocks
					foreach (TerrainBlock t in tBlocks)
					{
						t.DrawTerrainBlock();
					}
				}
				catch
				{
					return 25;
				}
			}
			else
			{
				try
				{
					// Rendern des Terrains
					dev.VertexFormat = PositionNormalMultiTextured.Format;
					dev.SetStreamSource(0, Vertexbuffer, 0);
					dev.Indices = Indexbuffer;
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, Indices.Length/3);
				}
				catch
				{
					return 26;
				}
			}

			return 0;
		}

		// Umwandeln von Zahlencodes in Nachrichten
		public string MessageFilter (int code)
		{
			string message = "Unbekannter Fehler";

			switch (code)
			{
				case  1: message = "(Error 01) LoadHeightmap(): Datei konnte nicht gefunden werden!";										break;
				case  2: message = "(Error 02) LoadHeightmap(): Normalisierung wurde aufgrund eines Fehlers abgebrochen!";					break;
				case  3: message = "(Error 03) LoadHeightmap(): Vertexberechnung wurde aufgrund eines Fehlers abgebrochen!";				break;
				case  4: message = "(Error 04) LoadHeightmap(): Normalenberechnung wurde aufgrund eines Fehlers abgebrochen!";				break; 
				case  5: message = "(Error 05) LoadHeightmap(): Vertexbuffer konnte nicht erstellt werden!";								break;
				case  6: message = "(Error 06) LoadHeightmap(): Indexberechnung wurde aufgrund eines Fehlers abgebrochen!";					break;
				case  7: message = "(Error 07) LoadHeightmap(): Indexbuffer konnte nicht erstellt werden!";									break;
				case  8: message = "(Error 08) LoadHeightmap(): Blockberechnung wurde aufgrund eines Fehlers abgebrochen!";					break;
				case  9: message = "(Error 09) LoadTexture(): Datei konnte nicht gefunden werden!";											break;
				case 10: message = "(Error 10) LoadTexture(): Level der Texturschicht ist ungueltig!";										break;
				case 11: message = "(Error 11) ScaleTerrain(): Skalierung der Terrains wurde aufgrund eines Fehlers abgebrochen!";          break;
				case 12: message = "(Error 25) UpdateTerrainBlocks(): Berechnung der Blockdaten wurde aufgrund eines Fehlers abgebrochen!"; break;
				case 13: message = "(Error 26) UpdateTerrainBlocks(): Berechnung der Distanz wurde aufgrund eines Fehlers abgebrochen!";    break; 
				case 14: message = "(Error 30) DrawTerrain(): Initialisierung der Einstellungen wurde aufgrund eines Fehlers abgebrochen!"; break;
				case 15: message = "(Error 31) DrawTerrain(): Rendervorgang (GeoMipMapping) wurde aufgrund eines Fehlers abgebrochen!";     break;
				case 16: message = "(Error 32) DrawTerrain(): Rendervorgang (Normal) wurde aufgrund eines Fehlers abgebrochen!";            break;
			}

			 return message;
		}

		#endregion
	}

	public class TerrainBlock : Render.Renderable
	{
		private Vector3 centerVertex;
		private int center = 0;

		// Indexbuffer
		// private IndexBuffer[] Indexbuffer = new IndexBuffer[8];

		// Vertices
		private const short NumberOfVertices = 289;
		#endregion

		// Standardkonstruktor
		public TerrainBlock(Render.Vertex[] vertices, int stage)
		{
			if      (stage > 4) center = 4;
			else if (stage < 0) center = 0;
			else            center = stage;

			// Initialisierung
			centerVertex = vertices[144].Position;
			VB = new Game.Render.VertexBuffer();
			VB.Create(Render.VBOType.No, Render.BufferUsage.DYNAMIC, NumberOfVertices);
			VB.Vertices = vertices;

			CreateIndexBuffer();
		}

		#endregion

		#region Eigenschaften

		// Vertex im Zentrum des Terrainblocks
		public Vector3 CenterVertex
		{
			get 
			{ 
				return centerVertex; 
			}
		}

		// Detailstufe des Terrainblocks
		public int Stage
		{
			get 
			{ 
				return center; 
			}
			set 
			{
				if (value > 4) 
				{
					center = 4;
				}
				else if (value < 0)
				{
					center = 0;	
				}
				else
				{
					center = value;
				}
			}
		}

		#endregion

		#region Methoden

		// IndexBuffer erzeugen
		// 0: True  1: False
		private int CreateIndexBuffer()
		{
			try
			{
				// Deklarationen
				int Counter = 0;

				// Stage 0
				short[] a = {144, 72, 208, 216, 80, 72};

				Indexbuffer[0] = new IndexBuffer(typeof(short), 14, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[0].SetData(a, 0, LockFlags.None);

				// Stage 1
				short[] b = {36,104,40,40,104,108,104,172,108,108,172,176,172,240,176,176,240,244,
							 40,108,44,44,108,112,108,176,112,112,176,180,176,244,180,180,244,248,
							 44,112,48,48,112,116,112,180,116,116,180,184,180,248,184,184,248,252};

				Indexbuffer[1] = new IndexBuffer(typeof(short), 54, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[1].SetData(b, 0, LockFlags.None);

				// Stage 2
				short[] c = new short[216];
				
				for (int x=2; x<14; x+=2)
				{
					for (int z=2; z<14; z+=2)
					{
						c[Counter+0] = Convert.ToInt16( x   *17+z+2);
						c[Counter+1] = Convert.ToInt16( x   *17+z  );
						c[Counter+2] = Convert.ToInt16((x+2)*17+z  );
						c[Counter+3] = Convert.ToInt16( x   *17+z+2);
						c[Counter+4] = Convert.ToInt16((x+2)*17+z  );
						c[Counter+5] = Convert.ToInt16((x+2)*17+z+2);
						Counter += 6;
					}
				}

				Counter = 0;

				Indexbuffer[2] = new IndexBuffer(typeof(short), 216, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[2].SetData(c, 0, LockFlags.None);

				// Stage 3
				short[] d = new short[1176];

				for (int x=1; x<15; x+=1)
				{
					for (int z=1; z<15; z+=1)
					{
						d[Counter+0] = Convert.ToInt16( x   *17+z+1);
						d[Counter+1] = Convert.ToInt16( x   *17+z  );
						d[Counter+2] = Convert.ToInt16((x+1)*17+z  );
						d[Counter+3] = Convert.ToInt16( x   *17+z+1);
						d[Counter+4] = Convert.ToInt16((x+1)*17+z  );
						d[Counter+5] = Convert.ToInt16((x+1)*17+z+1);
						Counter += 6;
					}
				}

				Indexbuffer[3] = new IndexBuffer(typeof(short), 1176, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[3].SetData(d, 0, LockFlags.None);

				// Nebenbereich Stage 0
				short[] e = {0,72,4,4,72,8,8,72,80,8,80,12,12,80,16,
							 16,80,84,84,80,152,152,80,216,152,216,220,220,216,288,
							 288,216,284,284,216,280,280,216,208,280,208,276,276,208,272,
							 0,68,72,68,136,72,136,208,72,136,204,208,204,272,208}; 

				Indexbuffer[4] = new IndexBuffer(typeof(short), 60, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[4].SetData(e, 0, LockFlags.None);

				// Nebenbereich Stage 1
				short[] f = {0,36,4,4,36,40,4,40,8,8,40,44,8,44,12,12,44,48,12,48,16,
							 16,48,84,84,48,116,84,116,152,152,116,184,152,184,220,220,184,252,220,252,288,
							 288,252,284,284,252,248,284,248,280,280,248,244,280,244,276,276,244,240,276,240,272,
							 0,68,36,68,104,36,68,136,104,136,172,104,136,204,172,204,240,172,204,272,240};

				Indexbuffer[5] = new IndexBuffer(typeof(short), 84, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[5].SetData(f, 0, LockFlags.None);

				// Nebenbereich Stage 2
				short[] g = {0,36,4,4,36,38,4,38,40,4,40,8,8,40,42,8,42,44,8,44,12,12,44,46,12,46,48,12,48,16,
							 16,48,84,84,48,82,84,82,116,84,116,152,152,116,150,152,150,184,152,184,220,220,184,218,220,218,252,220,252,288,
							 288,252,284,284,252,250,284,250,248,284,248,280,280,248,246,280,246,244,280,244,276,276,244,242,276,242,240,276,240,272,
							 0,68,36,68,104,70,68,70,36,68,136,104,136,172,138,136,138,104,136,204,172,204,240,206,204,206,172,204,272,240};

				Indexbuffer[6] = new IndexBuffer(typeof(short), 120, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[6].SetData(g, 0, LockFlags.None);

				// Nebenbereich Stage 3
				short[] h = {0,18,19,0,19,4,4,19,20,4,20,21,4,21,22,4,22,23,4,23,8,8,23,24,8,24,25,8,25,26,8,26,27,8,27,12,12,27,28,12,28,29,12,29,30,12,30,31,12,31,16,16,31,32,
							 16,32,49,16,49,84,84,49,66,84,66,83,84,83,100,84,100,117,84,117,152,152,117,134,152,134,151,152,151,168,152,168,185,152,185,220,220,185,202,220,202,219,220,219,236,220,236,253,220,253,288,288,253,270,
							 288,270,269,288,269,284,284,269,268,284,268,267,284,267,266,284,266,265,284,265,280,280,265,264,280,264,263,280,263,262,280,262,261,280,261,276,276,261,260,276,260,259,276,259,258,276,258,257,276,257,272,272,257,256,
							 0,35,18,0,68,35,68,136,103,68,103,86,68,86,69,68,69,52,68,52,35,136,204,171,136,171,154,136,154,137,136,137,120,136,120,103,204,272,239,204,239,222,204,222,205,204,205,188,204,188,171,272,256,239};

				Indexbuffer[7] = new IndexBuffer(typeof(short), 216, dev, Usage.WriteOnly, Pool.Default);
				Indexbuffer[7].SetData(h, 0, LockFlags.None);

				return 0;
			}
			catch
			{
				return 1;
			}	
		}

		// Terrainblock rendern
		// 0: True  1: False
		public int DrawTerrainBlock()
		{
			try
			{
				// Einstellungen
				dev.VertexFormat = Terrain.PositionNormalMultiTextured.Format;
				dev.SetStreamSource(0, Vertexbuffer, 0);

				// Rendervorgang
				if (center == 0)
				{
					dev.Indices = Indexbuffer[0];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleFan, 0, 0, NumberOfVertices, 0, 4);
					dev.Indices = Indexbuffer[4];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 20);
				}
				else if (center == 1)
				{
					dev.Indices = Indexbuffer[1];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 18);
					dev.Indices = Indexbuffer[5];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 28);
				}
				else if (center == 2)
				{
					dev.Indices = Indexbuffer[2];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 72);
					dev.Indices = Indexbuffer[6];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 40);
				}
				else if (center == 3)
				{
					dev.Indices = Indexbuffer[3];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 392);
					dev.Indices = Indexbuffer[7];
					dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumberOfVertices, 0, 72);
				}

				return 0;
			}
			catch
			{
				return 1;
			}	
		}

		#endregion
	}
}
