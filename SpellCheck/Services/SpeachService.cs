using System.Speech.Synthesis;

namespace SpellCheck.Services
{
    class SpeachService : ISpeachService
    {
        public void Say(string textToSay)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SetOutputToDefaultAudioDevice();
                    synth.Speak(textToSay);
                }
            }
    }
}
