#!/bin/bash

docker run \
  --name firestore-emulator \
  --env "FIRESTORE_PROJECT_ID=dummy-project-id" \
  --env "PORT=8080" \
  --publish 8080:8080 \
  mtlynch/firestore-emulator-docker

# You can emulate database
