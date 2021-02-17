using UnityEngine;
using System.Collections.Generic;
public class Bot : DiscordSocket
{
    List<Player> players;
    List<Enemy> enemies;
    List<string> data;
    void Start()
    {
        data = new List<string>();
        players = new List<Player>();
        enemies = new List<Enemy>();
        players.Add(GameObject.Find("Square").GetComponent<Player>());
        enemies.Add(GameObject.Find("Isometric Diamond").GetComponent<Enemy>());

        connectSocketAsClient();
        setReceiveDataThread();

        receiveEvent += delegate(string data){
            this.data.Add(data);
        }; //subscribes to the event that fires when the socket receives data
    }

    void FixedUpdate(){
        if(data.Count > 0){
            var cur = data[0];
            actions(cur);
            data.RemoveAt(0);
        }
    }

    //Takes string received from socket and uses it to determine actions
    void actions(string data){
        var msg = JsonUtility.FromJson<MessageObject>(data);
        foreach(var player in players){
            if(player.ent_name == msg.author){
                player.act(msg.command);
                break;
            }
        }
    }

}
