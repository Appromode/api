using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace marking_api.Global.Services
{
    /// <summary>
    /// Certificate validation service
    /// </summary>
    public class CertificateValidationService
    {
        /// <summary>
        /// Checks if the thumbprint of a newly generated certificate is the same as one parsed in.
        /// </summary>
        /// <param name="clientCertificate">X509Certificate2 - certificate to validate</param>
        /// <returns>True if certificate has the same thumbprint</returns>
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            var cert = new X509Certificate2(Path.Combine("localhost_root_l1.pfx"), "1234");
            if (clientCertificate.Thumbprint == cert.Thumbprint)
                return true;
            return false;
        }
    }
}
