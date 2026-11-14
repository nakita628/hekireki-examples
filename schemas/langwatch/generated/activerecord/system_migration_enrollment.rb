class SystemMigrationEnrollment < ApplicationRecord
  self.table_name = "SystemMigrationEnrollment"
  self.primary_key = %w[organizationId migrationName]

  alias_attribute :created_at, :createdAt

  validates :enrolledByUserId, presence: true
end
