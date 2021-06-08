using BlogsLogic.BindingModels;
using BlogsLogic.BusinessLogic;
using System;
using System.Windows.Forms;
using Unity;

namespace BlogsPlaceView
{
    public partial class FormComments : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly CommentLogic commentLogic;

        public FormComments(CommentLogic commentLogic)
        {
            InitializeComponent();
            this.commentLogic = commentLogic;
        }

        private void LoadData()
        {
            try
            {
                var list = commentLogic.Read(null);
                if (list != null)
                {
                    dataGridViewComments.DataSource = list;
                    dataGridViewComments.Columns[0].Visible = false;
                    dataGridViewComments.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewComments.Columns[5].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormComment>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewComments.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormComment>();
                form.Id = Convert.ToInt32(dataGridViewComments.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewComments.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewComments.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        commentLogic.Delete(new CommentBindingModel
                        {
                            Id = id
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}