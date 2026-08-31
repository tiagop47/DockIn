#!/bin/bash
set -e

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd "$SCRIPT_DIR"

echo "Executar testes e coletando dados de cobertura..."
dotnet test --collect:"XPlat Code Coverage"

echo "Gerar relatório HTML em CoverageReport/..."
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:Html

echo "Relatório gerado com sucesso!"
echo "file://$SCRIPT_DIR/CoverageReport/index.html"
echo ""

wslview CoverageReport/index.html &
