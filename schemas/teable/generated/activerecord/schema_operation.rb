class SchemaOperation < ApplicationRecord
  self.table_name = "schema_operation"
  self.inheritance_column = nil

  attribute :attempts, default: 0
  attribute :max_attempts, default: 8
  alias_attribute :updated_at, :last_modified_time

  validates :type, presence: true
  validates :status, presence: true
  validates :phase, presence: true
  validates :resource_type, presence: true
  validates :resource_id, presence: true
  validates :idempotency_key, presence: true, uniqueness: true
  validates :created_by, presence: true
end
