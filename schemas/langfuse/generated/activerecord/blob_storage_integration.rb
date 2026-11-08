class BlobStorageIntegration < ApplicationRecord
  self.primary_key = "project_id"
  self.inheritance_column = nil

  attribute :export_field_groups,
            default: -> { %w[core basic time io metadata model usage prompt metrics tools trace_context] }
  attribute :compressed, default: true
  attribute :backfill, default: true

  enum :type, { s3: "S3", s3_compatible: "S3_COMPATIBLE", azure_blob_storage: "AZURE_BLOB_STORAGE", google_cloud_storage: "GOOGLE_CLOUD_STORAGE" },
       validate: true
  enum :auth_type, { access_keys: "ACCESS_KEYS", role_arn: "ROLE_ARN" }, default: :access_keys, validate: true
  enum :file_type, { json: "JSON", csv: "CSV", jsonl: "JSONL", parquet: "PARQUET" }, default: :csv, validate: true
  enum :export_mode, { full_history: "FULL_HISTORY", from_today: "FROM_TODAY", from_custom_date: "FROM_CUSTOM_DATE" },
       default: :full_history, validate: true
  enum :export_source, { traces_observations: "TRACES_OBSERVATIONS", traces_observations_events: "TRACES_OBSERVATIONS_EVENTS", events: "EVENTS" },
       default: :traces_observations, validate: true

  validates :bucket_name, presence: true
  validates :prefix, presence: true
  validates :region, presence: true
  validates :force_path_style, inclusion: { in: [ true, false ] }
  validates :enabled, inclusion: { in: [ true, false ] }
  validates :export_frequency, presence: true

  belongs_to :project, inverse_of: :blob_storage_integration
end
