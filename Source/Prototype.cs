namespace Prototype
{
    public class PrototypeMainApp
    {
        public static void Main()
        {
            var mainModel = new Model
            {
                Data = 1
            };
            var copyModel = mainModel.Clone() as Model;
        }
    }

    public interface ICloneableModel
    {
        ICloneableModel Clone();
    }

    public class Model : ICloneableModel
    {
        public int Data { get; set; }

        public ICloneableModel Clone() => MemberwiseClone() as ICloneableModel;
    }
}