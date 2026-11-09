class TaskLog < ApplicationRecord
  self.table_name = "task_log"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :level, presence: true
  validates :logger, presence: true
  validates :message, presence: true
  validates :sync_id, presence: true
  validates :task_id, presence: true
end
