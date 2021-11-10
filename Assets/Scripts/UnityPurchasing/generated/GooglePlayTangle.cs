// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("mmp0bBEdHGdBoKvzDmAfrxfocIH6P3rz+1NKctUHxrSNVdBDdItuVqz5TF98gVM6mjZbLTadW14iteT7jHbCMFKY4ZQE8DTMzl2JdkSXOhbVWeTneEkJrghilorcghotC1eMD+7tKrzZJim+EotKC1jM/VPbU2jU6fxbAVCPJ4D91cIzaLyXETO0Ja1uzbIzx8F2AtAYM9298iYkJ7ELtfH0RGQoCfUIIm1aR03poB9W/5KZNkYFzfx2tOthGG+B+WlDfCSyJZXlF/Lv1G9rEtLmP9CsIrBv6cHSJgOAjoGxA4CLgwOAgIEALMSQ5OqR59ZXtvxGGKManqxQbWBHSKc9slaxA4CjsYyHiKsHyQd2jICAgISBghOJLsCcW9rrWIOCgIGA");
        private static int[] order = new int[] { 13,5,10,10,9,10,7,8,11,12,11,11,12,13,14 };
        private static int key = 129;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
