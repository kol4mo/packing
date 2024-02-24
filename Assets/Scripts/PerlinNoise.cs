using System;
using System.Collections.Generic;
using UnityEngine;
public class PerlinNoise : MonoBehaviour {
	Dictionary<int, GameObject> tileset;
	Dictionary<int, GameObject> tileGroups;

	public GameObject prefab_concrete1;
	public GameObject prefab_concrete2;
	public GameObject prefab_concrete3;
	public GameObject prefab_concrete_cracked2;

	int mapWidth = 160;
	int mapHeight = 90;

	List<List<int>> noiseGrid = new List<List<int>>();
	List<List<GameObject>> tileGrid = new List<List<GameObject>>();

	[SerializeField][Range(4,20)] float magnification = 7.0f;

	[SerializeField] int xOffset = 0; // - -> left, + -> right
	[SerializeField] int yOffset = 0; // - -> down, + -> up

	private void Start() {
		createTileset();
		createTileGroup();
		generateMap();
	}

	private void createTileset() {
		tileset = new Dictionary<int, GameObject>();
		tileset.Add(0, prefab_concrete1);
		tileset.Add(1, prefab_concrete2);
		tileset.Add(2, prefab_concrete3);
		tileset.Add(3, prefab_concrete_cracked2);
	}

	private void createTileGroup() {
		tileGroups = new Dictionary<int, GameObject>();
		foreach (KeyValuePair<int, GameObject> prefab_pair in tileset) {
			GameObject tileGroup = new GameObject(prefab_pair.Value.name);
			tileGroup.transform.parent = gameObject.transform;
			tileGroup.transform.localPosition = new Vector3(0, 0, 0);
			tileGroups.Add(prefab_pair.Key, tileGroup);
		}
	}

	private void generateMap() {
		for (int x = 0; x < mapWidth; x++) {
			noiseGrid.Add(new List<int>());
			tileGrid.Add(new List<GameObject>());
			for (int y = 0; y < mapHeight; y++) {
				int tileId = getIdUsingPerlin(x, y);
				noiseGrid[x].Add(tileId);
				createTile(tileId, x, y);
			}
		}
	}

	private int getIdUsingPerlin(int x, int y) {
		float rawPerlin = Mathf.PerlinNoise(
			(x - xOffset) / magnification,
			(y - yOffset) / magnification);
		float clampedPerlin = Mathf.Clamp(rawPerlin, 0.0f, 1.0f);
		float scaledPerlin = clampedPerlin * tileset.Count;
		if (scaledPerlin == 4) {
			scaledPerlin = 3;
		}
		return Mathf.FloorToInt(scaledPerlin);
	}

	private void createTile(int tileId, int x, int y) {
		GameObject tilePrefab = tileset[tileId];
		GameObject tileGroup = tileGroups[tileId];
		GameObject tile = Instantiate(tilePrefab, tileGroup.transform);

		tile.name = string.Format("tile_x{0}_y{1}", x, y);
		tile.transform.localPosition = new Vector3(x, y, 0);

		tileGrid[x].Add(tile);
	}
}