#!/bin/bash

ENVIRONMENT=$1

if [ "$ENVIRONMENT" == "Production" ]; then
    TAG="latest"
    BUILD_CONFIGURATION="Release"
    ASPNETCORE_ENVIRONMENT="Production"
else
    TAG="latest-t"
    BUILD_CONFIGURATION="Debug"
    ASPNETCORE_ENVIRONMENT="Stage"
fi

export ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

docker build -t authorization:$TAG \
  --build-arg BUILD_CONFIGURATION=$BUILD_CONFIGURATION \
  -f WebService.Authorization/Dockerfile .

IMAGE=authorization:$TAG  ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT docker compose up -d