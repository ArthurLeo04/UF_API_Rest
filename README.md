# UF_API_Rest

## La base de donnée
Nom : my_api_rest
MDP : password
User : me
MDP : password

Tout ca dans un powershell et penser a avoir installer Postgres et avoir ajouter le path
Importer DUMP : psql -U me -d my_api_rest -f database_structure.sql
Exporter DUMP : pg_dump -U me -d my_api_rest -s -f database_structure.sql
