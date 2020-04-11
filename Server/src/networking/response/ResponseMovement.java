package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;

public class ResponseMovement extends GameResponse {
    private Player p;

    public ResponseMovement() { responseCode = Constants.SMSG_MOVEMENT; }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(p.getX());
        packet.addInt32(p.getY());

        return packet.getBytes();
    }

    public void setPlayer(Player player) { this.p = player; }
}