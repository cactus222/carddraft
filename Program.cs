using System;
using System.Collections.Generic;

class Program
{
    Game model;
    static void Main(string[] args)
    {
        Program vc = new Program();
        Console.WriteLine("Hello World!");
        vc.model = new Game();
        vc.model.newDay();
        List<Card> cards = new List<Card>();

        System.ConsoleKey key = Console.ReadKey().Key;
        while (key != ConsoleKey.Escape) {
            Player player = vc.model.getCurrentPlayer(); // TODO NEEDS TO BE CURRENT PLAYER
            if (key == ConsoleKey.Spacebar) {
                vc.model.receiveVisit(player, new Forest());
            } else if (key == ConsoleKey.Q) {
                vc.model.receiveVisit(player, new Workshop());
            } else if (key == ConsoleKey.W) {
                vc.model.receiveSell(player, new List<Card>());
            } else if (key == ConsoleKey.E) {
                 vc.model.receivePurchase(player, cards);
            } else if (key == ConsoleKey.R) {
                vc.model.receiveCraftFinish(player);
            } else if (key == ConsoleKey.S) {
               vc.model.receiveActionFinished(player);
            } else if (key == ConsoleKey.D) {
                vc.model.receiveAction(player, cards[0]);
            } else if (key == ConsoleKey.G) {
                // vc.logMessage("multi input play");
                // try {
                //     string inputCards = Console.ReadLine();
                //     List<Card> cards = parseCards(inputCards);
                //     vc.model.receivePlay(player, cards);
                // } catch (Exception e) {
                //     vc.logMessage(e.ToString());
                // }
            } else if (key == ConsoleKey.Z) {
                cards.Add(vc.model.getPurchasableCards(vc.model.getCurrentPlayer())[0]);
                // vc.logMessage("multi input play");
                // try {
                //     string inputCards = Console.ReadLine();
                //     List<Card> cards = parseCards(inputCards);
                //     vc.model.receivePlay(player, cards);
                // } catch (Exception e) {
                //     vc.logMessage(e.ToString());
                // }
            } else if (key == ConsoleKey.X) {
                // cards.Add(vc.model.getPurchasableCards(vc.model.getCurrentPlayer())[0]);
                // vc.logMessage("multi input play");
                // try {
                //     string inputCards = Console.ReadLine();
                //     List<Card> cards = parseCards(inputCards);
                //     vc.model.receivePlay(player, cards);
                // } catch (Exception e) {
                //     vc.logMessage(e.ToString());
                // }
            }
            key = Console.ReadKey().Key;
        }
        // vc.logMessage("WDF");
    }
}

