using System;
using System.Collections.Generic;

public class GameMessage {

    public MessageType type;
    public string message;

    public List<object> parameters = new List<object>();

    public GameMessage(MessageType mt) {
        type = mt;
    }

}
