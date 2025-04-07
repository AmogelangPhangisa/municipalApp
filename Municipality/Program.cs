using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Municipality
{
    // Existing Issue and IssueRepository classes remain unchanged
    // ... [Keep Issue and IssueRepository classes exactly as provided] ...

    public static class AppColors
    {
        public static readonly Color PrimaryBackground = Color.FromArgb(245, 245, 245);
        public static readonly Color PrimaryText = Color.FromArgb(51, 51, 51);
        public static readonly Color AccentColor = Color.FromArgb(30, 144, 255);
        public static readonly Color SecondaryAccent = Color.FromArgb(70, 130, 180);
        public static readonly Color ActionButton = Color.FromArgb(50, 205, 50);
        public static readonly Color DisabledColor = Color.FromArgb(224, 224, 224);
        public static readonly Color ProgressGood = Color.FromArgb(46, 139, 87);
        public static readonly Color HoverBlue = Color.FromArgb(0, 191, 255);
        public static readonly Color ErrorRed = Color.FromArgb(220, 53, 69);
    }

    public class MainMenuForm : Form
    {
        private FlowLayoutPanel buttonPanel;
        private ToolTip buttonToolTip = new ToolTip();

        public MainMenuForm()
        {
            InitializeComponents();
            ApplyDesignConsiderations();
        }

        private void InitializeComponents()
        {
            // Form setup
            this.Text = "Community Engagement Portal";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
            this.DoubleBuffered = true;

            // Main container
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                Padding = new Padding(20),
                BackColor = AppColors.PrimaryBackground
            };

            // Header Section
            var headerPanel = CreateHeaderPanel();

            // Button Container
            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(50)
            };

            // Create buttons
            var buttons = new[]
            {
                CreateMenuButton("Report Municipal Issues", true,
                    "Submit reports about infrastructure problems, sanitation issues, or other municipal concerns"),
                CreateMenuButton("Community Events", false,
                    "View and participate in local community events (Coming Soon)"),
                CreateMenuButton("Service Tracking", false,
                    "Track status of your service requests (Coming Soon)")
            };

            foreach (var btn in buttons)
            {
                buttonPanel.Controls.Add(btn);
            }

            // Assembly
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(buttonPanel, 0, 1);
            this.Controls.Add(mainLayout);
        }

        private Panel CreateHeaderPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = AppColors.AccentColor
            };

            var title = new Label
            {
                Text = "Community Services Portal",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10)
            };

            var subtitle = new Label
            {
                Text = "Your platform for municipal services and community engagement",
                Dock = DockStyle.Bottom,
                Height = 30,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.WhiteSmoke,
                TextAlign = ContentAlignment.TopCenter
            };

            panel.Controls.Add(title);
            panel.Controls.Add(subtitle);
            return panel;
        }

        private Button CreateMenuButton(string text, bool enabled, string tooltip)
        {
            var btn = new Button
            {
                Text = enabled ? text : $"{text} \u26A0",
                Size = new Size(600, 80),
                Margin = new Padding(20),
                Font = new Font("Segoe UI", 14, enabled ? FontStyle.Bold : FontStyle.Italic),
                ForeColor = Color.White,
                BackColor = enabled ? AppColors.SecondaryAccent : AppColors.DisabledColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = enabled ? Cursors.Hand : Cursors.No,
                Enabled = enabled
            };

            btn.FlatAppearance.BorderSize = 0;
            buttonToolTip.SetToolTip(btn, tooltip);

            if (enabled)
            {
                btn.MouseEnter += (s, e) => btn.BackColor = AppColors.HoverBlue;
                btn.MouseLeave += (s, e) => btn.BackColor = AppColors.SecondaryAccent;
                btn.Click += ReportIssuesButton_Click;
            }

            return btn;
        }

        private void ApplyDesignConsiderations()
        {
            this.Font = new Font("Segoe UI", 9F);
            this.Resize += (s, e) =>
            {
                foreach (Control ctrl in buttonPanel.Controls)
                {
                    ctrl.Width = buttonPanel.ClientSize.Width - 40;
                }
            };
        }

        private void ReportIssuesButton_Click(object sender, EventArgs e)
        {
            using (var reportForm = new ReportIssuesForm(this))
            {
                this.Hide();
                reportForm.ShowDialog();
                this.Show();
            }
        }
    }

    public class ReportIssuesForm : Form
    {
        // ... [All previous ReportIssuesForm implementation] ...
        // Enhanced with the following changes:

        private void InitializeComponents()
        {
            // Add accessibility features
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // Enhanced progress panel
            var progressPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Progress bar styling
            _progressBar.Style = ProgressBarStyle.Continuous;
            _progressBar.ForeColor = AppColors.ProgressGood;
        }

        private void AddFormRow(TableLayoutPanel panel, string labelText, Control inputControl, int row)
        {
            // Add input validation
            if (inputControl is TextBoxBase textBox)
            {
                textBox.Enter += (s, e) => textBox.BackColor = Color.LightYellow;
                textBox.Leave += (s, e) => textBox.BackColor = Color.White;
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // Enhanced validation
            var errorMessage = ValidateInputs();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ShowErrorIndicator(errorMessage);
                return;
            }

            // Rest of submission logic...
        }

        private string ValidateInputs()
        {
            var location = (TextBox)this.Controls.Find("locationTextBox", true)[0];
            var description = (RichTextBox)this.Controls.Find("descriptionTextBox", true)[0];

            if (string.IsNullOrWhiteSpace(location.Text))
                return "Please specify a location for the issue";

            if (string.IsNullOrWhiteSpace(description.Text) || description.Text.Length < 20)
                return "Please provide a detailed description (at least 20 characters)";

            return null;
        }

        private void ShowErrorIndicator(string message)
        {
            var errorLabel = new Label
            {
                Text = message,
                ForeColor = AppColors.ErrorRed,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 10)
            };

            var errorPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(255, 238, 238)
            };

            errorPanel.Controls.Add(errorLabel);
            this.Controls.Add(errorPanel);
            errorPanel.BringToFront();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Municipality
{
    // Existing Issue and IssueRepository classes remain unchanged
    // ... [Keep Issue and IssueRepository classes exactly as provided] ...

    public static class AppColors
    {
        public static readonly Color PrimaryBackground = Color.FromArgb(245, 245, 245);
        public static readonly Color PrimaryText = Color.FromArgb(51, 51, 51);
        public static readonly Color AccentColor = Color.FromArgb(30, 144, 255);
        public static readonly Color SecondaryAccent = Color.FromArgb(70, 130, 180);
        public static readonly Color ActionButton = Color.FromArgb(50, 205, 50);
        public static readonly Color DisabledColor = Color.FromArgb(224, 224, 224);
        public static readonly Color ProgressGood = Color.FromArgb(46, 139, 87);
        public static readonly Color HoverBlue = Color.FromArgb(0, 191, 255);
        public static readonly Color ErrorRed = Color.FromArgb(220, 53, 69);
    }

    public class MainMenuForm : Form
    {
        private FlowLayoutPanel buttonPanel;
        private ToolTip buttonToolTip = new ToolTip();

        public MainMenuForm()
        {
            InitializeComponents();
            ApplyDesignConsiderations();
        }

        private void InitializeComponents()
        {
            // Form setup
            this.Text = "Community Engagement Portal";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
            this.DoubleBuffered = true;

            // Main container
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                Padding = new Padding(20),
                BackColor = AppColors.PrimaryBackground
            };

            // Header Section
            var headerPanel = CreateHeaderPanel();

            // Button Container
            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(50)
            };

            // Create buttons
            var buttons = new[]
            {
                CreateMenuButton("Report Municipal Issues", true,
                    "Submit reports about infrastructure problems, sanitation issues, or other municipal concerns"),
                CreateMenuButton("Community Events", false,
                    "View and participate in local community events (Coming Soon)"),
                CreateMenuButton("Service Tracking", false,
                    "Track status of your service requests (Coming Soon)")
            };

            foreach (var btn in buttons)
            {
                buttonPanel.Controls.Add(btn);
            }

            // Assembly
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(buttonPanel, 0, 1);
            this.Controls.Add(mainLayout);
        }

        private Panel CreateHeaderPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = AppColors.AccentColor
            };

            var title = new Label
            {
                Text = "Community Services Portal",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10)
            };

            var subtitle = new Label
            {
                Text = "Your platform for municipal services and community engagement",
                Dock = DockStyle.Bottom,
                Height = 30,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.WhiteSmoke,
                TextAlign = ContentAlignment.TopCenter
            };

            panel.Controls.Add(title);
            panel.Controls.Add(subtitle);
            return panel;
        }

        private Button CreateMenuButton(string text, bool enabled, string tooltip)
        {
            var btn = new Button
            {
                Text = enabled ? text : $"{text} \u26A0",
                Size = new Size(600, 80),
                Margin = new Padding(20),
                Font = new Font("Segoe UI", 14, enabled ? FontStyle.Bold : FontStyle.Italic),
                ForeColor = Color.White,
                BackColor = enabled ? AppColors.SecondaryAccent : AppColors.DisabledColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = enabled ? Cursors.Hand : Cursors.No,
                Enabled = enabled
            };

            btn.FlatAppearance.BorderSize = 0;
            buttonToolTip.SetToolTip(btn, tooltip);

            if (enabled)
            {
                btn.MouseEnter += (s, e) => btn.BackColor = AppColors.HoverBlue;
                btn.MouseLeave += (s, e) => btn.BackColor = AppColors.SecondaryAccent;
                btn.Click += ReportIssuesButton_Click;
            }

            return btn;
        }

        private void ApplyDesignConsiderations()
        {
            this.Font = new Font("Segoe UI", 9F);
            this.Resize += (s, e) =>
            {
                foreach (Control ctrl in buttonPanel.Controls)
                {
                    ctrl.Width = buttonPanel.ClientSize.Width - 40;
                }
            };
        }

        private void ReportIssuesButton_Click(object sender, EventArgs e)
        {
            using (var reportForm = new ReportIssuesForm(this))
            {
                this.Hide();
                reportForm.ShowDialog();
                this.Show();
            }
        }
    }

    public class ReportIssuesForm : Form
    {
        // ... [All previous ReportIssuesForm implementation] ...
        // Enhanced with the following changes:

        private void InitializeComponents()
        {
            // Add accessibility features
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // Enhanced progress panel
            var progressPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Progress bar styling
            _progressBar.Style = ProgressBarStyle.Continuous;
            _progressBar.ForeColor = AppColors.ProgressGood;
        }

        private void AddFormRow(TableLayoutPanel panel, string labelText, Control inputControl, int row)
        {
            // Add input validation
            if (inputControl is TextBoxBase textBox)
            {
                textBox.Enter += (s, e) => textBox.BackColor = Color.LightYellow;
                textBox.Leave += (s, e) => textBox.BackColor = Color.White;
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // Enhanced validation
            var errorMessage = ValidateInputs();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ShowErrorIndicator(errorMessage);
                return;
            }

            // Rest of submission logic...
        }

        private string ValidateInputs()
        {
            var location = (TextBox)this.Controls.Find("locationTextBox", true)[0];
            var description = (RichTextBox)this.Controls.Find("descriptionTextBox", true)[0];

            if (string.IsNullOrWhiteSpace(location.Text))
                return "Please specify a location for the issue";

            if (string.IsNullOrWhiteSpace(description.Text) || description.Text.Length < 20)
                return "Please provide a detailed description (at least 20 characters)";

            return null;
        }

        private void ShowErrorIndicator(string message)
        {
            var errorLabel = new Label
            {
                Text = message,
                ForeColor = AppColors.ErrorRed,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 10)
            };

            var errorPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(255, 238, 238)
            };

            errorPanel.Controls.Add(errorLabel);
            this.Controls.Add(errorPanel);
            errorPanel.BringToFront();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}