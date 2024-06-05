# omdb-backend

## Clone from GitHub
-   Using SSH:
    ```shell
    git clone git@github.com:sergeyd108/omdb-backend.git
    ```

-   Using HTTPS:
    ```shell
    git clone https://github.com/sergeyd108/omdb-backend.git
    ```

## Running dev server
```shell
dotnet run 
```

Default URL is `http://localhost:5099/`.

## Configuration

Create an `appsettings.Development.json` with the following content:
```json
{
  "App": {
    "ApiKey": "<your OMDb API key>"
  }
}

```
