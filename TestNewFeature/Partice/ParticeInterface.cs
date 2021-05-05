namespace TestNewFeature
{
    public class ParticeInterface
    {
        interface I1stLevelInterface
        {
            void execute1stMethod();
        }

        interface I2ndLevelInterface : I1stLevelInterface
        {
            void execute2ndMethod();
        }

        interface I3rdLevelInterface : I2ndLevelInterface
        {
            void execute3rdMethod();
        }

    }
}
