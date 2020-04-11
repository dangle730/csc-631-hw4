package networking.request;

import networking.response.ResponsePlayers;
import utility.Log;

import java.io.IOException;

public class RequestPlayers extends GameRequest {

    private ResponsePlayers responsePlayers;

    //Do not remove this constructor even though the IDE claims it is not being used anywhere. It is being called by reflection.
    public RequestPlayers() {
        responses.add(responsePlayers = new ResponsePlayers());
    }

    @Override
    public void parse() throws IOException {
    }

    @Override
    public void doBusiness() throws Exception {
        Log.printf("Number of players request received !!!");
    }
}
