package networking.response;

import metadata.Constants;
import utility.GamePacket;

public class ResponseTest extends GameResponse {

    public ResponseTest() {
        responseCode = Constants.SMSG_TEST;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        return packet.getBytes();
    }

}
