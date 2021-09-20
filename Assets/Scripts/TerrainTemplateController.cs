


private const float debugLineHeight = 10.0f;

private void OnDrawGizmos()
{
    Debug.DrawLine(transform.position + Vector3.up * debugLineHeight / 2, transform.position + Vector3.down * debugLineHeight / 2, Color.green);
}

[Header("Force Early Template")]
public List<TerrainTemplateController> earlyTerrainTemplates;


private void Start()
{
    spawnedTerrain = new List<GameObject>();

    lastGeneratedPositionX = GetHorizontalPositionStart();

    foreach (TerrainTemplateController terrain in earlyTerrainTemplates)
    {
        GenerateTerrain(lastGeneratedPositionX, terrain);
        lastGeneratedPositionX += terrainTemplateWidth;
    }

    while (lastGeneratedPositionX < GetHorizontalPositionEnd())
    {
        GenerateTerrain(lastGeneratedPositionX);
        lastGeneratedPositionX += terrainTemplateWidth;
    }
}


private float lastRemovedPositionX;

private void Start()
{
    spawnedTerrain = new List<GameObject>();

    lastGeneratedPositionX = GetHorizontalPositionStart();
    lastRemovedPositionX = lastGeneratedPositionX - terrainTemplateWidth;


    foreach (TerrainTemplateController terrain in earlyTerrainTemplates)
    {
        GenerateTerrain(lastGeneratedPositionX, terrain);
        lastGeneratedPositionX += terrainTemplateWidth;
    }


    while (lastGeneratedPositionX < GetHorizontalPositionEnd())
    {
        GenerateTerrain(lastGeneratedPositionX);
        lastGeneratedPositionX += terrainTemplateWidth;
    }
}

private void Update()
{
    while (lastGeneratedPositionX < GetHorizontalPositionEnd())
    {
        GenerateTerrain(lastGeneratedPositionX);
        lastGeneratedPositionX += terrainTemplateWidth;
    }

    while (lastRemovedPositionX + terrainTemplateWidth < GetHorizontalPositionStart())
    {
        lastRemovedPositionX += terrainTemplateWidth;
        RemoveTerrain(lastRemovedPositionX);
    }
}

private void RemoveTerrain(float posX)
{
    GameObject terrainToRemove = null;

    // find terrain at posX
    foreach (GameObject item in spawnedTerrain)
    {
        if (item.transform.position.x == posX)
        {
            terrainToRemove = item;
            break;
        }
    }



    // after found;
    if (terrainToRemove != null)
    {
        spawnedTerrain.Remove(terrainToRemove);
        Destroy(terrainToRemove);
    }
}

// pool list
private Dictionary<string, List<GameObject>> pool;

private void Start()
{
    // init pool
    pool = new Dictionary<string, List<GameObject>>();

    spawnedTerrain = new List<GameObject>();

    lastGeneratedPositionX = GetHorizontalPositionStart();
    lastRemovedPositionX = lastGeneratedPositionX - terrainTemplateWidth;


    foreach (TerrainTemplateController terrain in earlyTerrainTemplates)
    {
        GenerateTerrain(lastGeneratedPositionX, terrain);
        lastGeneratedPositionX += terrainTemplateWidth;
    }


    while (lastGeneratedPositionX < GetHorizontalPositionEnd())
    {
        GenerateTerrain(lastGeneratedPositionX);
        lastGeneratedPositionX += terrainTemplateWidth;
    }
}

// pool function
private GameObject GenerateFromPool(GameObject item, Transform parent)
{
    if (pool.ContainsKey(item.name))
    {
        // if item available in pool
        if (pool[item.name].Count > 0)
        {
            GameObject newItemFromPool = pool[item.name][0];
            pool[item.name].Remove(newItemFromPool);
            newItemFromPool.SetActive(true);
            return newItemFromPool;
        }
    }
    else
    {
        // if item list not defined, create new one
        pool.Add(item.name, new List<GameObject>());
    }


    // create new one if no item available in pool
    GameObject newItem = Instantiate(item, parent);
    newItem.name = item.name;
    return newItem;
}

private void ReturnToPool(GameObject item)
{
    if (!pool.ContainsKey(item.name))
    {
        Debug.LogError("INVALID POOL ITEM!!");
    }


    pool[item.name].Add(item);
    item.SetActive(false);
}