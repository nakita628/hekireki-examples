class DataDbConnection < ApplicationRecord
  self.table_name = "data_db_connection"

  attribute :id, default: -> { Cuid.generate }
  attribute :consecutive_health_failures, default: 0
  alias_attribute :updated_at, :last_modified_time

  enum :provider, { postgres: "postgres" }, default: :postgres, validate: true
  enum :status, { pending: "pending", validating: "validating", ready: "ready", error: "error", migrating: "migrating", disabled: "disabled" },
       default: :pending, validate: true
  enum :health_state, { healthy: "healthy", read_only: "read_only", unreachable: "unreachable", degraded: "degraded" },
       default: :healthy, validate: true

  validates :encrypted_url, presence: true
  validates :url_fingerprint, presence: true, uniqueness: true
  validates :internal_schema, presence: true
  validates :created_by, presence: true

  has_many :space_bindings, class_name: "SpaceDataDbBinding", inverse_of: :data_db_connection, dependent: :nullify
  has_many :source_migration_jobs, class_name: "SpaceDataDbMigrationJob", foreign_key: "source_connection_id",
                                   inverse_of: :source_connection, dependent: :nullify
  has_many :target_migration_jobs, class_name: "SpaceDataDbMigrationJob", foreign_key: "target_connection_id",
                                   inverse_of: :target_connection, dependent: :nullify
end
