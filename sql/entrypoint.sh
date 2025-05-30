#!/bin/bash
set -e

echo "⏳ Waiting for SQL Server to be ready..."

for i in {1..60}; do
  /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
  if [ $? -eq 0 ]; then
    echo "✅ SQL Server is ready!"
    break
  fi
  echo "⌛ Attempt $i: still waiting..."
  sleep 2
done

if [ $i -eq 60 ]; then
  echo "❌ SQL Server did not become ready in time"
  exit 1
fi

echo "📜 Running init.sql..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -i /scripts/init.sql
