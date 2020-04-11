package model;

// Other Imports
import core.GameClient;

/**
 * The Player class holds important information about the player including, most
 * importantly, the account. Such information includes the username, password,
 * email, and the player ID.
 */
public class Player {

    private int player_id;
    private String username;
    private String password;
    private GameClient client; // References GameClient instance
    private int x, y;
    private int health;

    public Player(int player_id) {
        this.player_id = player_id;
    }

    public Player(int player_id, String username, String password) {
        this.player_id = player_id;
        this.username = username;
        this.password = password;
        this.health = 100;
    }

    public int getID() {
        return player_id;
    }

    public int setID(int player_id) {
        return this.player_id = player_id;
    }

    public String getPassword() {
        return password;
    }

    public String setPassword(String password) {
        return this.password = password;
    }

    public String getUsername() {
        return username;
    }

    public String setUsername(String username) {
        return this.username = username;
    }

    public GameClient getClient() {
        return client;
    }

    public GameClient setClient(GameClient client) {
        return this.client = client;
    }

    public int getX() { return x; }

    public int setX(int x) { return this.x = x; }

    public int getY() { return y; }

    public int setY(int y) { return this.y = y; }

    public int getHealth() { return health; }

    public int setHealth(int health) { return this.health = health; }

    @Override
    public String toString() {
        return "Player{" +
                "player_id=" + player_id +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
    }
}