using System;

namespace HelloWorld
{
    class MaTran
    {
        // Thuộc tính số dòng và số cột
        private int dong, cot;
        private int[,] data;

        // Hàm nhập ma trận
        public void NhapMaTran()
        {
            Console.Write("Nhap so dong");
            dong = int.Parse(Console.ReadLine());
            Console.Write("Nhap so cot:");
            cot = int.Parse(Console.ReadLine());

            data = new int[dong, cot];

            for (int i = 0; i < dong; i++)
            {
                for (int j = 0; j < cot; j++)
                {
                    Console.Write("Nhap phan tu [{0},{1}]: ", i, j);
                    data[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }

        // Hàm in ma trận
        public void InMaTran()
        {
            for (int i = 0; i < dong; i++)
            {
                for (int j = 0; j < cot; j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        // Getter để lấy mảng
        public int[,] GetData() => data;
        public int GetDong() => dong;
        public int GetCot() => cot;

        // Cộng 2 ma trận
        public static MaTran Cong(MaTran A, MaTran B)
        {
            if (A.dong != B.dong || A.cot != B.cot)
            {
                Console.WriteLine("Khong the cong 2 ma tran khac kih thuoc");
                return null;
            }

            MaTran C = new MaTran();
            C.dong = A.dong;
            C.cot = A.cot;
            C.data = new int[C.dong, C.cot];

            for (int i = 0; i < C.dong; i++)
            {
                for (int j = 0; j < C.cot; j++)
                {
                    C.data[i, j] = A.data[i, j] + B.data[i, j];
                }
            }

            return C;
        }

        // Nhân 2 ma trận
        public static MaTran Nhan(MaTran A, MaTran B)
        {
            if (A.cot != B.dong)
            {
                Console.WriteLine("Khong the nhan (so cot A phai bang so dong B)!");
                return null;
            }

            MaTran C = new MaTran();
            C.dong = A.dong;
            C.cot = B.cot;
            C.data = new int[C.dong, C.cot];

            for (int i = 0; i < C.dong; i++)
            {
                for (int j = 0; j < C.cot; j++)
                {
                    C.data[i, j] = 0;
                    for (int k = 0; k < A.cot; k++)
                    {
                        C.data[i, j] += A.data[i, k] * B.data[k, j];
                    }
                }
            }
            return C;
        }

        // Chuyển vị
        public MaTran ChuyenVi()
        {
            MaTran T = new MaTran();
            T.dong = cot;
            T.cot = dong;
            T.data = new int[T.dong, T.cot];

            for (int i = 0; i < dong; i++)
            {
                for (int j = 0; j < cot; j++)
                {
                    T.data[j, i] = data[i, j];
                }
            }
            return T;
        }

        // Tìm max và min
        public void TimMaxMin()
        {
            int max = data[0, 0];
            int min = data[0, 0];

            for (int i = 0; i < dong; i++)
            {
                for (int j = 0; j < cot; j++)
                {
                    if (data[i, j] > max) max = data[i, j];
                    if (data[i, j] < min) min = data[i, j];
                }
            }

            Console.WriteLine("Gia tri lon nhat: " + max);
            Console.WriteLine("Gia tri nho nhat: " + min);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nhap ma tran A:");
            MaTran A = new MaTran();
            A.NhapMaTran();

            Console.WriteLine("\nNhap ma tran B:");
            MaTran B = new MaTran();
            B.NhapMaTran();

            Console.WriteLine("\nMa tran A:");
            A.InMaTran();

            Console.WriteLine("\nMa tran B:");
            B.InMaTran();

            Console.WriteLine("\nA + B:");
            MaTran C = MaTran.Cong(A, B);
            if (C != null) C.InMaTran();

            Console.WriteLine("\nA × B:");
            MaTran D = MaTran.Nhan(A, B);
            if (D != null) D.InMaTran();

            Console.WriteLine("\nChuyen vi cua A:");
            MaTran AT = A.ChuyenVi();
            AT.InMaTran();

            Console.WriteLine("\nTim max/min trong A:");
            A.TimMaxMin();
        }
    }
}




