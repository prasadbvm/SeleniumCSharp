Repository secrets to set for CI/CD deploys

Add these repository secrets (Settings -> Secrets -> Actions) before enabling deploys:

- DEV_SSH_PRIVATE_KEY: private SSH key for deploy user on dev host
- DEV_HOST: hostname or IP for dev server (e.g. dev.example.com)
- DEV_USER: SSH user for dev deploy

- QA_SSH_PRIVATE_KEY: private SSH key for deploy user on qa host
- QA_HOST: hostname or IP for qa server
- QA_USER: SSH user for qa deploy

- PROD_SSH_PRIVATE_KEY: private SSH key for deploy user on prod host
- PROD_HOST: hostname or IP for prod server
- PROD_USER: SSH user for prod deploy

Notes:
- Generate a deploy key pair on your control machine with: ssh-keygen -t ed25519 -C "deploy@github-actions" -f deploy_key
- Add the public key (deploy_key.pub) to the target server's ~/.ssh/authorized_keys for the deploy user.
- Keep private keys secret and paste them into GitHub repository secrets.
- Optionally add additional secrets for cloud providers: AZURE_CREDENTIALS, AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, GCP_SA_KEY
