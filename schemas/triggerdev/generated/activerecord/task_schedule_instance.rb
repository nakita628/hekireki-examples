class TaskScheduleInstance < ApplicationRecord
  self.table_name = "TaskScheduleInstance"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :active, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :taskScheduleId, uniqueness: { scope: :environmentId }

  belongs_to :task_schedule, foreign_key: "taskScheduleId", inverse_of: :instances
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId",
                           inverse_of: :task_schedule_instances
  belongs_to :project, foreign_key: "projectId", inverse_of: :task_schedule_instances
end
