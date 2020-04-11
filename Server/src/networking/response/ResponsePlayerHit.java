package networking.response;

import metadata.Constants;
import model.Player;
import utility.GamePacket;

public class ResponsePlayerHit extends GameResponse {

    private Player player;

    public ResponsePlayerHit() { responseCode = Constants.SMSG_PLAYERHIT; }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getHealth());
        return packet.getBytes();
    }

    public void setPlayer(Player player) { this.player = player; }
}