package dataAccessLayer;

import model.Player;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class UsersDAO {

    public UsersDAO() {
    }

    public static List<Player> getUsersListFromDB() {
        List<Player> usersList = new ArrayList<>();
        String query = "SELECT * FROM `users`";
        Connection connection = null;
        PreparedStatement pstmt = null;
        try {
            connection = DAO.getDataSource().getConnection();
            pstmt = connection.prepareStatement(query);
            ResultSet rs = pstmt.executeQuery();
            while (rs.next()) {
                int player_id = rs.getInt("player_id");
                String username = rs.getString("username");
                String password = rs.getString("password");
                short level = rs.getShort("level");
                int money = rs.getInt("money");
                Player player = new Player(player_id, username, password, level, money);
                usersList.add(player);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return usersList;
    }

    public static Player getUserFromDbIfCredentialsAreValid(String userName, String userPassword) {
        Player player = null;
        String query = "SELECT * FROM `users` WHERE `username` = '" + userName + "' AND `password` = '" + userPassword + "'";
        Connection connection = null;
        PreparedStatement pstmt = null;
        try {
            connection = DAO.getDataSource().getConnection();
            pstmt = connection.prepareStatement(query);
            ResultSet rs = pstmt.executeQuery();
            while (rs != null && rs.next()) {
                int player_id = rs.getInt("player_id");
                String username = rs.getString("username");
                String password = rs.getString("password");
                short level = rs.getShort("level");
                int money = rs.getInt("money");
                player = new Player(player_id, username, password, level, money);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return player;
    }
}