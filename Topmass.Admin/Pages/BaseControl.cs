namespace Topmass.Admin.Pages

{
    public class TitleBoxControlItem : ControlItem
    {

        public TitleBoxControlItem()
        {
            Type = 0;
        }
    }
    public class ImageControlItem : ControlItem
    {

        public ImageControlItem()
        {
            Type = 4;
        }

    }

    public class FileControlItem : ControlItem
    {

        public FileControlItem()
        {
            Type = 12;
        }

    }


    public class BlogAvatarControlItem : ImageControlItem
    {

        public BlogAvatarControlItem()
        {
            Type = 13;
        }
    }
    public class AvatarControlItem : ImageControlItem
    {

        public AvatarControlItem()
        {
            Type = 5;
        }
    }

    public class ControlItem
    {

        public string Name { get; set; }
        public dynamic Value { get; set; }

        public int Type { get; set; }

        public string Lable { get; set; }
        public bool ReadOnly { get; set; }
        public bool Disiable { get; set; }



        public string PathControl
        {
            get
            {
                if (Type == 0)
                {
                    return "Control/titleBox";
                }

                if (Type == 1)
                {
                    return "Control/textbox";
                }

                if (Type == 2)
                {
                    return "Control/editorText";
                }

                if (Type == 3)
                {
                    return "Control/    ";
                }
                if (Type == 5)
                {
                    return "Control/avatarUpload";
                }

                if (Type == 4)
                {
                    return "Control/FileInputCs";
                }

                if (Type == 7)
                {
                    return "Control/selectBox2";
                }
                if (Type == 8)
                {
                    return "Control/mutiSelectBox";
                }
                if (Type == 9)
                {
                    return "Control/categorySelectBox";
                }
                if (Type == 10)
                {
                    return "Control/GenderBox";
                }
                if (Type == 12)
                {
                    return "Control/FileUpload";
                }
                if (Type == 13)
                {
                    return "Control/blogImageUpload";
                }
                if (Type == 14)
                {
                    return "Control/textareaBox";
                }
                


                return "Control/textbox";
            }
        }

        public ControlItem()
        {
            Type = 1;
        }
    }
}