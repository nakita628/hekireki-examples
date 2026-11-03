class DataMigration < ApplicationRecord
  self.table_name = "DataMigration"

  attribute :id, default: -> { Cuid.generate }

  enum :status, { pending: "pending", applied: "applied", failed: "failed" }, validate: true

  validates :name, presence: true, uniqueness: true
end
