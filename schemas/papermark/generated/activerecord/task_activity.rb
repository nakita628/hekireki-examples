class TaskActivity < ApplicationRecord
  self.table_name = "TaskActivity"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :type, presence: true

  belongs_to :task, foreign_key: "taskId", inverse_of: :activities
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :task_activities
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :task_activities
  belongs_to :view, foreign_key: "viewId", optional: true, inverse_of: :task_activities
end
