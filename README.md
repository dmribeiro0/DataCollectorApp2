# Inversão de Controle no padrão Observer

Inversão de Controle (IoC) significa que o controle do fluxo de execução é invertido: em vez de os observadores ficarem perguntando por dados, o sujeito decide quando e como os observadores serão chamados.

Neste projeto, a IoC acontece dentro dos métodos de notificação:

- `NotifyTempObservers()`
- `NotifyPHObservers()`

Quando `SetTemperature(...)` ou `SetPH(...)` é chamado, o `DataCollector` atualiza seu estado interno e imediatamente dispara o método de notificação correspondente. Em seguida, o sujeito percorre os observadores registrados e chama seus métodos de atualização:

- `observer.UpdateTemperature(temperature)`
- `observer.UpdatePH(pH)`

Esse é o ponto de inversão:

- Sem o padrão Observer: cada observador precisaria consultar o coletor repetidamente para verificar se houve mudança.
- Com o padrão Observer: o coletor controla o momento da notificação e envia as atualizações para os observadores.

Isso reduz o acoplamento porque os observadores dependem apenas das interfaces de observador (`ITempObserver`, `IPHObserver`) e o sujeito só precisa saber que cada observador consegue tratar uma atualização. Novos tipos de observador podem ser adicionados sem modificar a lógica principal do sujeito, mantendo o design aberto para extensão e fechado para modificação. Além disso, cabe ao observador implementar a lógica de como lidar com as atualizações.