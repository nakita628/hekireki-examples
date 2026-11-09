class SourceTask < ApplicationRecord
  self.table_name = "source_task"
  self.primary_key = "task_id"

  validates :sync_id, presence: true
  validates :package, presence: true
  validates :version, presence: true
  validates :status, presence: true
end
