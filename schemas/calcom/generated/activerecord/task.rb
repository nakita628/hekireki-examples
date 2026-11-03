class Task < ApplicationRecord
  self.table_name = "Task"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :attempts, default: 0
  attribute :maxAttempts, default: 3
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :type, presence: true
  validates :payload, presence: true
  validates :referenceUid, uniqueness: { scope: :type, allow_nil: true }
end
