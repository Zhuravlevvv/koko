using BlogsLogic.BindingModels;
using BlogsLogic.BusinessLogic;
using System;
using System.Windows.Forms;
using Unity;

namespace BlogsPlaceView
{
    public partial class FormBlog : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly BlogLogic blogLogic;

        private int? id;

        public FormBlog(BlogLogic blogLogic)
        {
            InitializeComponent();
            this.blogLogic = blogLogic;
        }

        private void FormBlog_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = blogLogic.Read(new BlogBindingModel
                    {
                        Id = id
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.BlogName;
                        textBoxAuthor.Text = view.BlogAuthor;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxAuthor.Text))
            {
                MessageBox.Show("Заполните ФИО автора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                blogLogic.CreateOrUpdate(new BlogBindingModel
                {
                    Id = id,
                    BlogName = textBoxName.Text,
                    BlogAuthor = textBoxAuthor.Text
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}