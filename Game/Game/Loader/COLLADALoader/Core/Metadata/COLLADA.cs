using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class COLLADA : Extensible,IHasAttribute,IHasChildNode,IHasAsset
	{
		public Uri Base;
		public Uri XMLNS;
		public Version Version;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "version":
					Version	= new Version(Attr.Value);
					break;
				case "xmlns":
					XMLNS	= new Uri(Attr.Value);
					break;
				case "base":
					Base	= new Uri(Attr.Value);
					break;
					
				default:
					throw new Exception("Invalid Attribute");
			}
		}

		public asset Asset;
		void IHasAsset.Add(asset ChildAsset)
		{
			Asset	= ChildAsset;
		}

		public scene Scene;

		public library_animation_clips AnimationClips;
		public library_animations Animations;
		public library_cameras Cameras;
		public library_controllers Controllers;
		public library_effect Effects;
		public library_formulas Formulars;
		public library_geometries Geometries;
		public library_image Images;
		public library_lights Lights;
		public library_materials Materials;
		public library_nodes Nodes;
		public library_visual_scenes VisualScenes;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "scene":
					Scene	= Doc.Load<scene>(this,Child);
					break;

				case "library_animation_clips":
					AnimationClips	= Doc.Load<library_animation_clips>(this,Child);
					break;

				case "library_animations":
					Animations	= Doc.Load<library_animations>(this,Child);
					break;

				case "library_articulated_systems":
					break;

				case "library_cameras":
					Cameras	= Doc.Load<library_cameras>(this,Child);
					break;

				case "library_controllers":
					Controllers	= Doc.Load<library_controllers>(this,Child);
					break;

				case "library_effects":
					break;

				case "library_force_fields":
					break;

				case "library_formulas":
					Formulars	= Doc.Load<library_formulas>(this,Child);
					break;

				case "library_geometries":
					Geometries	= Doc.Load<library_geometries>(this,Child);
					break;

				case "library_images":
					Images	= Doc.Load<library_image>(this,Child);
					break;

				case "library_joints":
					break;

				case "library_kinematics_models":
					break;

				case "library_kinematics_scenes>":
					break;

				case "library_lights":
					Lights	= Doc.Load<library_lights>(this,Child);
					break;

				case "library_materials":
					Materials	= Doc.Load<library_materials>(this,Child);
					break;

				case "library_nodes":
					Nodes	= Doc.Load<library_nodes>(this,Child);
					break;

				case "library_physics_materials":
					break;

				case "library_physics_models":
					break;

				case "library_physics_scenes":
					break;

				case "library_visual_scenes":
					VisualScenes	= Doc.Load<library_visual_scenes>(this,Child);
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}

		Dictionary<string,IElement> IDElements	= new Dictionary<string,IElement>();
		public IElement this[string ID]
		{
			get{return IDElements[ID];}
			set{IDElements[ID]	= value;}
		}

		public static COLLADA Load(string Path)
		{
			if(!File.Exists(Path))
				throw new FileNotFoundException("Cannot find file",Path);

			XmlDocument XD	= new XmlDocument();
			XD.Load(Path);

			XmlNode ColladaNode	= XD["COLLADA"];
			if(ColladaNode == null)
				throw new Exception("Invalid format");

			COLLADA Doc	= new COLLADA();
			
			Doc.Load(Doc,null,ColladaNode);

			return Doc;
		}
		
////////////////////////////////////////////////////////////////

		protected readonly Dictionary<IElement,Dictionary<string,IElement>> Scoped	= new Dictionary<IElement,Dictionary<string,IElement>>();
		internal T Load<T>(IElement Parent,XmlNode Node) where T : IElement,new()
		{
			T TElement	= new T();

			Load(TElement,Parent,Node);

			return TElement;
		}

////////////////////////////////////////////////////////////////

		void Load(IElement Current,IElement Parent,XmlNode Node)
		{
			if(Current is IHasDocument)
				(Current as IHasDocument).Doc	= this;

			foreach(XmlAttribute Attr in Node.Attributes)
				LoadAttribute(Current,Parent,Attr);

			foreach(XmlNode Child in Node.ChildNodes)
				LoadChildNode(Current,Child);
		}

		public readonly Dictionary<string,IElement> SymbalList	= new Dictionary<string,IElement>();
		void LoadAttribute(IElement Current,IElement Parent,XmlAttribute Attr)
		{
			string Value	= Attr.Value;
			switch(Attr.Name)
			{
				case "url":
				case "source":
				case "target":
					Value	= Attr.Value;
					if(Value.StartsWith("#"))
						Value	= Value.Remove(0,1);

					break;
			}

			switch(Attr.Name)
			{
				case "id":
					(Current as IHasID).ID	= Value;
					IDElements[Attr.Value]	= Current;
					break;

				case "name":
					(Current as IHasName).Name	= Value;
					break;

				case "sid":
					(Current as IHasScopedID).SID	= Value;
					if(!Scoped.ContainsKey(Parent))
						Scoped[Parent]	= new Dictionary<string,IElement>();
					Scoped[Parent][Attr.Value]	= Current;
					break;

				case "url":
					(Current as IHasURL).URL	= Value;
					break;

				case "source":
					(Current as IHasSource).Source	= Value;
					break;

				case "target":
					(Current as IHasTarget).Target	= Value;
					break;
					
				case "symbol":
					(Current as IHasSymbol).Symbol	= Value;
					SymbalList[Value]	= Current;
					break;

				default:
					(Current as IHasAttribute).InitAtrribute(Attr);
					break;
			}
		}
		void LoadChildNode(IElement Current,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "asset":
					(Current as IHasAsset).Add(Load<asset>(Current,Child));
					break;

				case "extra":
					(Current as IHasExtra).Add(Load<extra>(Current,Child));
					break;

				case "technique_common":
					foreach(XmlAttribute Attr in Child.Attributes)
						(Current as IHasTechAttribute).InitAttribute(Attr);
					
					foreach(XmlNode Node in Child.ChildNodes)
						(Current as IHasTechChildNode).InitChildNode(this,Node);

					break;

				default:
					(Current as IHasChildNode).InitChildNode(this,Child);
					break;
			}
		}
	}
}