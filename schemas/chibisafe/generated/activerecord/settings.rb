class Settings < ApplicationRecord
  attribute :generateOriginalFileNameWithIdentifier, default: false
  attribute :generatedLinksLength, default: 8
  attribute :saveDuplicatesToAlbum, default: false
  attribute :disableUpdateCheck, default: false
  attribute :metaDomain, default: ""
  attribute :serveUploadsFrom, default: ""
  attribute :enableMixedCaseFilenames, default: true
  attribute :usersStorageQuota, default: "0"
  attribute :useNetworkStorage, default: false
  attribute :useMinimalHomepage, default: false
  attribute :useUrlShortener, default: false
  attribute :generateThumbnails, default: true
  attribute :privacyPolicyPageContent, default: ""
  attribute :termsOfServicePageContent, default: ""
  attribute :rulesPageContent, default: ""
  attribute :S3Region, default: ""
  attribute :S3Bucket, default: ""
  attribute :S3AccessKey, default: ""
  attribute :S3SecretKey, default: ""
  attribute :S3Endpoint, default: ""
  attribute :S3PathStyle, default: false
  attribute :S3PublicUrl, default: ""
  attribute :defaultSortOrder, default: "createdAt:desc"

  validates :rateLimitWindow, presence: true
  validates :rateLimitMax, presence: true
  validates :secret, presence: true
  validates :serviceName, presence: true
  validates :chunkSize, presence: true
  validates :chunkedUploadsTimeout, presence: true
  validates :maxSize, presence: true
  validates :generateZips, inclusion: { in: [ true, false ] }
  validates :generatedFilenameLength, presence: true
  validates :generatedAlbumLength, presence: true
  validates :blockedExtensions, presence: true
  validates :blockNoExtension, inclusion: { in: [ true, false ] }
  validates :publicMode, inclusion: { in: [ true, false ] }
  validates :userAccounts, inclusion: { in: [ true, false ] }
  validates :disableStatisticsCron, inclusion: { in: [ true, false ] }
  validates :backgroundImageURL, presence: true
  validates :logoURL, presence: true
  validates :metaDescription, presence: true
  validates :metaKeywords, presence: true
  validates :metaTwitterHandle, presence: true
end
