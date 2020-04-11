package networking.response;

import core.GameServer;
import metadata.Constants;
import utility.GamePacket;

public class ResponsePlayers extends GameResponse {

    public ResponsePlayers() {
        responseCode = Constants.SMSG_PLAYERS;
    }

    @Override
    public byte[] constructResponseInBytes() {
        int numActivePlayers = GameServer.getInstance().getActivePlayers().size();
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(numActivePlayers);
        return packet.getBytes();
    }
}
