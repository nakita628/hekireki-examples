class ComputedFieldActivity < ApplicationRecord
  self.table_name = "computed_field_activity"
  self.primary_key = "field_id"

  attribute :active_task_count, default: 0
  attribute :processing_task_count, default: 0
  attribute :generation, default: 0
  attribute :estimated_complexity, default: 0
  attribute :estimated_dirty_records, default: 0
  attribute :has_all_target_records, default: false

  validates :table_id, presence: true
  validates :base_id, presence: true
  validates :status, presence: true
end
