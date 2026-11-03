class Task < ApplicationRecord
  self.table_name = "task"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :type, presence: true
  validates :status, presence: true
  validates :created_by, presence: true

  has_many :runs, class_name: "TaskRun", inverse_of: :task, dependent: :destroy
end
