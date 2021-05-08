namespace DcsBios.Communicator
{
    public interface IBiosTranslator
    {
        /// <summary>
        /// Processes data coming from DCS-BIOS. The data provided here is the full data (i.e., the complete string),
        /// not the partial data as is sent from DCS-BIOS.
        /// </summary>
        /// <param name="biosCode">the bios string ID of the control</param>
        /// <param name="data">data stored for the address received</param>
        /// <typeparam name="T">T is either a string or an integer</typeparam>
        void FromBios<T>(string biosCode, T data);
    }
}