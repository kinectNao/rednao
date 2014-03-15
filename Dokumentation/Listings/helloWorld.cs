using Aldebaran.Proxies;

class Program
{
  static void Main(string[] args)
  {
     TextToSpeechProxy tts = new TextToSpeechProxy("<IP OF YOUR ROBOT>", 9559);
     tts.say("Hello World");
  }
}