class TaskList < ApplicationRecord
  self.table_name = "TaskList"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :task_lists
  belongs_to :team, foreign_key: "teamId", inverse_of: :task_lists
  has_many :tasks, foreign_key: "taskListId", inverse_of: :task_list, dependent: :destroy
end
