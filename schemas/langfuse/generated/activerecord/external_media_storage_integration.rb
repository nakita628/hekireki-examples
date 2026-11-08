class ExternalMediaStorageIntegration < ApplicationRecord
  self.primary_key = "project_id"
  self.inheritance_column = nil

  enum :type, { s3: "S3", s3_compatible: "S3_COMPATIBLE" }, validate: true
  enum :auth_type, { access_keys: "ACCESS_KEYS", role_arn: "ROLE_ARN" }, default: :access_keys, validate: true

  validates :bucket_name, presence: true
  validates :region, presence: true
  validates :force_path_style, inclusion: { in: [ true, false ] }

  belongs_to :project
end
