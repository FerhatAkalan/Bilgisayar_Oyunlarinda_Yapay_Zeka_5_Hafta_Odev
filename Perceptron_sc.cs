using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TrainingSet
{
    public double[] input;  // Giriş değerlerini tutar.
    public double output;   // Beklenen çıkışı tutar.
}
public class Perceptron_sc : MonoBehaviour
{
    public TrainingSet[] ts; // Eğitim veri setlerini tutan dizi
	double[] weights = {0,0}; // Perceptron'un ağırlıkları
	double bias = 0; // Bias değeri
	double totalError = 0; // Toplam hata miktarı
	// Ağırlıklı toplam hesaplama fonksiyonu
    double DotProductBias(double[] weightArray, double[] inputArray) 
	{
		if (inputArray == null || weightArray == null)
			return -1;
		if (inputArray.Length != weightArray.Length)
			return -1;
		double d = 0;
		for (int x = 0; x < inputArray.Length; x++)
		{
			d += inputArray[x] * weightArray[x];
		}
		d += bias;
		return d;
	}
	// Çıktı hesaplama
    double CalcOutput(int i)
	{
		double db = DotProductBias(weights, ts[i].input);
		if (db > 0) return(1);
		return(0);
	}
	// Çıktı hesaplama
	double CalcOutput(double i1, double i2)
	{
		double[] inp = new double[] {i1, i2};
		double dp = DotProductBias(weights, inp);
		if(dp > 0) return(1);
		return(0);
	}
	// Ağırlıkları rastgele başlangıç değerleriyle başlat
    void InitialiseWeights()
	{
		for(int i = 0; i < weights.Length; i++)
		{
			weights[i] = Random.Range(-1.0f,1.0f);
		}
		bias = Random.Range(-1.0f,1.0f);
	}
	// Ağırlıkları güncelleme fonksiyonu
	void UpdateWeights(int j)
	{
		double error = ts[j].output - CalcOutput(j);
		totalError += Mathf.Abs((float)error);
		for(int i = 0; i < weights.Length; i++)
		{			
			weights[i] = weights[i] + error*ts[j].input[i]; 
		}
		bias += error;
	}
	// Perceptron'u eğitme fonksiyonu
	void Train(int epochs)
	{		
		InitialiseWeights(); // Ağırlıkları başlat
		// Belirlenen epoch sayısı kadar eğitim yap
		for(int e = 0; e < epochs; e++)
		{
			totalError = 0;
			for(int t = 0; t < ts.Length; t++)
			{
				UpdateWeights(t);
				Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
			}
			Debug.Log("TOTAL ERROR: " + totalError);
		}
	}
	// Başlangıçta çalışacak fonksiyon
    void Start()
    {
        Train(8);// 8 epoch'luk eğitim başlat
        // Tüm olası girdi kombinasyonlarını test et
		Debug.Log("Test 0 0:" + CalcOutput(0, 0));
		Debug.Log("Test 0 1:" + CalcOutput(0, 1));
		Debug.Log("Test 1 0:" + CalcOutput(1, 0));
		Debug.Log("Test 1 1:" + CalcOutput(1, 1));
    }
}