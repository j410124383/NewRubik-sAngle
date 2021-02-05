

public class TimerBase 
{
    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED
    }
    private STATE state;
    public STATE State { get { return this.state; } }

    public TimerBase()
    {
        state = STATE.IDLE;
    }

    public TimerBase(float _duration) : this()
    {
        this.duration = _duration;
    }

    private float duration = 1.0f;
    private float elapsedTime = 0f;

    public float Duration { get { return this.duration; }  set { this.duration = value >= 0 ? value : 0; } }
    public float ElapsedTime { get { return this.elapsedTime; } }

    public void Tick(float deltaTime)
    {
        switch (state)
        {
            case STATE.IDLE:

                break;
            case STATE.RUN:
                elapsedTime += deltaTime;
                if (elapsedTime >= duration)
                    state = STATE.FINISHED;
                break;
            case STATE.FINISHED:

                break;
        }
    }

    public void Go()
    {
        elapsedTime = 0;
        state = STATE.RUN;
    }

    public void Stop()
    {
        elapsedTime = 0;
        state = STATE.IDLE;
    }

    public void Pause()
    {
        //elapsedTime = 0;
        state = STATE.IDLE;
    }

    public void Continue()
    {
        state = STATE.RUN;
    }

    public void End()
    {
        elapsedTime = 0;
        state = STATE.FINISHED;
    }

}
