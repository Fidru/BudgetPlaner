namespace IData.Interfaces
{
    public interface IElement : ITimestamp, IIdentifier
    {
        bool IsNew { get; set; }
        bool IsDeleted { get; set; }
        string Name { get; set; }

        int LoadingOrder { get; }

        void ConnectElements(IProject project);

        void Delete();
    }
}