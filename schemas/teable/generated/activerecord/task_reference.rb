class TaskReference < ApplicationRecord
  self.table_name = "task_reference"

  attribute :id, default: -> { Cuid.generate }

  validates :from_field_id, presence: true
  validates :to_field_id, presence: true, uniqueness: { scope: :from_field_id }
end
