use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "settings")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "rateLimitWindow")]
    pub rate_limit_window: i32,
    #[sea_orm(column_name = "rateLimitMax")]
    pub rate_limit_max: i32,
    pub secret: String,
    #[sea_orm(column_name = "serviceName")]
    pub service_name: String,
    #[sea_orm(column_name = "chunkSize")]
    pub chunk_size: String,
    #[sea_orm(column_name = "chunkedUploadsTimeout")]
    pub chunked_uploads_timeout: i32,
    #[sea_orm(column_name = "maxSize")]
    pub max_size: String,
    #[sea_orm(column_name = "generateZips")]
    pub generate_zips: bool,
    #[sea_orm(
        column_name = "generateOriginalFileNameWithIdentifier",
        default_value = false
    )]
    pub generate_original_file_name_with_identifier: bool,
    #[sea_orm(column_name = "generatedFilenameLength")]
    pub generated_filename_length: i32,
    #[sea_orm(column_name = "generatedAlbumLength")]
    pub generated_album_length: i32,
    #[sea_orm(column_name = "generatedLinksLength", default_value = 8)]
    pub generated_links_length: i32,
    #[sea_orm(column_name = "saveDuplicatesToAlbum", default_value = false)]
    pub save_duplicates_to_album: bool,
    #[sea_orm(column_name = "blockedExtensions")]
    pub blocked_extensions: String,
    #[sea_orm(column_name = "blockNoExtension")]
    pub block_no_extension: bool,
    #[sea_orm(column_name = "publicMode")]
    pub public_mode: bool,
    #[sea_orm(column_name = "userAccounts")]
    pub user_accounts: bool,
    #[sea_orm(column_name = "disableStatisticsCron")]
    pub disable_statistics_cron: bool,
    #[sea_orm(column_name = "disableUpdateCheck", default_value = false)]
    pub disable_update_check: bool,
    #[sea_orm(column_name = "backgroundImageURL")]
    pub background_image_url: String,
    #[sea_orm(column_name = "logoURL")]
    pub logo_url: String,
    #[sea_orm(column_name = "metaDescription")]
    pub meta_description: String,
    #[sea_orm(column_name = "metaKeywords")]
    pub meta_keywords: String,
    #[sea_orm(column_name = "metaTwitterHandle")]
    pub meta_twitter_handle: String,
    #[sea_orm(column_name = "metaDomain", default_value = "")]
    pub meta_domain: String,
    #[sea_orm(column_name = "serveUploadsFrom", default_value = "")]
    pub serve_uploads_from: String,
    #[sea_orm(column_name = "enableMixedCaseFilenames", default_value = true)]
    pub enable_mixed_case_filenames: bool,
    #[sea_orm(column_name = "usersStorageQuota", default_value = "0")]
    pub users_storage_quota: String,
    #[sea_orm(column_name = "useNetworkStorage", default_value = false)]
    pub use_network_storage: bool,
    #[sea_orm(column_name = "useMinimalHomepage", default_value = false)]
    pub use_minimal_homepage: bool,
    #[sea_orm(column_name = "useUrlShortener", default_value = false)]
    pub use_url_shortener: bool,
    #[sea_orm(column_name = "generateThumbnails", default_value = true)]
    pub generate_thumbnails: bool,
    #[sea_orm(column_name = "privacyPolicyPageContent", default_value = "")]
    pub privacy_policy_page_content: String,
    #[sea_orm(column_name = "termsOfServicePageContent", default_value = "")]
    pub terms_of_service_page_content: String,
    #[sea_orm(column_name = "rulesPageContent", default_value = "")]
    pub rules_page_content: String,
    #[sea_orm(column_name = "S3Region", default_value = "")]
    pub s3_region: String,
    #[sea_orm(column_name = "S3Bucket", default_value = "")]
    pub s3_bucket: String,
    #[sea_orm(column_name = "S3AccessKey", default_value = "")]
    pub s3_access_key: String,
    #[sea_orm(column_name = "S3SecretKey", default_value = "")]
    pub s3_secret_key: String,
    #[sea_orm(column_name = "S3Endpoint", default_value = "")]
    pub s3_endpoint: String,
    #[sea_orm(column_name = "S3PathStyle", default_value = false)]
    pub s3_path_style: bool,
    #[sea_orm(column_name = "S3PublicUrl", default_value = "")]
    pub s3_public_url: String,
    #[sea_orm(column_name = "defaultSortOrder", default_value = "createdAt:desc")]
    pub default_sort_order: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
