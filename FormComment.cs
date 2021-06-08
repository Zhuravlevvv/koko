using BlogsLogic.BindingModels;
using BlogsLogic.BusinessLogic;
using BlogsLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace BlogsPlaceView
{
    public partial class FormComment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly CommentLogic commentLogic;
        private readonly BlogLogic blogLogic;

        private int? id;

        public FormComment(CommentLogic commentLogic, BlogLogic blogLogic)
        {
            InitializeComponent();
            this.commentLogic = commentLogic;
            this.blogLogic = blogLogic;
        }

        private void FormComment_Load(object sender, EventArgs e)
        {
            List<BlogViewModel> blogList = blogLogic.Read(null);
            if (blogList != null)
            {
                comboBoxBlog.DisplayMember = "BlogName";
                comboBoxBlog.ValueMember = "Id";
                comboBoxBlog.DataSource = blogList;
                comboBoxBlog.SelectedItem = null;
            }

            if (id.HasValue)
            {
                try
                {
                    var view = commentLogic.Read(new CommentBindingModel
                    {
                        Id = id
                    })?[0];
                    if (view != null)
                    {
                        textBoxTitle.Text = view.Title;
                        textBoxAuthor.Text = view.CommentAuthor;
                        textBoxText.Text = view.Text;
                        comboBoxBlog.SelectedValue = view.BlogId;
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
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                MessageBox.Show("Заполните заголовок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxAuthor.Text))
            {
                MessageBox.Show("Заполните ФИО автора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                MessageBox.Show("Заполните текст комментария", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxBlog.SelectedItem == null)
            {
                MessageBox.Show("Выберите блог", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                commentLogic.CreateOrUpdate(new CommentBindingModel
                {
                    Id = id,
                    Title = textBoxTitle.Text,
                    CommentAuthor = textBoxAuthor.Text,
                    Text = textBoxText.Text,
                    BlogId = Convert.ToInt32(comboBoxBlog.SelectedValue)
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