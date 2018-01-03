using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLogger {

    private static CSVLogger mSelfReference;
    private static StreamWriter file;

    private static void verify_initialization() {
        if (mSelfReference == null) {
            File.WriteAllText(@"mage_logs.csv", string.Empty);
            file = new StreamWriter(@"mage_logs.csv");
            string output = "real time" + "," + "time" + "," + "Person" + "," + "Event Type" + "," + "Message";
            file.WriteLine(output);
            file.Flush();
            mSelfReference = new CSVLogger();
        }
    }

    private static void trPostMessage(float time, string person, string channel, string message) {
        string output = Time.fixedTime + "," + time.ToString() + "," + person + "," + channel + "," + message;
        file.WriteLine(output);
        file.Flush();
    }

    public static void log(float time, string person, string channel, string message) {
        verify_initialization();
        trPostMessage(time, person, channel, message);
    }
}