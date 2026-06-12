Repository secrets to set for CI/CD deploys

Add these repository secrets (Settings -> Secrets -> Actions) before enabling deploys for Azure Web Apps:

- DEV_AZURE_CREDENTIALS: JSON output of `az ad sp create-for-rbac --name "github-actions-dev" --role contributor --scopes /subscriptions/{sub}` (store entire JSON)
- DEV_WEBAPP_NAME: Name of the Azure Web App for dev (e.g. myapp-dev)

- DEV_SMOKE_URL: Optional full URL to hit for smoke tests (overrides derived azurewebsites.net URL)

- QA_AZURE_CREDENTIALS: JSON credentials for QA service principal
- QA_WEBAPP_NAME: Name of the Azure Web App for qa

- QA_SMOKE_URL: Optional full URL to hit for QA smoke tests

- PROD_AZURE_CREDENTIALS: JSON credentials for Prod service principal
- PROD_WEBAPP_NAME: Name of the Azure Web App for prod

- PROD_SMOKE_URL: Optional full URL to hit for Prod smoke tests

Notes:
- Create a service principal and capture JSON with Azure CLI:
  az ad sp create-for-rbac --name "github-actions-<env>" --role contributor --scopes /subscriptions/{subscription-id}
  Copy the resulting JSON and add it as a repository secret named <ENV>_AZURE_CREDENTIALS (replace <ENV> with DEV/QA/PROD).
- The azure/webapps-deploy action expects the service principal to have access to the target resource.
- Optionally add additional secrets for other providers: AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, GCP_SA_KEY

Docker registry (optional)
--------------------------
If you want GitHub Actions to push built images to a container registry, add the appropriate secrets:

- DOCKERHUB_USERNAME and DOCKERHUB_TOKEN (or DOCKERHUB_PASSWORD)
- OR use an Azure Container Registry with AZURE_ACR_LOGIN_SERVER and a service principal in AZURE_ACR_PASSWORD / AZURE_ACR_USERNAME

In the workflow we currently export the built image as an artifact (image.tar). To push to a registry, add a step that logs in and pushes the image after building.

Sample Azure CLI commands
-------------------------
Use these commands to provision a Resource Group, App Service plan and Web App, and to create a service principal (SDK auth JSON) suitable for the azure/login action.

1) Login and set subscription

   az login
   az account set --subscription "<SUBSCRIPTION_ID>"

2) Create resource group

   az group create --name <RESOURCE_GROUP> --location eastus

3) Create an App Service plan (Linux example)

   az appservice plan create --name <APP_SERVICE_PLAN> --resource-group <RESOURCE_GROUP> --is-linux --sku B1

4) Create a Web App (adjust runtime as needed for your target .NET version)

   az webapp create --resource-group <RESOURCE_GROUP> --plan <APP_SERVICE_PLAN> --name <WEBAPP_NAME> --runtime "DOTNET|8.0"

   If you prefer container-based deployment, use --deployment-container-image-name instead of --runtime.

5) Create a service principal and output SDK auth JSON (copy this JSON into the repository secret, e.g. DEV_AZURE_CREDENTIALS)

   az ad sp create-for-rbac --name "github-actions-<ENV>" --role contributor --scopes /subscriptions/<SUBSCRIPTION_ID> --sdk-auth

   The above command prints a JSON blob; paste the entire JSON value into the corresponding <ENV>_AZURE_CREDENTIALS secret in GitHub.

6) (Optional) From your local machine you can deploy a published package with the Azure CLI

   az webapp deploy --resource-group <RESOURCE_GROUP> --name <WEBAPP_NAME> --src-path ./publish_output --type zip

Replace the angle-bracket placeholders with your values. Keep the service principal JSON secret and limited to the minimum required role for deployment.
