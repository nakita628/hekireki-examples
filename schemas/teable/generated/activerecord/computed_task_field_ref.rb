class ComputedTaskFieldRef < ApplicationRecord
  self.table_name = "computed_task_field_ref"
  self.primary_key = %w[task_id field_id]

  attribute :was_processing, default: false

  validates :table_id, presence: true
  validates :base_id, presence: true
end
