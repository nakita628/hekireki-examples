class TaskRun < ApplicationRecord
  self.table_name = "task_run"

  attribute :id, default: -> { Cuid.generate }
  attribute :depends_on_run_ids, default: -> { [] }
  alias_attribute :updated_at, :last_modified_time

  validates :base_id, presence: true
  validates :status, presence: true
  validates :snapshot, presence: true

  belongs_to :task, inverse_of: :runs
end
