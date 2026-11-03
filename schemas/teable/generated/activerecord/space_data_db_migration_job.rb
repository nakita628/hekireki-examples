class SpaceDataDbMigrationJob < ApplicationRecord
  self.table_name = "space_data_db_migration_job"

  attribute :id, default: -> { Cuid.generate }
  attribute :target_mode, default: "migrate-space"
  attribute :switch_on_completion, default: false
  alias_attribute :updated_at, :last_modified_time

  enum :state, { pending: "pending", waiting_worker: "waiting_worker", preflight: "preflight", freezing_writes: "freezing_writes", copying: "copying", validating: "validating", switching: "switching", succeeded: "succeeded", failed: "failed", canceled: "canceled", rolled_back: "rolled_back" },
       default: :pending, validate: true

  validates :target_url_fingerprint, presence: true
  validates :target_internal_schema, presence: true
  validates :created_by, presence: true

  belongs_to :space, inverse_of: :data_db_migration_jobs
  belongs_to :source_connection, class_name: "DataDbConnection", optional: true, inverse_of: :source_migration_jobs
  belongs_to :target_connection, class_name: "DataDbConnection", optional: true, inverse_of: :target_migration_jobs
end
