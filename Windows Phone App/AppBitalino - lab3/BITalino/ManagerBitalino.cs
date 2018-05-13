// Copyright (c) 2014, Tokyo University of Science All rights reserved.
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met: * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution. * Neither the name of the Tokyo University of Science nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.Collections;
using System;
using System.IO;

public class ManagerBitalino
{
    public enum AcquisitionState
    {
        Run = 0,
        NotRun = 1
    }

    public int[] defaultAnalogChannels = { 2 };

    public int defaultSamplingRate = 1000;

    public string pathTxt = ( AppDomain.CurrentDomain.BaseDirectory + "save.txt" );

    public string pathCsv = ( AppDomain.CurrentDomain.BaseDirectory + "save.CSV" );

    private bool isConnected = false;

    private BitalinoSerialPort bitalinoSerialPort;

    private static BITalinoDevice device;

    private string version;

    #region GETTER/SETTER

    public AcquisitionState StateAcquisition { get; set; }

    public bool IsReady { get; set; }

    public IBITalinoCommunication BitalinoCommunication { get; set; }

    public string PortName { get; set; }
    #endregion

    public ManagerBitalino ( )
    {
        StateAcquisition = AcquisitionState.NotRun;

        IsReady = false;

        BitalinoCommunication = null;
    }

    public bool IsConnected ( )
    {
        return isConnected;
    }

    public bool isAcquisition ( )
    {
        if ( StateAcquisition == AcquisitionState.Run )
        {
            return true;
        }

        return false;
    }

    public void Connection ( )
    {
        bitalinoSerialPort = new BitalinoSerialPort ( PortName );

        BitalinoCommunication = bitalinoSerialPort.BitalinoCommunication;

        device = new BITalinoDevice ( BitalinoCommunication, defaultAnalogChannels, defaultSamplingRate );

        device.Connection ( );

        isConnected = true;
    }

    public void Deconnection ( )
    {
        device.Deconnection ( );

        StateAcquisition = AcquisitionState.NotRun;

        bitalinoSerialPort = null;

        isConnected = false;
    }

    public string GetVersion ( )
    {
        version = device.GetVersion ( );

        return version;
    }

    public void StartAcquisition ( )
    {
        device.SamplingRate = defaultSamplingRate;

        device.AnalogChannels = defaultAnalogChannels;

        device.StartAcquisition ( );

        StateAcquisition = AcquisitionState.Run;

    }

    public void StopAcquisition ( )
    {
        device.StopAcquisition ( );

        StateAcquisition = AcquisitionState.NotRun;
    }

    public BITalinoFrame [ ] Read ( int nbSamples )
    {
        return device.ReadFrames ( nbSamples );
    }

    private void OnApplicationQuit ( )
    {
        if ( device != null )
        {
            device.Deconnection ( );
        }
    }
}
