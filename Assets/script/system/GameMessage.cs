using System;
using System.Collections.Generic;

public class GameMessage {

    public MessageType type;
    public string message;

    public List<Object> parameters = new List<Object>();

    public GameMessage(MessageType mt) {
        type = mt;
    }

}
