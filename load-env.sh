#!/bin/bash

if [ -f ./.env.example ]; then
   source ./.env.example
   export $(cut -d= -f1 ./.env.example)
else
    echo "Unable to find .env file." >&2
    echo "Create a new .env by copying .env.example file." >&2
    exit 1
fi
