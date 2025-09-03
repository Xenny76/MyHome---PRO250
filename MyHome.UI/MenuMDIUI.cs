using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyHome.Services;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    /// The main form -allows the user to view and edit data of their income and expenses
    /// </summary>
    public partial class MenuMDIUI : Form
    {
        /// <summary>
        /// The amount of Mdi children open
        /// </summary>
        public int MdiChilrenSum { get; set; }

        /// <summary>
        /// Single instance of expense categories form
        /// </summary>
        public ViewCategoriesUI ExpCatForm { get; set; }

        /// <summary>
        /// Single instance of income categories form
        /// </summary>
        public ViewCategoriesUI IncCatForm { get; set; }

        /// <summary>
        /// Single instance of payment method categories form
        /// </summary>
        public ViewCategoriesUI PaymentCatForm { get; set; }

        /// <summary>
        /// Single instance of new income form
        /// </summary>
        public InputINUI NewIncome { get; set; }

        /// <summary>
        /// Single instance of new expense form
        /// </summary>
        public InputOutUI NewExpense { get; set; }

        /// <summary>
        /// Single instance of new recurring expense form
        /// </summary>
        public RecurringExpenseInput NewRecurringExpense { get; set; }

        /// <summary>
        /// Single instance of new recurring income form
        /// </summary>
        public RecurringIncomeInput NewRecurringIncome { get; set; }

        private Panel navigationPanel;
        private Dictionary<string, ToolStripMenuItem> topMenuItems;


        /// <inheritdoc />
        /// <summary>
        /// Default ctor
        /// </summary>
        public MenuMDIUI()
        {
            InitializeComponent();

            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
            this.BackgroundImage = null;

            navigationPanel = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(45, 45, 48),
            };
            this.Controls.Add(navigationPanel);
            this.Controls.SetChildIndex(navigationPanel, 1);

            InitializeMenus();

            mainMenuToolStripMenuItem.Click += (s, e) => ShowMenu("Main");
            frameWorkToolStripMenuItem.Click += (s, e) => ShowMenu("Framework");
            visualizationToolStripMenuItem.Click += (s, e) => ShowMenu("Visualization");

            topMenuItems = new Dictionary<string, ToolStripMenuItem>
            {
                { "Main", mainMenuToolStripMenuItem },
                { "Framework", frameWorkToolStripMenuItem },
                { "Visualization", visualizationToolStripMenuItem }
            };

            //menuStrip1.Renderer = new DarkRenderer();
            statusStrip.Renderer = new DarkRenderer();
            menuStrip1.Renderer = new DarkRenderer(topMenuItems);

            ShowMenu("Main");
            //AdjustMenuHeight(menuPages["Main"]);
        }

        private Dictionary<string, FlowLayoutPanel> menuPages = new Dictionary<string, FlowLayoutPanel>();

        private void InitializeMenus()
        {
            // Main menu
            var mainFlow = CreateMenuFlow(new (string, EventHandler)[]
            {
        ("View Details", ViewDetailToolStripMenuItem_Click),
        ("Single Income", NewIncomeToolStripMenuItem_Click),
        ("Single Expense", NewExcpenceToolStripMenuItem_Click),
        ("Recurring Income", RecurringIncomeToolStripMenuItem_Click),
        ("Recurring Expense", RecurringExpenseToolStripMenuItem_Click),
        ("Graph", CategoryGraphToolStripMenuItem_Click),
        ("Pie Chart", CategoryPieChartToolStripMenuItem_Click),
        ("Payment Method Chart", MethodPieChartToolStripMenuItem_Click)
            });
            menuPages["Main"] = mainFlow;
            navigationPanel.Controls.Add(mainFlow);

            // Framework menu
            var frameworkFlow = CreateMenuFlow(new (string, EventHandler)[]
            {
        ("Expense Category", ExCatToolStripMenuItem_Click),
        ("Income Category", IncomeCatToolStripMenuItem_Click),
        ("Payment Category", PaymentCatToolStripMenuItem_Click)
            }, visible: false);
            menuPages["Framework"] = frameworkFlow;
            navigationPanel.Controls.Add(frameworkFlow);

            // Visualization menu
            var visualizationFlow = CreateMenuFlow(new (string, EventHandler)[]
            {
        ("Category Graph", CategoryGraphToolStripMenuItem_Click),
        ("Multiple Category Graph", MethodGraphToolStripMenuItem_Click),
        ("Category Pie Chart", CategoryPieChartToolStripMenuItem_Click),
        ("Method Pie Chart", MethodPieChartToolStripMenuItem_Click)
            }, visible: false);
            menuPages["Visualization"] = visualizationFlow;
            navigationPanel.Controls.Add(visualizationFlow);
        }

        private FlowLayoutPanel CreateMenuFlow((string Text, EventHandler Click)[] buttons, bool visible = true)
        {
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = navigationPanel.BackColor,
                Padding = new Padding(10),
                Visible = visible
            };

            foreach (var (text, click) in buttons)
                flow.Controls.Add(CreateMenuButton(text, click));

            return flow;
        }

        private void AdjustMenuHeight(FlowLayoutPanel flow)
        {
            int maxHeight = 0;
            foreach (Control c in flow.Controls)
            {
                if (c.Height > maxHeight)
                    maxHeight = c.Height;
            }

            int minHeight = 50;
            navigationPanel.Height = Math.Max(minHeight, maxHeight + flow.Padding.Vertical + 10);
        }

        private string activeMenu = null;
        private void ShowMenu(string menuName)
        {
            // If clicking the same menu, toggle visibility
            if (activeMenu == menuName)
            {
                var page = menuPages[menuName];
                page.Visible = !page.Visible;

                if (page.Visible)
                {
                    AdjustMenuHeight(page);
                    navigationPanel.Visible = true;
                }
                else
                {
                    navigationPanel.Height = 0;
                    navigationPanel.Visible = false;
                    activeMenu = null;
                }

                UpdateMenuHighlights(); // update button state
                return;
            }

            // Otherwise switch menus
            foreach (var kvp in menuPages)
                kvp.Value.Visible = kvp.Key == menuName;

            navigationPanel.Visible = true;
            AdjustMenuHeight(menuPages[menuName]);
            activeMenu = menuName;

            UpdateMenuHighlights(); // highlight correct button
        }

        private void UpdateMenuHighlights()
        {
            foreach (var kvp in topMenuItems)
            {
                var item = kvp.Value;
                bool isActive = kvp.Key == activeMenu;

                // Tell renderer
                (menuStrip1.Renderer as DarkRenderer)?.SetActive(item, isActive);

                // Text colors
                if (isActive)
                {
                    item.ForeColor = Color.White;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }
                else
                {
                    item.ForeColor = Color.Gainsboro;
                    item.Font = new Font(item.Font, FontStyle.Regular);
                }
            }

            menuStrip1.Invalidate(); // force redraw
        }

        private Button CreateMenuButton(string text, EventHandler click)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = Color.FromArgb(35, 35, 38),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += click;

            using (Graphics g = this.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(text, this.Font);
                btn.Size = new Size((int)textSize.Width + 20, (int)textSize.Height + 20);
            }

            return btn;
        }

        class DarkRenderer : ToolStripProfessionalRenderer
        {
            private readonly Dictionary<ToolStripMenuItem, bool> activeItems;

            public DarkRenderer(Dictionary<string, ToolStripMenuItem> topMenuItems)
    : base(new DarkColorTable())
            {
                activeItems = topMenuItems.ToDictionary(kvp => kvp.Value, kvp => false);
            }

            public DarkRenderer() : base(new DarkColorTable()) { }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(28, 28, 28)), e.AffectedBounds);
            }

            public void SetActive(ToolStripMenuItem item, bool active)
            {
                if (activeItems.ContainsKey(item))
                    activeItems[item] = active;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var item = e.Item as ToolStripMenuItem;

                // Active highlight (persistent)
                if (item != null && activeItems.TryGetValue(item, out bool isActive) && isActive)
                {
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(70, 70, 70)))
                        e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
                }
                // Hover highlight
                else if (e.Item.Selected)
                {
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(60, 60, 60)))
                        e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
                }
                // Default background
                else
                {
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(45, 45, 48)))
                        e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
                }

                base.OnRenderMenuItemBackground(e);
            }

            // Text Color
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = Color.White;
                base.OnRenderItemText(e);
            }


            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                e.Graphics.FillRectangle(Brushes.Gray, e.Item.Bounds.X, e.Item.Bounds.Y + e.Item.Bounds.Height / 2, e.Item.Bounds.Width, 1);
            }

            // Submenu Arrow Color
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = Color.White;
                base.OnRenderArrow(e);
            }
        }

        class DarkColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(60, 60, 60); // hover
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(60, 60, 60);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(60, 60, 60);

            public override Color MenuItemBorder => Color.FromArgb(60, 60, 60);

            public override Color MenuStripGradientBegin => Color.FromArgb(28, 28, 28);
            public override Color MenuStripGradientEnd => Color.FromArgb(28, 28, 28);

            public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
            public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 48);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 48);
            public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 48);
        }

        private void MenuMDIUI_MdiChildActivate(object sender, EventArgs e)
        {
            tslblMdiChildNumber.Text = MdiChilrenSum.ToString();

            if (ActiveMdiChild != null)
                ApplyDarkTheme(ActiveMdiChild);
        }

        private void ApplyDarkTheme(Form form)
        {
            form.BackColor = Color.FromArgb(45, 45, 48);
            form.ForeColor = Color.White;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            foreach (Control ctrl in form.Controls)
                ApplyDarkThemeToControl(ctrl);
        }

        private void ApplyDarkThemeToControl(Control ctrl)
        {
            ctrl.BackColor = Color.FromArgb(45, 45, 48);
            ctrl.ForeColor = Color.White;

            switch (ctrl)
            {
                case Button btn:
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(35, 35, 38);
                    btn.ForeColor = Color.White;
                    break;

                case TextBox txt:
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.FromArgb(30, 30, 30);
                    txt.ForeColor = Color.White;
                    break;

                case ComboBox cb:
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.BackColor = Color.FromArgb(30, 30, 30);
                    cb.ForeColor = Color.White;
                    break;

                case DataGridView dgv:
                    dgv.BackgroundColor = Color.FromArgb(45, 45, 48);
                    dgv.ForeColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 38);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 38);
                    dgv.RowHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
                    dgv.DefaultCellStyle.ForeColor = Color.White;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70);
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    break;

                case Label lbl:
                    lbl.Font = new Font(lbl.Font.FontFamily, 9, lbl.Font.Style);
                    lbl.ForeColor = Color.White;
                    break;

                case GroupBox gb:
                    gb.ForeColor = Color.White;
                    break;

                case Panel pnl:
                    pnl.BackColor = Color.FromArgb(45, 45, 48);
                    break;
            }

            foreach (Control child in ctrl.Controls)
                ApplyDarkThemeToControl(child);
        }

        /// <summary>
        /// Closes the form -on close FormClosing will activate and check for changes in
        /// the data base
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows or hides the status bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ShowStatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = showStatusBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Closes all child forms currently open
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// When the form loads the data is loaded from the data base into the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MenuMDIUI_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Opens the form to view the data for the whole month
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ViewDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var childData = new DataViewUI { MdiParent = this };
            childData.Show();
            childData.FormClosed += MdiChildClosed;
        }

        /// <summary>
        /// Opens the form to allow the user to add a new income
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewIncome == null)
            {
                MdiChilrenSum++;
                NewIncome = new InputINUI { MdiParent = this };
                NewIncome.Show();
                NewIncome.FormClosed += MdiChildClosed;
                NewIncome.FormClosed += NewIncomeClose;
            }
            // Forces the form to the front
            else
            {
                NewIncome.BringToFront();
            }
        }

        /// <summary>
        /// Opens the form to allow the user to add a new expense
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewExcpenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewExpense == null)
            {
                MdiChilrenSum++;
                NewExpense = new InputOutUI { MdiParent = this };
                NewExpense.Show();
                NewExpense.FormClosed += MdiChildClosed;
                NewExpense.FormClosed += NewExpenseClose;
            }
            // Forces the form to the front
            else
            {
                NewExpense.BringToFront();
            }
        }

        /// <summary>
        /// Opens the form to allow the user to add a new recurring expense
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void RecurringExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewRecurringExpense == null)
            {
                MdiChilrenSum++;
                var childData = new RecurringExpenseInput { MdiParent = this };
                childData.Show();
                childData.FormClosed += MdiChildClosed;
                childData.FormClosed += NewRecurringExpenseClose;
            }
            // Forces the form to the front
            else
            {
                NewRecurringExpense.BringToFront();
            }
        }

        /// <summary>
        /// Opens the form to allow the user to add a new recurring income
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void RecurringIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewRecurringIncome == null)
            {
                MdiChilrenSum++;
                var childData = new RecurringIncomeInput { MdiParent = this };
                childData.Show();
                childData.FormClosed += MdiChildClosed;
                childData.FormClosed += NewRecurringIncomeClose;
            }
            // Forces the form to the front
            else
            {
                NewRecurringIncome.BringToFront();
            }
        }

        /// <summary>
        /// Displays information about the program
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutUI().ShowDialog();
        }

        /// <summary>
        /// Shows the data for the month in a pie chart
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CategoryPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var mcuNew = new MonthChartUI(DateTime.Now.Date) { MdiParent = this };
            mcuNew.Show();
            mcuNew.FormClosed += MdiChildClosed;
        }

        private void MethodPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var mcuNew = new DataPerPaymentMethod(DateTime.Now.Date) { MdiParent = this };
            mcuNew.Show();
            mcuNew.FormClosed += MdiChildClosed;
        }

        private void MethodGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var mcuNew = new MultipleCategoriesCompare { MdiParent = this };
            mcuNew.Show();
            mcuNew.FormClosed += MdiChildClosed;
        }

        /// <summary>
        /// Shows a list of the options in the expenses category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (ExpCatForm == null)
            {
                MdiChilrenSum++;
                ExpCatForm = new ViewCategoriesUI(CategoryType.Expense) { MdiParent = this };
                ExpCatForm.Show();
                ExpCatForm.FormClosed += MdiChildClosed;
                ExpCatForm.FormClosed += ExpCatClose;
            }
            // Forces the form to the front
            else
            {
                ExpCatForm.BringToFront();
            }
        }

        /// <summary>
        /// Shows a list of the options in the income category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void IncomeCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (IncCatForm == null)
            {
                MdiChilrenSum++;
                IncCatForm = new ViewCategoriesUI(CategoryType.Income) { MdiParent = this };
                IncCatForm.Show();
                IncCatForm.FormClosed += MdiChildClosed;
                IncCatForm.FormClosed += IncCatClose;
            }
            // Forces the form to the front
            else
            {
                IncCatForm.BringToFront();
            }
        }

        /// <summary>
        /// Shows a list of the options in the payment method category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void PaymentCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (PaymentCatForm == null)
            {
                MdiChilrenSum++;
                PaymentCatForm = new ViewCategoriesUI(CategoryType.PaymentMethod) { MdiParent = this };
                PaymentCatForm.Show();
                PaymentCatForm.FormClosed += MdiChildClosed;
                PaymentCatForm.FormClosed += PaymentCatClose;
            }
            // Forces the form to the front
            else
            {
                PaymentCatForm.BringToFront();
            }
        }

        /// <summary>
        /// Shows a flow chart of the last year per selected category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CategoryGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var childData = new DataChartUI { MdiParent = this };
            childData.Show();
            childData.FormClosed += MdiChildClosed;
        }

        /// <summary>
        /// When form closes checks for changes to cache and asks if they could be saved
        ///  - also allows the user to cancel the exit
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MenuMDIUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the cache has any changes
            //if (DataStatusHandler.DataHasChanges())
            //{
            //    DialogResult = MessageBox.Show("Changes detected\nDo you want to save the changes?",
            //                                   "Closing...",
            //                                   MessageBoxButtons.YesNoCancel,
            //                                   MessageBoxIcon.Question,
            //                                   MessageBoxDefaultButton.Button1);

            //    // If the user is saving the changes
            //    // if the user wants to exit but not save changes the form will just close
            //    if (DialogResult == DialogResult.Yes)
            //    {
            //        GlobalHandler.SaveData();
            //    }
            //    // If the user does not want to exit the program
            //    else if (DialogResult == DialogResult.Cancel)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        /// <summary>
        /// Saves changes to the data base if the user confirms
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            // If the cache has any changes
            //if (DataStatusHandler.DataHasChanges())
            //{
            //    DialogResult = MessageBox.Show("Changes detected\nDo you want to save the changes?",
            //                                   "Saving...",
            //                                   MessageBoxButtons.YesNo,
            //                                   MessageBoxIcon.Question,
            //                                   MessageBoxDefaultButton.Button1);

            //    // If the user is saving the changes
            //    if (DialogResult == DialogResult.Yes)
            //    {
            //        GlobalHandler.SaveData();
            //    }
            //}
        }

        /// <summary>
        /// Gives the user a choice of type of new item to add
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            int nResult;

            // Opens the form with the users choices
            using (var ncUserChoice = new NewChoice())
            {
                // the form is opened as ShowDialog to be able to preserve the
                // property values after it is closed
                ncUserChoice.ShowDialog();

                // Saves the users choice
                nResult = ncUserChoice.UserChoice;
            }

            // Opens the appropriate form based on the users choice that was sent back
            // Opening the form to add a new expense
            if (nResult == 1)
            {
                NewExcpenceToolStripMenuItem_Click(sender, e);
            }
            // Opening the form to add a new income
            else if (nResult == 2)
            {
                NewIncomeToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Backups the data currently in the cache, saving it to text files
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void BackupStripMenuItem_Click(object sender, EventArgs e)
        {
            // Performs a backup of all the data, each table gets its own file
            new ProgressForm().BackupAllData();
        }

        /// <summary>
        /// When any form is opened updates the status bar with the number of mdi children
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        //private void MenuMDIUI_MdiChildActivate(object sender, EventArgs e)
        //{
        //    tslblMdiChildNumber.Text = MdiChilrenSum.ToString();
        //}

        /// <summary>
        /// When the new income form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewIncomeClose(object sender, FormClosedEventArgs e)
        {
            NewIncome = null;
        }

        /// <summary>
        /// When the new expense form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewExpenseClose(object sender, FormClosedEventArgs e)
        {
            NewExpense = null;
        }

        /// <summary>
        /// When the new recurring expense form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewRecurringExpenseClose(object sender, FormClosedEventArgs e)
        {
            NewRecurringExpense = null;
        }

        /// <summary>
        /// When the new recurring income form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewRecurringIncomeClose(object sender, FormClosedEventArgs e)
        {
            NewRecurringIncome = null;
        }

        /// <summary>
        /// When the expense categories form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExpCatClose(object sender, FormClosedEventArgs e)
        {
            ExpCatForm = null;
        }

        /// <summary>
        /// When the income categories form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void IncCatClose(object sender, FormClosedEventArgs e)
        {
            IncCatForm = null;
        }

        /// <summary>
        /// When the payment method categories form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void PaymentCatClose(object sender, FormClosedEventArgs e)
        {
            PaymentCatForm = null;
        }

        /// <summary>
        /// For any mdi child form that closes updates the variable with the sum, 
        /// and updates the status bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MdiChildClosed(object sender, FormClosedEventArgs e)
        {
            MdiChilrenSum--;
            tslblMdiChildNumber.Text = MdiChilrenSum.ToString();
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            navigationPanel.Visible = !navigationPanel.Visible;
            navigationPanel.BringToFront();
        }
    }
}
