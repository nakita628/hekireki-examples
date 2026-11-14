class SystemMigrationTenantState < ApplicationRecord
  self.table_name = "SystemMigrationTenantState"
  self.primary_key = %w[migrationName tenantId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :status, presence: true
end
