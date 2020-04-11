package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import core.GameClient;
import core.GameServer;
import metadata.Constants;
import model.Player;
import networking.response.ResponseRegister;
import utility.DataReader;
import utility.Log;
import java.util.ArrayList;

// SQL
import java.sql.*;

public class RequestRegister extends GameRequest {
    private String version;
    private String user_id;
    private String password;

    private ResponseRegister responseRegister;

    public RequestRegister() {
        responses.add(responseRegister = new ResponseRegister());
    }

    // public RequestRegister () { }

    @Override
    public void parse() throws IOException {
        version = DataReader.readString(dataInput).trim();
        user_id = DataReader.readString(dataInput).trim();
        password = DataReader.readString(dataInput).trim();
    }

    @Override
    public void doBusiness() throws Exception {
        
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
                        if (user_id.equals(username)) {
                            System.out.println("Username: " + user_id + " is taken.");
                            checkUser = true;
                        }
                    }
                } catch (SQLException ex) { System.out.println("ERROR"); }

                if (!checkUser) {
                    try(
                      Connection connect = DriverManager.getConnection(url, user, sqlpw);
                      PreparedStatement sql_statement = connect.prepareStatement( "insert into  wander.Users values(default,?,?)")){     
                          
                          sql_statement.setString(1, user_id);
                          sql_statement.setString(2, password);
                          sql_statement.executeUpdate();
                          connect.close();
                          
                         Log.printf("'%s' is successfully registered!", user_id);
                         responseRegister.setStatus((short) 0);
                      } catch (SQLException ex) { System.out.println("error"); }
                }
                else { responseRegister.setStatus((short) 1); }
        }
}