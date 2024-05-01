# UF_API_Rest

## Lancer l'application

- Naviguer dans le dossier où se trouve le docker-compose.yml
- S'assurer que DockerDeamon est actif sur la machine
- Lancer la commande suivante : 
```shell
docker-compose up
```
- La commande va lire le docker-compose.yml, télécharger les images necéssaires (si pas déjà présentes) puis lancer les 3 dockers souhaités à savoir : PostgresSQL, Redis et l'API Web.

## API Web

Endpoint : localhost:5214

## DOC DEVELOPPEUR 

### La base de donnée

- Nom : my_api_rest
- MDP : password
- User : me
- MDP : password

### Premiers pas

Pour pouvoir executer les commandes d'import-export, il est néscessaire d'avoir un compte "me" qui possède les droits appropriés.
Il est donc néscessaire de suivre les étapes suivantes :
- télécharger postgres, le lancer, donner un mot de passe pour l'admin
- ajouter le fichier d'executable postgres au PATH windows (le chemin c'est souvent ProgammesFiles/Postgres/16/bin)
- dans un terminal lancer la commande "psql -U postgres", vous aurez besoin de votre mdp donné lors de la première étape
- dans le terminal postgres executez les commandes suivantes :
```sql
CREATE USER me WITH PASSWORD 'password';
CREATE DATABASE my_api_rest;
GRANT ALL PRIVILEGES ON DATABASE my_api_rest TO me;
GRANT ALL PRIVILEGES ON SCHEMA public TO me;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO me;
```

Vous devriez maintenant être en capacité d'executez depuis un terminal (pas dans postgres) les commandes suivantes :
```shell
# Importer
psql -U postgres -d my_api_rest -f database_structure.sql
# Exporter
pg_dump -U me -d my_api_rest -s -f database_structure.sql
```

> [!NOTE]
> Il est possible de faire ces commandes directement dans pgAdmin. Pour ce faire une fois le logiciel ouvert, vous pouvez faire clic droit dans l'architecture à gauche et sur l'onglet *Query Tool*. Vous pouvez maintenant rentrer et lancer des commandes sans passer par le shell.

Pour clear tout la base de donnée, vous pouvez utiliser la commande suivante :
```
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
```
N'oubliez juste pas de redonner les droits sur toutes les tables à l'utilisateur administrateur ensuite.

