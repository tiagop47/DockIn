#!/bin/bash
set -e

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd "$SCRIPT_DIR"

echo "Limpando resultados anteriores..."
rm -rf TestResults CoverageReport

echo "A executar testes em todos os projetos de Tests/..."
for proj in $(find . -name "*.Tests.csproj"); do
    echo "▶ A testar: $proj"
    dotnet test "$proj" --collect:"XPlat Code Coverage" --results-directory "$SCRIPT_DIR/TestResults"
done

echo "A gerar relatório HTML combinado em CoverageReport/..."
reportgenerator -reports:"$SCRIPT_DIR/TestResults/**/coverage.cobertura.xml" -targetdir:"$SCRIPT_DIR/CoverageReport" -reporttypes:Html

echo "Relatório gerado com sucesso!"
echo "file://$SCRIPT_DIR/CoverageReport/index.html"
echo ""

if command -v wslview &> /dev/null; then
    wslview CoverageReport/index.html &
fi
