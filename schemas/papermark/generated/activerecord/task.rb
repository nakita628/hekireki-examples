class Task < ApplicationRecord
  self.table_name = "Task"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :type, default: "TODO"
  attribute :status, default: "OPEN"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true

  belongs_to :task_list, foreign_key: "taskListId", inverse_of: :tasks
  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :tasks
  belongs_to :team, foreign_key: "teamId", inverse_of: :tasks
  belongs_to :upload_folder, class_name: "DataroomFolder", foreign_key: "uploadFolderId", optional: true,
                             inverse_of: :upload_tasks
  belongs_to :created_by_user, class_name: "User", foreign_key: "createdByUserId", optional: true,
                               inverse_of: :created_tasks
  has_many :assignments, class_name: "TaskAssignment", foreign_key: "taskId", inverse_of: :task, dependent: :destroy
  has_many :activities, class_name: "TaskActivity", foreign_key: "taskId", inverse_of: :task, dependent: :destroy
  has_many :uploads, class_name: "DocumentUpload", foreign_key: "taskId", inverse_of: :task, dependent: :nullify
end
