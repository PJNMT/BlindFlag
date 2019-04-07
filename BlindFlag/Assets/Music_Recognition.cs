using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Recognition : MonoBehaviour
{
    public  float RmsValue;
    public  float DbValue;
    public  float PitchValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    static float[] _samples;
    private  float[] _spectrum;
    private  float _fSample;

    private  Dictionary<string, float> music_reference = new Dictionary<string, float>();
    private  Dictionary<string, float> base_reference = new Dictionary<string, float>();

     string note;
     float high;
     int pow;

    float user;
     bool b;

     float music_note;

    void Start()
    {
        start_recognition();
    }

    // A modifier en fonction de ce qu'on veut faire
    void Update()
    {
        user = AnalyzeSound();
        b = Is_right(user, "Do_3", 0.5f);

        if (b)
        {
            transform.Translate(Random.Range(-50, 50f), 0, Random.Range(-50f, 50f));
        }
    }


    void start_recognition()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;

        base_reference.Add("Do_", 32.70f); base_reference.Add("Do#_", 34.65f); base_reference.Add("Re_", 36.71f); base_reference.Add("Re#_", 38.89f);
        base_reference.Add("Mi_", 41.20f); base_reference.Add("Fa_", 43.65f); base_reference.Add("Fa#_", 46.25f); base_reference.Add("Sol_", 49.00f);
        base_reference.Add("Sol#_", 51.91f); base_reference.Add("La_", 55.00f); base_reference.Add("La#_", 58.27f); base_reference.Add("Si_", 61.74f);

        foreach (var reference in base_reference)       // Créer la table de correspondance entre les notes et la fréquence en Hertz
        {
            note = reference.Key;
            high = reference.Value;
            pow = 1;

            for (int i = 0; i < 8; i++)
            {
                music_reference.Add(note + i, high * pow);
                pow *= 2;
            }
        }

    }

    bool Is_right(float note_user, string note, float limit)
    {
        return music_reference[note] + limit >= note_user && note_user >= music_reference[note] - limit;
    }

    float AnalyzeSound()
    {
        GetComponent<AudioSource>().GetOutputData(_samples, 0);
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i];
        }
        RmsValue = Mathf.Sqrt(sum / QSamples);
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue);
        if (DbValue < -160) DbValue = -160;

        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        {
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i;
        }
        float freqN = maxN;
        if (maxN > 0 && maxN < QSamples - 1)
        {
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }

        PitchValue = freqN * (_fSample / 2) / QSamples;

        return PitchValue;
    }
}
