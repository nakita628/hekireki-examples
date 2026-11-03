class ComputedTableActivity < ApplicationRecord
  self.table_name = "computed_table_activity"
  self.primary_key = "table_id"

  attribute :calculating_field_count, default: 0
  attribute :queued_field_count, default: 0
  attribute :estimated_complexity, default: 0
  attribute :recent_completions, default: -> { JSON.parse("[]") }
  attribute :generation, default: 0

  validates :base_id, presence: true
  validates :status, presence: true
end
