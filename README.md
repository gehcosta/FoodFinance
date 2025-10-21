# FoodFinance - Aplicativo de Gerenciamento de Ganhos em Entregas

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-10.0-blue)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20macOS-lightgrey)

## Sobre o Projeto

O **FoodFinance** é um aplicativo mobile multiplataforma desenvolvido em **.NET MAUI** para gerenciar ganhos diários de entregadores de comida. Ele oferece controle completo sobre:

- ? Registro de ganhos diários
- ? Controle de quilometragem percorrida
- ? Timer de horas trabalhadas
- ? Cálculo automático de faturamento líquido
- ? Visualização de relatórios diários e mensais
- ? Configurações personalizáveis (tema, custos, etc.)

## Funcionalidades

### 1. Registro Diário
- Registre seus ganhos do dia
- Acompanhe a quilometragem percorrida
- Timer para controlar horas trabalhadas
- Validação de dados antes de salvar

### 2. Faturamento
- Visualize ganhos diários ou mensais
- Cálculo automático de:
  - Custo de combustível
  - Custo de manutenção
  - **Lucro líquido**
- Listagem detalhada de todas as entregas

### 3. Configurações
- Escolha o tema do aplicativo (Claro, Escuro, Sistema)
- Configure valores de:
  - Preço da gasolina (R$/L)
  - Consumo do veículo (Km/L)
  - Custo de manutenção por km (R$/km)

## Arquitetura

O projeto segue o padrão **MVVM (Model-View-ViewModel)** com a seguinte estrutura:

```
FoodFinance/
??? Models/           # Modelos de dados
?   ??? DayEntry.cs
?   ??? Settings.cs
?   ??? RevenueSummary.cs
??? ViewModels/          # Lógica de apresentação
?   ??? DayEntryViewModel.cs
?   ??? SettingsViewModel.cs
?   ??? RevenueViewModel.cs
??? Views/     # Interfaces do usuário (XAML)
?   ??? DayEntryPage.xaml
?   ??? SettingsPage.xaml
? ??? RevenuePage.xaml
??? Services/    # Serviços de negócio
?   ??? LocalStorageService.cs
??? Helpers/   # Classes auxiliares
?   ??? RevenueCalculator.cs
??? Converters/          # Conversores de dados
?   ??? TimerTextConverter.cs
?   ??? ValueConverters.cs
??? Tests/       # Testes
    ??? RevenueCalculatorTests.cs
```

## Tecnologias Utilizadas

- **.NET 10** (Preview)
- **.NET MAUI** - Framework multiplataforma
- **CommunityToolkit.Mvvm** - Biblioteca MVVM
- **System.Text.Json** - Serialização de dados
- **XAML** - Interface do usuário

## Persistência de Dados

Os dados são armazenados localmente no dispositivo usando:
- **JSON** para serialização
- **FileSystem.AppDataDirectory** para armazenamento

Arquivos salvos:
- `data.json` - Registros de entregas
- `settings.json` - Configurações do usuário

## Cálculo de Faturamento Líquido

### Fórmula

```csharp
Custo de Combustível = (Km Percorridos ÷ Km por Litro) × Preço da Gasolina
Custo de Manutenção = Km Percorridos × Custo por Km
Lucro Líquido = Ganho Total - Custo de Combustível - Custo de Manutenção
```

### Exemplo Prático

**Entrada:**
- Ganho do dia: R$ 51,42
- Km percorridos: 27,5 km
- Preço da gasolina: R$ 6,50/L
- Consumo: 30 km/L
- Manutenção: R$ 0,20/km

**Cálculo:**
```
Custo Combustível = (27,5 ÷ 30) × 6,50 = R$ 5,96
Custo Manutenção = 27,5 × 0,20 = R$ 5,50
Lucro Líquido = 51,42 - 5,96 - 5,50 = R$ 39,96
```

## Como Executar

### Pré-requisitos

- Visual Studio 2022 (17.13 ou superior) com workload .NET MAUI
- .NET 10 SDK (Preview)
- Para Android: SDK do Android 21.0 ou superior
- Para iOS: macOS com Xcode 15+

### Passos

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/foodfinance.git
cd foodfinance
```

2. Restaure os pacotes:
```bash
dotnet restore
```

3. Execute o projeto:
```bash
# Android
dotnet build -t:Run -f net10.0-android

# iOS (apenas em macOS)
dotnet build -t:Run -f net10.0-ios

# Windows
dotnet build -t:Run -f net10.0-windows10.0.19041.0
```

Ou simplesmente abra a solução no Visual Studio e pressione F5.

## Testes

O projeto inclui testes automatizados que são executados durante o modo Debug.

Para ver os resultados dos testes:
- Execute o app em modo Debug
- Verifique a janela de Output do Visual Studio
- Os testes validam:
  - ? Cálculo de faturamento líquido
  - ? Estrutura de dados dos modelos
  - ? Casos extremos (valores zero, altos)

## Interface do Usuário

### Design

- **Paleta de cores:** Azul Material Design (#2196F3)
- **Estilo:** Minimalista e intuitivo
- **Componentes:** Centralizados e responsivos
- **Temas:** Suporte para modo claro, escuro e sistema

### Navegação

O app usa **Shell Navigation** com TabBar:
- **Registro** - Adicionar novo dia de trabalho
- **Faturamento** - Visualizar relatórios
- **Configurações** - Ajustar preferências

## Próximas Melhorias

- [ ] Notificações push para lembretes
- [ ] Gráficos interativos (usando LiveCharts ou Syncfusion)
- [ ] Exportação de dados para CSV/PDF
- [ ] Backup em nuvem (Azure, Firebase)
- [ ] Filtros customizados por período
- [ ] Modo offline completo
- [ ] Suporte a múltiplos veículos
- [ ] Comparativo de desempenho mensal

## Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/NovaFuncionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/NovaFuncionalidade`)
5. Abra um Pull Request

## Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## Autor

Desenvolvido como projeto de estudo de .NET MAUI e gerenciamento de finanças pessoais.

## Suporte

Para reportar bugs ou sugerir melhorias:
- Abra uma **Issue** no GitHub
- Entre em contato pelo email: suporte@foodfinance.app

---

**FoodFinance** - Gerencie seus ganhos com inteligência! ????
