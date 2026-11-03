class TaskSchedule < ApplicationRecord
  self.table_name = "TaskSchedule"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :deduplicationKey, default: -> { Cuid.generate }
  attribute :userProvidedDeduplicationKey, default: false
  attribute :generatorDescription, default: ""
  attribute :timezone, default: "UTC"
  attribute :active, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { declarative: "DECLARATIVE", imperative: "IMPERATIVE" }, default: :imperative, validate: true
  enum :generatorType, { cron: "CRON" }, default: :cron, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :taskIdentifier, presence: true
  validates :deduplicationKey, uniqueness: { scope: :projectId }
  validates :generatorExpression, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :task_schedules
  has_many :instances, class_name: "TaskScheduleInstance", foreign_key: "taskScheduleId", inverse_of: :task_schedule,
                       dependent: :destroy
end
