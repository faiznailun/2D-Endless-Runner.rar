
private void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        // reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}