#!/bin/bash
set -e

# Garantir que executa sempre a partir do diretório onde está o script
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd "$SCRIPT_DIR"

echo "🧪 Executando testes e coletando dados de cobertura..."
dotnet test --collect:"XPlat Code Coverage"

echo "📊 Gerando relatório HTML em CoverageReport/..."
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:Html

echo ""
echo "✅ Relatório gerado com sucesso!"
echo "file://$SCRIPT_DIR/CoverageReport/index.html"
echo ""

# Abrir automaticamente no browser do Windows via WSL2
if command -v wslview &> /dev/null; then
    wslview "CoverageReport/index.html" &
elif command -v powershell.exe &> /dev/null; then
    WIN_PATH=$(wslpath -w "$SCRIPT_DIR/CoverageReport/index.html")
    powershell.exe -c "Start-Process '$WIN_PATH'" &
fi
