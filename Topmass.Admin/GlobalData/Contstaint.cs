namespace Topmass.Admin.GlobalData
{
    public static class Constaint
    {

        public static List<dynamic> GetTimeWayOrderBy()
        {
            return new List<dynamic>()
            {
                    new
                    {
                        id = 0,
                        text = "thời gian cập nhật gần nhất"
                    },
                     new
                    {
                        id = 1,
                        text = "thời gian cập nhật xa nhất"
                    }
            };
        }
        public static List<dynamic> GetListDocumentStatus()
        {
            return new List<dynamic>() { new
                        {
                        id =0, text ="Chưa ghi nhận thông tin",
                        },
                        new  {
                        id =1, text ="Chờ duyệt",
                        },
                        new  {
                        id =2, text ="Từ chối",
                        },
                        new  {
                        id =3, text ="Đã duyệt",
                        }
             };
        }
        public static List<dynamic> GetListAccountStatus()
        {
            return new List<dynamic>() { new
                        {
                        id =0, text ="Chưa ghi nhận thông tin",
                        },
                        new  {
                        id =1, text ="Chờ duyệt",
                        },
                        new  {
                        id =2, text ="Từ chối",
                        },
                        new  {
                        id =3, text ="Đã duyệt",
                        }
             };
        }
        public static List<dynamic> GetListMailConfirm()
        {
            return new List<dynamic>()
                 {
                    new  {id = "0", text = "Chưa xác thực email"
                    },
                    new  {id = "1", text = "Cấp độ 1"
                    },
                    new  {id = "2", text = "Cấp độ 2"
                    },
                    new  {id = "3", text = "Cấp độ 3"
                    }
                };
        }
        public static List<dynamic> GetListStatusNews()
        {
            return new List<dynamic>() {
                new
                {
                id = 0, text = "Đang xét duyệt",
                },

                new {
                id = 2, text = "Đã duyệt",
                },
                new {
                id = 3, text = "Bị từ chối",
                },
                new {
                id = 4, text = "Tin bị khóa",
                }
                };
        }
        public static List<dynamic> GetListStatusDisplayNews()
        {
            return new List<dynamic>() {
                new
                {
                id = -1, text = "Tất cả",
                },
                new
                {
                id = 0, text = "Đang tắt",
                },
                new {
                id = 1, text = "Bật",
                },
                new {
                id = 2, text = "Hết hạn",
                }
                };
        }

        //candidate
        public static List<dynamic> GetListStatusCandidateMail()
        {
            return new List<dynamic>() {
                new
                {
                id = 2, text = "Xác nhận",
                },

                new {
                id = -2, text = "Chưa xác nhận",
                }

                };
        }

    }
}
