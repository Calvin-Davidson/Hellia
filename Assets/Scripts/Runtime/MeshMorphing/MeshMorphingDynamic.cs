using UnityEngine;
using System;

public class MeshMorphingDynamic : MonoBehaviour
{
	[Serializable]
	struct MorphTarget
	{
		public Mesh MorphMesh;
		[NonSerialized]
		public Vector3[] DifVertices;

		[Range(-1, 1f)]
		public float Weight;

		internal void Initialize(Vector3[] m_VertsBase)
		{
			//We calculate and store only the difference for each vertex from the base mesh.
			DifVertices = new Vector3[m_VertsBase.Length];
			var meshVerts = MorphMesh.vertices;
			for (int i = 0; i < m_VertsBase.Length; i++)
			{
				DifVertices[i] = meshVerts[i] - m_VertsBase[i];
			}
		}
	}

	float m_LastMorph;

	//Make sure both meshes use the same vertex count and indices! Modify a mesh in a 3D editing tool and manipulate only the position of the vertecies is the best way to do that.
	//Keep in mind that changing values like normals or uvs can affect the vertex count of a mesh and your morph target might not match anymore! This is why this example only reads vertex position.
	public Mesh m_MeshBase;
	[SerializeField]
	MorphTarget[] m_MorphTargets;
	Mesh m_MorphedMesh;
	float[] m_LastWeights;
	public bool m_GenerateNormals;
	[SerializeField] private float[] startWeights;
	[SerializeField] private float[] midWeights;
	[SerializeField] private float[] endWeights;
	[SerializeField] private float lerpSpeed = 1f;
	[SerializeField] private float maxDifference = 0.1f;
	float t;
	float t2;
	bool doneUpdating = false;
	bool isPaused = false;
	bool isUpdating = false;
	float[] currentWeights;
	//You could add more than just one morp target! Just add the meshes and additional parameters and notice the example below.

	Material m_Mat;

	Vector3[] m_VertsBase;
	Vector3[] m_VertsCurrent;

	void Start()
	{
		t = 0;
		currentWeights = new float[m_MorphTargets.Length];
		m_MorphedMesh = new Mesh();
		//This should improve performance a bit.
		m_MorphedMesh.MarkDynamic();

		m_MorphedMesh.name = "Morphed Mesh";
		gameObject.GetComponent<MeshFilter>().mesh = m_MorphedMesh;
		m_Mat = gameObject.GetComponent<MeshRenderer>().material;
		ResetMesh();
	}

	void ResetMesh()
	{
		m_LastWeights = new float[m_MorphTargets.Length];

		m_MorphedMesh.vertices = m_MeshBase.vertices;
		m_MorphedMesh.triangles = m_MeshBase.triangles;
		m_MorphedMesh.uv = m_MeshBase.uv;
		m_MorphedMesh.uv2 = m_MeshBase.uv2;
		m_MorphedMesh.normals = m_MeshBase.normals;
		m_MorphedMesh.tangents = m_MeshBase.tangents;
		m_MorphedMesh.bounds = m_MeshBase.bounds;
		//Add more properties if you need them

		m_VertsBase = m_MeshBase.vertices;
		for (int i = 0; i < m_MorphTargets.Length; i++)
		{
			m_MorphTargets[i].Initialize(m_VertsBase);
		}

		m_VertsCurrent = new Vector3[m_VertsBase.Length];
	}

	void Update()
	{
		if (isUpdating)
		{
			DynamicUpdate();
		}
	}
	public void StartUpdating()
	{
		isUpdating = true;
	}
	public void PauseUpdating()
	{
		isPaused = true;
	}
	public void UnPauseUpdating()
	{
		isPaused = false;
	}
	void DynamicUpdate()
	{
		if (doneUpdating)
		{
			return;
		}
		if (isPaused)
		{
			return;
		}
		t2 = t * 2;
		bool update = false;
		for (int i = 0; i < startWeights.Length; i++)
		{
			if(t < 0.5f)
			{
				currentWeights[i] = Mathf.Lerp(startWeights[i], midWeights[i], t2);
			}
			if(t >= 0.5f)
			{
				currentWeights[i] = Mathf.Lerp(midWeights[i], endWeights[i], (t2-1));
			}
			if(Mathf.Abs(m_LastWeights[i] - currentWeights[i]) > maxDifference)
			{
				m_LastWeights[i] = currentWeights[i];
				m_MorphTargets[i].Weight = currentWeights[i];
				update = true;
			}
			if (t >= 1)
			{
				m_LastWeights[i] = endWeights[i];
				m_MorphTargets[i].Weight = endWeights[i];
				doneUpdating = true;
			}
		}
		if (!isPaused)
			t += lerpSpeed * Time.deltaTime;
		if (update)
			UpdateMorph();
	}
	void NormalUpdate()
	{
		bool update = false;
		for (int i = 0; i < m_MorphTargets.Length; i++)
		{
			if (m_LastWeights[i] != m_MorphTargets[i].Weight)
			{
				update = true;
			}
			m_LastWeights[i] = m_MorphTargets[i].Weight;
		}

		if (update)
			UpdateMorph();
	}

	void UpdateMorph()
	{
		if (m_GenerateNormals)
			ResetMesh();

		for (int i = 0; i < m_VertsCurrent.Length; i++)
		{
			m_VertsCurrent[i] = m_VertsBase[i];
		}
		for (int d = 0; d < m_MorphTargets.Length; d++)
		{
			var target = m_MorphTargets[d];

			//The vertex cound should be the same, otherwise the meshes don't match.
			if (target.DifVertices.Length != m_VertsBase.Length)
				continue;
			for (int i = 0; i < m_VertsCurrent.Length; i++)
			{
				m_VertsCurrent[i] += target.DifVertices[i] * target.Weight;
			}
		}
		m_MorphedMesh.vertices = m_VertsCurrent;
		//You might want to update bounds or change other values as well such as vertex colors.
		//You can recalculate normals for instance, but you should keep in mind that this can change the vertex count! So in this case you have to reset the mesh before you call UpdateMorph() a second time.

		m_MorphedMesh.RecalculateBounds();

		if (m_GenerateNormals)
			m_MorphedMesh.RecalculateNormals();
	}
}