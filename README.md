# Caapora RPG
### Note: Cyanogen users
O audio pode não funcionar corretamente em Androids com CyanogenMod por conta de um bug em um módulo que habilita automaticamente a funcionalidade Audio Fast Path

para corrigir:

1. (optional) adb shell pm list features shows `feature:android.hardware.audio.low_latency`
1. Rename/remove `/system/etc/permissions/android.hardware.audio.low_latency.xml`
1. reboot

mas informações em: http://forum.unity3d.com/threads/android-sound-problem.359341/page-2

## Funcionalidades 


1. Telas  

1.1 Menu principal 
1.1 Start Game 
1.1 Load 
1.1 Opções 
1.1 Música de Fundo 


1. Introdução 


1.1 Exibir História 


1.1 Start Game 


1.1 Música de Fundo 


 


1. Loading 


1.1 Carregar Jogo  


 

 1.  Introdução  


   1.1 Exibir Informações da Fase 


   Esconder controles, balões de avisos, telas de conversa e balões de conversas 


   Iniciar conversação automática 


   Botão para pular 


   Iniciar deslocamento automático 


   Executar música de fundo 


    


 1.   Controle do Personagem 


    Controle por Teclado 


    Setas 


    Botões A/B 


    Botão Start 


     


     Controle por Touch  


     Mesmo que o de cima 


      


      Controle por IA 


      Implantar 


       


   1. Eventos de Jogabilidade 


       Monkey  


       Seguir o Jogador 


       Colisão com Fogo Caapora 


       Exibir no Painel 


       Executar animação 


       Piscar em vermelho 


       Reduzir life 


       Colisão com Fogo Monkey 


       Idém 


       Pegar Balde 


       Pegar com o botão A 


       Adicionar a Classe Inventory 


       Adicionar ao Painel 


       Mudar Animação 


       Coletar água no Rio 


       Encher balde com água 


       Aumentar nível de água no Painel 


       Executar animação 


       Jogar Agua 


       Jogar com o botão B 


       Checar se há água 


       Reduzir nível de água no Painel 


       Só jogar quando houver água 


       Executar animação 


       Apagar chama ao colidir com ela 


       Chamas 


       Aumentar automaticamente com o passar do tempo 


        

	  1. Estados do Game 


	  Gameover 


	  Caipora sem life 


	  Monkey sem life 


	  Winner 


	  Fogo Apagado 


	  Animais Recuperados 


	  Try Again 


	  Retornar ao início da Fase


