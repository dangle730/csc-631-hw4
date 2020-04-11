package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import core.GameClient;
import core.GameServer;
import metadata.Constants;
import model.Player;
import networking.response.ResponseMovement;
import utility.DataReader;
import utility.Log;

public class RequestMovement extends GameRequest {

    // Data
    private int playerX;
    private int playerY;

    // Responses
    private ResponseMovement responseMove;

    public RequestMovement() {
        responses.add(responseMove = new ResponseMovement());
    }

    @Override
    public void parse() throws IOException {
        playerX = DataReader.readInt(dataInput);
        playerY = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = GameServer.getInstance().getActivePlayer(100);
        player.setX(playerX);
        player.setY(playerY);
        responseMove.setPlayer(player);
    }
}
