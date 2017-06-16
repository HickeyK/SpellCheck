using System.Speech.Synthesis;
using System.Threading;

namespace SpellCheck.Services
{
    class SpeachService : ISpeachService
    {
        public void Say(string textToSay, bool async)
        {
            if (async)
            {
                new Thread(() =>
                {
                    runSynth(textToSay);
                }).Start();
            }
            else
            {
                runSynth(textToSay);
            }
        }



        private void runSynth(string textToSay)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SetOutputToDefaultAudioDevice();
                synth.Speak(textToSay);
            }

        }

    }
}
