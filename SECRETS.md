Repository secrets to set for CI/CD deploys

Add these repository secrets (Settings -> Secrets -> Actions) before enabling deploys for Azure Web Apps:

- DEV_AZURE_CREDENTIALS: JSON output of `az ad sp create-for-rbac --name "github-actions-dev" --role contributor --scopes /subscriptions/{sub}` (store entire JSON)
- DEV_WEBAPP_NAME: Name of the Azure Web App for dev (e.g. myapp-dev)

- QA_AZURE_CREDENTIALS: JSON credentials for QA service principal
- QA_WEBAPP_NAME: Name of the Azure Web App for qa

- PROD_AZURE_CREDENTIALS: JSON credentials for Prod service principal
- PROD_WEBAPP_NAME: Name of the Azure Web App for prod

Notes:
- Create a service principal and capture JSON with Azure CLI:
  az ad sp create-for-rbac --name "github-actions-<env>" --role contributor --scopes /subscriptions/{subscription-id}
  Copy the resulting JSON and add it as a repository secret named <ENV>_AZURE_CREDENTIALS (replace <ENV> with DEV/QA/PROD).
- The azure/webapps-deploy action expects the service principal to have access to the target resource.
- Optionally add additional secrets for other providers: AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, GCP_SA_KEY
