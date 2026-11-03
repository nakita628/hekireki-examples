class AppMigration < ApplicationRecord
  self.table_name = "AppMigration"

  alias_attribute :created_at, :createdAt

  enum :status, { applied: "APPLIED", pending: "PENDING", failed: "FAILED" }, validate: true

  validates :name, presence: true, uniqueness: true
end
