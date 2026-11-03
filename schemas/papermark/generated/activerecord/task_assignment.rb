class TaskAssignment < ApplicationRecord
  self.table_name = "TaskAssignment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :taskId, uniqueness: { scope: :viewerId, conditions: -> { where.not(viewerId: nil) } }
  validates :taskId, uniqueness: { scope: :groupId, conditions: -> { where.not(groupId: nil) } }
  validates :taskId, uniqueness: { scope: :linkId, conditions: -> { where.not(linkId: nil) } }
  validates :email, uniqueness: { scope: :taskId, allow_nil: true }

  belongs_to :task, foreign_key: "taskId", inverse_of: :assignments
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :task_assignments
  belongs_to :group, class_name: "ViewerGroup", foreign_key: "groupId", optional: true, inverse_of: :task_assignments
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :task_assignments
end
