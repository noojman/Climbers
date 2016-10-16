using UnityEngine;
using System.Collections;

public static class MeshGenerator
{
   public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve _heightCurve, int levelOfDetail, bool useFlatShading, bool buildingWall)
   {
      AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);

      int width = heightMap.GetLength(0);
      int height = heightMap.GetLength(1);
      float topLeftX = (width - 1) / -2f;
      float topLeftZ = (height - 1) / 2f;

      int meshSimplificationIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;
      int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

      MeshData meshData = new MeshData(verticesPerLine, verticesPerLine, useFlatShading);
      int vertexIndex = 0;

      for (int y = 0; y < height; y += meshSimplificationIncrement)
      {
         for (int x = 0; x < width; x += meshSimplificationIncrement)
         {
            if (buildingWall)
            {
               meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, topLeftZ - y, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier);
            }
            else
            {
               meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
            }
            
            meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

            if (x < width - 1 && y < height - 1)
            {
               if (buildingWall)
               {
                  meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine, vertexIndex + verticesPerLine + 1);
                  meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex + 1, vertexIndex);
               }
               else
               {
                  meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                  meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
               }
            }

            vertexIndex++;
         }
      }

      meshData.ProcessMesh();

      return meshData;
   }
}

public class MeshData
{
   public Vector3[] vertices;
   public int[] triangles;
   public Vector2[] uvs;
   Vector3[] bakedNormals;

   int triangleIndex;

   bool useFlatShading;

   public MeshData(int meshWidth, int meshHeight, bool useFlatShading)
   {
      this.useFlatShading = useFlatShading;
      vertices = new Vector3[meshWidth * meshHeight];
      uvs = new Vector2[meshWidth * meshHeight];
      triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
   }

   public void AddTriangle(int a, int b, int c)
   {
      triangles[triangleIndex] = a;
      triangles[triangleIndex + 1] = b;
      triangles[triangleIndex + 2] = c;
      triangleIndex += 3;
   }

   public void ProcessMesh()
   {
      if (useFlatShading)
      {
         FlatShading();
      }
      else
      {
         BakeNormals();
      }
   }

   void BakeNormals()
   {
      bakedNormals = CalculateNormals();
   }

   // INCOMPLETE IMPLEMENTATION OF CUSTOM CALCULATENORMALS
   // this is because of a seam error when using an LOD greater than 1
   Vector3[] CalculateNormals()
   {
      Vector3[] vertexNormals = new Vector3[vertices.Length];
      int triangleCount = triangles.Length / 3;
      for (int i = 0; i < triangleCount; i++)
      {
         int normalTriangleIndex = i * 3;
         int vertexIndexA = triangles[normalTriangleIndex];
         int vertexIndexB = triangles[normalTriangleIndex + 1];
         int vertexIndexC = triangles[normalTriangleIndex + 2];

         Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
         vertexNormals[vertexIndexA] += triangleNormal;
         vertexNormals[vertexIndexB] += triangleNormal;
         vertexNormals[vertexIndexC] += triangleNormal;
      }

      for (int i = 0; i < vertexNormals.Length; i++)
      {
         vertexNormals[i].Normalize();
      }

      return vertexNormals;
   }

   Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
   {
      Vector3 pointA = vertices[indexA];
      Vector3 pointB = vertices[indexB];
      Vector3 pointC = vertices[indexC];

      Vector3 sideAB = pointB - pointA;
      Vector3 sideAC = pointC - pointA;
      return Vector3.Cross(sideAB, sideAC).normalized;
   }

   void FlatShading()
   {
      Vector3[] flatShadedVertices = new Vector3[triangles.Length];
      Vector2[] flatShadedUvs = new Vector2[triangles.Length];

      for (int i = 0; i < triangles.Length; i++)
      {
         flatShadedVertices[i] = vertices[triangles[i]];
         flatShadedUvs[i] = uvs[triangles[i]];
         triangles[i] = i;
      }

      vertices = flatShadedVertices;
      uvs = flatShadedUvs;
   }

   public Mesh CreateMesh()
   {
      Mesh mesh = new Mesh();
      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.uv = uvs;
      if (useFlatShading)
      {
         mesh.RecalculateNormals();
      }
      else
      {
         mesh.normals = bakedNormals;
      }
      return mesh;
   }

}