using UnityEngine;
using System.Collections;

public interface IListenerObject {
    void readMessage(GameMessage message);
}
