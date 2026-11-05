defmodule Chibisafe.Settings do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          rate_limit_window: integer(),
          rate_limit_max: integer(),
          secret: String.t(),
          service_name: String.t(),
          chunk_size: String.t(),
          chunked_uploads_timeout: integer(),
          max_size: String.t(),
          generate_zips: boolean(),
          generate_original_file_name_with_identifier: boolean(),
          generated_filename_length: integer(),
          generated_album_length: integer(),
          generated_links_length: integer(),
          save_duplicates_to_album: boolean(),
          blocked_extensions: String.t(),
          block_no_extension: boolean(),
          public_mode: boolean(),
          user_accounts: boolean(),
          disable_statistics_cron: boolean(),
          disable_update_check: boolean(),
          background_image_url: String.t(),
          logo_url: String.t(),
          meta_description: String.t(),
          meta_keywords: String.t(),
          meta_twitter_handle: String.t(),
          meta_domain: String.t(),
          serve_uploads_from: String.t(),
          enable_mixed_case_filenames: boolean(),
          users_storage_quota: String.t(),
          use_network_storage: boolean(),
          use_minimal_homepage: boolean(),
          use_url_shortener: boolean(),
          generate_thumbnails: boolean(),
          privacy_policy_page_content: String.t(),
          terms_of_service_page_content: String.t(),
          rules_page_content: String.t(),
          s3_region: String.t(),
          s3_bucket: String.t(),
          s3_access_key: String.t(),
          s3_secret_key: String.t(),
          s3_endpoint: String.t(),
          s3_path_style: boolean(),
          s3_public_url: String.t(),
          default_sort_order: String.t()
        }

  schema "settings" do
    field(:rate_limit_window, :integer, source: :rateLimitWindow)
    field(:rate_limit_max, :integer, source: :rateLimitMax)
    field(:secret, :string)
    field(:service_name, :string, source: :serviceName)
    field(:chunk_size, :string, source: :chunkSize)
    field(:chunked_uploads_timeout, :integer, source: :chunkedUploadsTimeout)
    field(:max_size, :string, source: :maxSize)
    field(:generate_zips, :boolean, source: :generateZips)
    field(:generate_original_file_name_with_identifier, :boolean, default: false, source: :generateOriginalFileNameWithIdentifier)
    field(:generated_filename_length, :integer, source: :generatedFilenameLength)
    field(:generated_album_length, :integer, source: :generatedAlbumLength)
    field(:generated_links_length, :integer, default: 8, source: :generatedLinksLength)
    field(:save_duplicates_to_album, :boolean, default: false, source: :saveDuplicatesToAlbum)
    field(:blocked_extensions, :string, source: :blockedExtensions)
    field(:block_no_extension, :boolean, source: :blockNoExtension)
    field(:public_mode, :boolean, source: :publicMode)
    field(:user_accounts, :boolean, source: :userAccounts)
    field(:disable_statistics_cron, :boolean, source: :disableStatisticsCron)
    field(:disable_update_check, :boolean, default: false, source: :disableUpdateCheck)
    field(:background_image_url, :string, source: :backgroundImageURL)
    field(:logo_url, :string, source: :logoURL)
    field(:meta_description, :string, source: :metaDescription)
    field(:meta_keywords, :string, source: :metaKeywords)
    field(:meta_twitter_handle, :string, source: :metaTwitterHandle)
    field(:meta_domain, :string, default: "", source: :metaDomain)
    field(:serve_uploads_from, :string, default: "", source: :serveUploadsFrom)
    field(:enable_mixed_case_filenames, :boolean, default: true, source: :enableMixedCaseFilenames)
    field(:users_storage_quota, :string, default: "0", source: :usersStorageQuota)
    field(:use_network_storage, :boolean, default: false, source: :useNetworkStorage)
    field(:use_minimal_homepage, :boolean, default: false, source: :useMinimalHomepage)
    field(:use_url_shortener, :boolean, default: false, source: :useUrlShortener)
    field(:generate_thumbnails, :boolean, default: true, source: :generateThumbnails)
    field(:privacy_policy_page_content, :string, default: "", source: :privacyPolicyPageContent)
    field(:terms_of_service_page_content, :string, default: "", source: :termsOfServicePageContent)
    field(:rules_page_content, :string, default: "", source: :rulesPageContent)
    field(:s3_region, :string, default: "", source: :S3Region)
    field(:s3_bucket, :string, default: "", source: :S3Bucket)
    field(:s3_access_key, :string, default: "", source: :S3AccessKey)
    field(:s3_secret_key, :string, default: "", source: :S3SecretKey)
    field(:s3_endpoint, :string, default: "", source: :S3Endpoint)
    field(:s3_path_style, :boolean, default: false, source: :S3PathStyle)
    field(:s3_public_url, :string, default: "", source: :S3PublicUrl)
    field(:default_sort_order, :string, default: "createdAt:desc", source: :defaultSortOrder)
  end
end