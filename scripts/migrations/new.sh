#!/bin/bash
#
# Create a new migration
# Usage: ./scripts/migrations/new.sh <Migration Name>
#
set -e
set -x

EF_DB_MIGRATION_NAME=${1:?"Missing argument EF_DB_MIGRATION_NAME"}
EF_COMMAND_ARGS="--context AppDbContext --startup-project=src/KingPrice.Api/KingPrice.Api.csproj --project=src/KingPrice.Core/KingPrice.Core.csproj"

dotnet ef migrations add $EF_COMMAND_ARGS $EF_DB_MIGRATION_NAME
dotnet ef database update $EF_COMMAND_ARGS
