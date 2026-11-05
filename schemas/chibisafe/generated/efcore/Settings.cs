#nullable enable

namespace Chibisafe.Models;

public partial class Settings
{
    public int Id { get; set; }

    public int RateLimitWindow { get; set; }

    public int RateLimitMax { get; set; }

    public string Secret { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string ChunkSize { get; set; } = null!;

    public int ChunkedUploadsTimeout { get; set; }

    public string MaxSize { get; set; } = null!;

    public bool GenerateZips { get; set; }

    public bool GenerateOriginalFileNameWithIdentifier { get; set; } = false;

    public int GeneratedFilenameLength { get; set; }

    public int GeneratedAlbumLength { get; set; }

    public int GeneratedLinksLength { get; set; } = 8;

    public bool SaveDuplicatesToAlbum { get; set; } = false;

    public string BlockedExtensions { get; set; } = null!;

    public bool BlockNoExtension { get; set; }

    public bool PublicMode { get; set; }

    public bool UserAccounts { get; set; }

    public bool DisableStatisticsCron { get; set; }

    public bool DisableUpdateCheck { get; set; } = false;

    public string BackgroundImageURL { get; set; } = null!;

    public string LogoURL { get; set; } = null!;

    public string MetaDescription { get; set; } = null!;

    public string MetaKeywords { get; set; } = null!;

    public string MetaTwitterHandle { get; set; } = null!;

    public string MetaDomain { get; set; } = "";

    public string ServeUploadsFrom { get; set; } = "";

    public bool EnableMixedCaseFilenames { get; set; } = true;

    public string UsersStorageQuota { get; set; } = "0";

    public bool UseNetworkStorage { get; set; } = false;

    public bool UseMinimalHomepage { get; set; } = false;

    public bool UseUrlShortener { get; set; } = false;

    public bool GenerateThumbnails { get; set; } = true;

    public string PrivacyPolicyPageContent { get; set; } = "";

    public string TermsOfServicePageContent { get; set; } = "";

    public string RulesPageContent { get; set; } = "";

    public string S3Region { get; set; } = "";

    public string S3Bucket { get; set; } = "";

    public string S3AccessKey { get; set; } = "";

    public string S3SecretKey { get; set; } = "";

    public string S3Endpoint { get; set; } = "";

    public bool S3PathStyle { get; set; } = false;

    public string S3PublicUrl { get; set; } = "";

    public string DefaultSortOrder { get; set; } = "createdAt:desc";
}
