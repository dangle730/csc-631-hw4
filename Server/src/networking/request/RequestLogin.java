package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import core.GameClient;
import core.GameServer;
import metadata.Constants;
import model.Player;
import networking.response.ResponseLogin;
import utility.DataReader;
import utility.Log;

// SQL
import java.sql.*;

/**
 * The RequestLogin class authenticates the user information to log in. Other
 * tasks as part of the login process lies here as well.
 */

public class RequestLogin extends GameRequest {

    // Data
    private String version;
    private String user_id;
    private String password;
    // Responses
    private ResponseLogin responseLogin;

    public RequestLogin() {
        responses.add(responseLogin = new ResponseLogin());
    }

    @Override
    public void parse() throws IOException {
        version = DataReader.readString(dataInput).trim();
        user_id = DataReader.readString(dataInput).trim();
        password = DataReader.readString(dataInput).trim();
    }

    @Override
    public void doBusiness() throws Exception {
        Log.printf("User '%s' is connecting...", user_id);
        Player player = null;
        // Checks if the connecting client meets the minimum version required
        if (version.compareTo(Constants.CLIENT_VERSION) >= 0) {
            if (!user_id.isEmpty()) {
                // Verification Needed
                Log.printf("User '%s' entered password '%s'", user_id, password);
                
                // SQL
                String url = "jdbc:mysql://wanderdb.c4p7z07xl4sc.us-east-1.rds.amazonaws.com:3306";
                String user = "root";
                String sqlpw = "awesomeganbold";
                String select = "SELECT * FROM wander.Users;";
                Boolean checkUser = false;

                try (Connection con = DriverManager.getConnection(url, user, sqlpw);
                    PreparedStatement sql_statement = con.prepareStatement(select);

                        ResultSet result = sql_statement.executeQuery()) {
                            while (result.next() && !checkUser) {
                                String username = result.getString("username");
                                String pw = result.getString("password");

                                if (user_id.equals(username) && password.equals(pw)) {
                                    player = new Player(100, user_id, password);
                                    System.out.println("Player created.");
                                    con.close();
                                    checkUser = true;
                                }
                                else { player = null; }
                            }
                        } catch (SQLException ex) { System.out.println("error"); }
            }
            if (player == null) {
                responseLogin.setStatus((short) 1); // User info is incorrect
                Log.printf("User '%s' has failed to log in.", user_id);
            } else {
                player.setClient(client);
                if (client.getPlayer() == null || player.getID() != client.getPlayer().getID()) {
                    GameClient thread = GameServer.getInstance().getThreadByPlayerID(player.getID());
                    // If account is already in use, remove and disconnect the client
                    if (thread != null) {
                        responseLogin.setStatus((short) 2); // Account is in use
                        thread.removePlayerData();
                        thread.newSession();
                        Log.printf("User '%s' account is already in use.", user_id);
                    } else {
                        // Continue with the login process
                        GameServer.getInstance().setActivePlayer(player);
                        player.setClient(client);
                        // Pass Player reference into thread
                        client.setPlayer(player);
                        // Set response information
                        responseLogin.setStatus((short) 0); // Login is a success
                        responseLogin.setPlayer(player);
                        Log.printf("User '%s' has successfully logged in.", player.getUsername());
                    }
                }
            }
        } else {
            responseLogin.setStatus((short) 3); // Client version not compatible
            Log.printf("User '%s' has failed to log in. (v%s)", player.getUsername(), version);
        }
    }
}
