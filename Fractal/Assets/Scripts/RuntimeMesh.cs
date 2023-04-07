using UnityEngine;

namespace Ard.Procedural
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshFilter))]
	public sealed class RuntimeMesh : MonoBehaviour
	{
		[SerializeField] [HideInInspector] private Vector3[] _vertices;
		[SerializeField] [HideInInspector] private Vector3[] _normals;
		[SerializeField] [HideInInspector] private Vector2[] _uv0;
		[SerializeField] [HideInInspector] private Color[] _colors;
		[SerializeField] [HideInInspector] private int[] _triangles;

		public void Awake()
		{
			Rebuild();
		}

		public void Serialize(Mesh mesh)
		{
			_vertices = mesh.vertices;
			_normals = mesh.normals;
			_uv0 = mesh.uv;
			_colors = mesh.colors;
			_triangles = mesh.triangles;
			
			Assign(mesh);
		}

		private void Rebuild()
		{
			Assign(new Mesh
			{
				vertices = _vertices,
				normals = _normals,
				uv = _uv0,
				colors = _colors,
				triangles = _triangles
			});
		}

		private void Assign(Mesh mesh)
		{
			mesh.RecalculateBounds();
			GetComponent<MeshFilter>().sharedMesh = mesh;
		}
	}
}