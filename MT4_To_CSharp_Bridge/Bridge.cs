﻿#define BY_REF
#define BOOLS
#define STRINGS

using DNNE;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
namespace MT4_To_CSharp_Bridge
{
    public static class Bridge
    {
        [UnmanagedCallersOnly(EntryPoint = "GimmeAnInt")]
        public static int GimmeAnInt(int a)
        {
            a += 1;
            return a;
        }



        [UnmanagedCallersOnly(EntryPoint = "GetIntSum")]
        public static int GetIntSum(int a, int b)
        {
            a += b;
            b += 2;
            return a;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetSum")]
        public static int GetSum(int a, double b)
        {
            a += (int)b;
            b += 2;
            return a;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetSumDoubles")]
        public static int GetSumDoubles(double a, double b)
        {
            a += b;
            b += 2.8;
            return (int)a;
        }

#if BY_REF
        [UnmanagedCallersOnly(EntryPoint = "GimmeAnIntRefDummy")]
        public unsafe static int GimmeAnIntRefDummy(int* a)
        {
            return 1;
        }


        [UnmanagedCallersOnly(EntryPoint = "GimmeAnIntRef")]
        public unsafe static int GimmeAnIntRef(int* a)
        {
            return *a;
        }

        [UnmanagedCallersOnly(EntryPoint = "GimmeAnIntRefMod")]
        public unsafe static int GimmeAnIntRefMod(int* a)
        {
            *a += 1;
            return *a;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetIntSumRef")]
        public unsafe static int GetIntSumRef(int* a, int* b)
        {
            *a += 1;
            *b += 2;
            return *a + *b;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetSumRef")]
        public unsafe static int GetSumRef(int* a, double* b)
        {
            *a += 1;
            *b += 2.8;
            return *a + (int)*b;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetSumDoublesRef")]
        public unsafe static int GetSumDoublesRef(double* a, double* b)
        {
            *a += 1.6;
            *b += 2.2;
            return (int)(*a + *b);
        }
#endif
#if BOOLS

        [UnmanagedCallersOnly(EntryPoint = "GetNot")]
        public static byte GetNot(byte a)
        {
            return MQL4Converter.WriteBool(GetNotManaged(MQL4Converter.ReadBool(a)));
        }
        /*
        //this is not visible by DNNE 
        [UnmanagedCallersOnly(EntryPoint = "GetNot2")]
        //[return : MarshalAs(UnmanagedType.I1)] //- this stops generating 
        public static bool GetNot2([MarshalAs(UnmanagedType.I1)] bool a)
        {
            return !a;
        }
        */


        public static bool GetNotManaged(bool a)
        {
            return !a;
        }
#endif
#if STRINGS
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MQLString
        {
            public int size;
            public IntPtr buffer;
            int reserverd;
        }

        /*
        [C99DeclCode(@"#pragma pack(push,1)
    struct MqlString
    {
        int      size;       // 32-bit integer, contains size of the buffer, allocated for the string.
        wchar_t*   buffer;     // 32-bit address of the buffer, containing the string. [LPWSTR] 
        int      reserved;   // 32-bit integer, reserved.
    };
    #pragma pack(pop,1)
    typedef struct MqlString string;")]
        */
        [UnmanagedCallersOnly(EntryPoint = "GetStringLength")]
        public unsafe static int GetStringLength([C99Type("wchar_t *")] char *a)
        {
            string b = new string(a);
            return b.Length;
        }

        [UnmanagedCallersOnly(EntryPoint = "ConvertHexToInt")]
        public unsafe static int ConvertHexToInt([C99Type("wchar_t *")] char *a)
        {
            string b = new string(a);
            int result;
            bool correct = int.TryParse(b, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
            return correct ? result : -1;
            
        }
#endif

        /*
        [UnmanagedCallersOnly(EntryPoint = "GetAnswerByValue")]
        public static int GetAnswerByValue(int a, double b, bool c)
        {
            return a + (int)b + (c ? 1 : 0);
        }
        */

        /*
        [UnmanagedCallersOnly(EntryPoint = "GetAnswerByValueEx")]
        public static int GetAnswerByValueEx(int a, double b, bool c, string d)
        {
            return a + (int)b + (c ? 1 : 0);
        }
        */


        //[UnmanagedCallersOnly(EntryPoint = "GetAnswerByReference")]
        //public static void GetAnswerByReference(ref int a, ref double b, ref bool c, ref string d)
        //{
        //    a *= 2;
        //    b *= 2;
        //    c = !c;
        //    d = "This is the result from C#";
        //}


        //[UnmanagedCallersOnly(EntryPoint = "GetAnswerByJSONObject")]
        //public static string GetAnswerByJSONObject(int a, double b, bool c, string d)
        //{
        //    string json = "Turn variables into serialized string";
        //    return json;
        //}
    }
}
