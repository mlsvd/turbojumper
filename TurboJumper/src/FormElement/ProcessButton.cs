namespace TurboJumper.FormElement;

public class ProcessButton : Button
{
    public string BottomText { get; set; }
    
    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        Graphics g = pevent.Graphics;
        Rectangle rect = this.ClientRectangle;

        // Draw the separator line
        int separatorX = this.Image != null ? this.Image.Width + 5 : 10;
        using (Pen pen = new Pen(Color.LightGray))
        {
            g.DrawLine(pen, separatorX, 5, separatorX, rect.Height - 5);
        }

        if (!string.IsNullOrEmpty(BottomText))
        {
            using (Font bottomTextFont = new Font(this.Font.FontFamily, this.Font.Size * 0.8f))
            {
                SizeF textSize = pevent.Graphics.MeasureString(BottomText, bottomTextFont);
                PointF textLocation = new PointF(
                    (this.Width - textSize.Width) / 2,
                    this.Height - textSize.Height - 2 // Adjust the -3 value as needed
                );
                pevent.Graphics.DrawString(BottomText, bottomTextFont, SystemBrushes.ControlText, textLocation);
            }
        }
    }
}