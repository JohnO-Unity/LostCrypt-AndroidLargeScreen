// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
package com.unity.lostcryptlargescreenexample;

import android.app.Activity;
import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static androidx.core.content.ContextCompat.getSystemService;

public final class HingeAngleSensor {
    private static final String HINGE_ANGLE_SENSOR_NAME = "Hinge Angle".toLowerCase(); // works on some other devices/emulators, use sensor.getName()

    private SensorManager mSensorManager;
    private Sensor mHingeAngleSensor;
    private SensorEventListener mSensorListener;
    private float lastValue = -1f;

    public void setupSensor() {

        if (mHingeAngleSensor == null) {
            // we haven't set it yet!
            List<Sensor> sensorList = mSensorManager.getSensorList(Sensor.TYPE_ALL);

            for (Sensor sensor : sensorList) {
                if (sensor.getName().toLowerCase().contains(HINGE_ANGLE_SENSOR_NAME)) {
                    mHingeAngleSensor = sensor;
                }
            }

            mSensorListener = new SensorEventListener() {
                @Override
                public void onSensorChanged(final SensorEvent event) {
                    if (event.sensor == mHingeAngleSensor) {
                        lastValue = event.values[0];
                    }
                }

                @Override
                public void onAccuracyChanged(Sensor sensor, int accuracy) {
                    //TODO: support accuracy change
                }
            };

            mSensorManager.registerListener(mSensorListener, mHingeAngleSensor, SensorManager.SENSOR_DELAY_GAME);
        }
    }
    /*
    * Get the last measured hinge angle value (-2 if a problem occurred)
    */
    public float getHingeAngle() {
        if (mSensorListener == null) {
            return -2f;
        }
        return lastValue;
    }

    public int isHingeEnabled() {
        return mSensorListener != null ? 1 : 0;
    }

    /*
     * Create the hinge angle sensor class (singleton)
     */
    public static HingeAngleSensor getInstance(Activity currentActivity) {
        // double check synchronization
        HingeAngleSensor result = instance;
        if (result == null) {
            synchronized (SINGLETON_LOCK) {
                result = instance;
                if (result == null) {
                    result = new HingeAngleSensor();
                    result.mSensorManager = (SensorManager) currentActivity.getSystemService(Context.SENSOR_SERVICE);
                    instance = result;
                }
            }
        }
        return result;
    }
    private static final Object SINGLETON_LOCK = new Object();
    private static volatile HingeAngleSensor instance;
    private HingeAngleSensor() {
        // Singleton
    }

    /*
    * Unregister listener - must be called by Unity
    */
    public void dispose() {
        mSensorManager.unregisterListener(mSensorListener);
    }
}
