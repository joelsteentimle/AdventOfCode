using Open.Collections;

namespace AoC2023;

public class Day20
{
    public Day20(List<string> lines)
    {
        foreach (var line in lines)
        {
            var ids = line.SplitAndTrim(' ', '-', '>', ',', '%', '&');

            var name = ids[0];
            var targets = ids.ToList()[1..];

            if (line[0] == '%')
                PulseMods[name] = new FlipMod(name, targets);

            if(line[0]== '&')
                PulseMods[name] = new ConMod(name, targets);

            if (line[0] == 'b')
                StartPoint = new Broadcaster(name, targets);
        }

        UpdateInputs(StartPoint.Name,StartPoint.Targets);

        foreach (var mod in PulseMods.Values)
            UpdateInputs(mod.Name,mod.Targets);


        void UpdateInputs(string source, List<string> targets)
        {
            foreach (var target in targets)
                if (PulseMods.TryGetValue(target, out var targetMod)
                    && targetMod is ConMod cm)
                    cm.AddInput(source);
        }
    }

    public long SentPulses()
    {
        long lowPulses=0;
        long highPulses=0;
        for (var i = 0; i < 1000; i++)
        {
            var pulses = StartPoint.Start();
             lowPulses++;
             while (pulses.Count > 0)
             {
                 var current = pulses.Dequeue();

                 if (current.Level == PulseLevel.High)
                     highPulses++;
                 else
                     lowPulses++;

                 if (PulseMods.TryGetValue(current.Target, out var mod))
                 {
                     var newPulses = mod.ReceivePulse(current);
                     foreach (var p in newPulses)
                         pulses.Enqueue(p);
                 }
             }
        }

        return highPulses * lowPulses;
    }

    private Broadcaster StartPoint { get; }= new ("-",[]);
    private Dictionary<string, PulseMod> PulseMods { get; } =[];


    private sealed class Broadcaster(string name, List<string> targets)
    {
        public string Name { get; } = name;
        public List<string> Targets { get; } = targets;

        public Queue<Pulse> Start() =>
            Targets.Select(t => new Pulse(Name, t, PulseLevel.Low)).ToQueue();
    }

    private abstract class PulseMod(string name, List<string> targets)
    {
        public  string Name { get; } = name;
        public List<string> Targets { get; } = targets;

        public abstract Queue<Pulse> ReceivePulse(Pulse input);
    }

    private sealed class FlipMod(string name, List<string> targets) : PulseMod(name, targets)
    {
        private bool modIsOn;

        public override Queue<Pulse> ReceivePulse(Pulse input)
        {
            if (input.Level == PulseLevel.Low)
            {
                modIsOn = !modIsOn;

                return Targets.Select(t =>
                        new Pulse(Name, t, modIsOn ? PulseLevel.High : PulseLevel.Low))
                    .ToQueue();
            }
            return [];
        }
    }

    private sealed class ConMod(string nameArg, List<string> targets) : PulseMod(nameArg, targets)
    {
        private readonly Dictionary<string, PulseLevel> inputs = [];

        public void AddInput(string from) => inputs[from] = PulseLevel.Low;

        public override Queue<Pulse> ReceivePulse(Pulse input)
        {
            inputs[input.Sender] = input.Level;

            var outPulse
                = inputs.Values.Any(p => p == PulseLevel.Low)
                    ? PulseLevel.High
                    : PulseLevel.Low;

            return Targets.Select(t => new Pulse(Name, t, outPulse)).ToQueue();
        }
    }

    private sealed record Pulse(string Sender, string Target, PulseLevel Level);

    private enum PulseLevel
    {
        Low = 1,
        High = 2
    }
}
