package networking.response;

// Other Imports
import metadata.Constants;

// import model.Player;
import utility.GamePacket;
import utility.Log;

public class ResponseRegister extends GameResponse {

    private short status;

    public ResponseRegister() {
        responseCode = Constants.SMSG_REGISTER;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addShort16(status);
        Log.printf("Register Server Status: " + status);
        return packet.getBytes();
    }

    public void setStatus(short status) {
        this.status = status;
    }
}