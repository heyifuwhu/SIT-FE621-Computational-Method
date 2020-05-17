﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implied_Volatility
{
    class BisectionAlgorithm
    {
        // Standard Normal CDF ===================================================================================
        public double NormCDF(double x)
        {
            double x1 = 7.0*Math.Exp(-0.5*x*x);
            double x2 = 16.0*Math.Exp(-x*x*(2.0 - Math.Sqrt(2.0)));
            double x3 = (7.0 + 0.25*Math.PI*x*x)*Math.Exp(-x*x);
            double Q = 0.5*Math.Sqrt(1.0 - (x1 + x2 + x3)/30.0);
            if(x > 0)
                return 0.5 + Q;
            else
                return 0.5 - Q;
        }
        // Black Scholes Price of Call or put ====================================================================
        public double BlackScholes(double S,double K,double T,double rf,double q,double v,string PutCall)
        {
            double d1 = (Math.Log(S/K) + (rf-q+v*v/2.0)*T) / v / Math.Sqrt(T);
            double d2 = d1 - v*Math.Sqrt(T);
            double BSCall = S*Math.Exp(-q*T)*NormCDF(d1) - K*Math.Exp(-rf*T)*NormCDF(d2);
            double Price = 0.0;
            if(PutCall == "C")
                Price = BSCall;
            else if(PutCall == "P")
                Price = BSCall - S*Math.Exp(-q*T) + K*Math.Exp(-rf*T);
            return Price;
        }
        // Bisection Algorithm for Black Scholes Implied Volatility
        public double BisecBSIV(string PutCall,double S,double K,double rf,double q,double T,double a,double b,double MktPrice,double Tol,int MaxIter)
        {
            double lowCdif  = MktPrice - BlackScholes(S,K,T,rf,q,a,PutCall);
            double highCdif = MktPrice - BlackScholes(S,K,T,rf,q,b,PutCall);
            double BSIV = 0.0;
            double midP;
            if(lowCdif*highCdif > 0.0)
                BSIV = -1.0;
            else
            {
                for(int x=0;x<=MaxIter;x++)
                {
                    midP = (a+b)/2.0;
                    double midCdif  = MktPrice - BlackScholes(S,K,T,rf,q,midP,PutCall);
                    if(Math.Abs(midCdif) < Tol)
                        break;
                    else
                    {
                        if(midCdif > 0.0)
                            a = midP;
                        else
                            b = midP;

                    }
                    BSIV = midP;
                }
            }
            return BSIV;
        }
    }
}


