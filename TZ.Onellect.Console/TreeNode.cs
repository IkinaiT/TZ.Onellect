namespace TZ.Onellect.Console
{
    public class TreeNode(int data)
    {
        public int Data { get; set; } = data;
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }


        public void Insert(TreeNode node)
        {
            if (node.Data < Data)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Insert(node);
                }
            }
        }

        public List<int> Transform(List<int>? elements = null)
        {
            elements ??= [];

            Left?.Transform(elements);

            elements.Add(Data);

            Right?.Transform(elements);

            return [.. elements];
        }
    }
}
