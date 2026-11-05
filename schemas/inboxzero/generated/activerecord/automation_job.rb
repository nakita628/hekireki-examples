class AutomationJob < ApplicationRecord
  self.table_name = "AutomationJob"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :cronExpression, presence: true
  validates :nextRunAt, presence: true
  validates :emailAccountId, uniqueness: true
  validates :emailAccountId, uniqueness: true

  belongs_to :messaging_channel, foreign_key: [ :messagingChannelId, :emailAccountId ],
                                 primary_key: [ :id, :emailAccountId ], inverse_of: :automation_jobs
  has_many :runs, class_name: "AutomationJobRun", foreign_key: "automationJobId", inverse_of: :automation_job,
                  dependent: :destroy
end
