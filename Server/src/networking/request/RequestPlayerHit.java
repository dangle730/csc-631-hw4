package networking.request;

import core.GameServer;
import model.Player;
import networking.response.ResponsePlayerHit;
import utility.DataReader;
import utility.Log;

import java.io.IOException;

public class RequestPlayerHit extends GameRequest {

    private int health;

    private ResponsePlayerHit responsePlayerHit;

    public RequestPlayerHit() { responses.add(responsePlayerHit = new ResponsePlayerHit()); }

    @Override
    public void parse() throws IOException {
        health = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = GameServer.getInstance().getActivePlayer(100);
        player.setHealth(health);
        responsePlayerHit.setPlayer(player);
        Log.printf("Player health: " + player.getHealth());
    }

}