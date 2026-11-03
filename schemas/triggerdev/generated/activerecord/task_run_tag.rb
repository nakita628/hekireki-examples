class TaskRunTag < ApplicationRecord
  self.table_name = "TaskRunTag"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true, uniqueness: { scope: :projectId }
  validates :friendlyId, presence: true, uniqueness: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :run_tags
end
