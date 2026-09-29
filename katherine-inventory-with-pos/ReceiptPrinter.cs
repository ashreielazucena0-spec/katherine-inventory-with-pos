using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace katherine_inventory_with_pos.Forms
{
    /// <summary>
    /// Prints a simple monospaced text receipt (works with regular printers and most
    /// 80mm thermal receipt printers configured as the Windows default printer).
    /// </summary>
    public class ReceiptPrinter
    {
        private readonly List<string> lines;
        private readonly PrintDocument document;
        private readonly Font font = new Font("Consolas", 10F);
        private int lineIndex;

        public ReceiptPrinter(List<string> receiptLines)
        {
            lines = receiptLines;
            document = new PrintDocument();
            document.PrintPage += Document_PrintPage;

            // Narrow page width so it looks right on 80mm receipt paper; if the default
            // printer only supports standard paper sizes it will simply print on the
            // left part of a normal sheet.
            document.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
        }

        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = e.MarginBounds.Top;
            float lineHeight = font.GetHeight(e.Graphics) + 2;

            while (lineIndex < lines.Count)
            {
                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.Graphics.DrawString(lines[lineIndex], font, Brushes.Black, e.MarginBounds.Left, y);
                y += lineHeight;
                lineIndex++;
            }

            e.HasMorePages = false;
        }

        /// <summary>Opens the built-in preview dialog, which also has a Print button.</summary>
        public void ShowPreview()
        {
            lineIndex = 0;
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = document;
                preview.Text = "Receipt Preview";
                preview.Width = 500;
                preview.Height = 650;
                preview.ShowDialog();
            }
        }

        /// <summary>Sends the receipt straight to the default printer.</summary>
        public void Print()
        {
            lineIndex = 0;
            document.Print();
        }
    }
}
