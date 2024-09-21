package com.example.tutorial_1

import androidx.appcompat.app.AppCompatActivity
import android.widget.EditText
import android.widget.TextView
import android.os.Bundle
import android.view.View

class MainActivity : AppCompatActivity() {
    private var height: EditText ? = null
    private var weight: EditText ? = null
    private var result: TextView ? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        height = findViewById<EditText>(R.id.editText)
        weight = findViewById<EditText>(R.id.editText2)
        result = findViewById<TextView>(R.id.textView)
    }

    fun calc(v: View?) {
        val h = height!!.text.toString().toFloat()
        val w = weight!!.text.toString().toFloat()
        val bmi = w / h / h
        result!!.text = "Your BMI is: $bmi"
    }
}