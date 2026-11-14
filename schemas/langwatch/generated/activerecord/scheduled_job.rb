class ScheduledJob < ApplicationRecord
  self.table_name = "ScheduledJob"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :attempts, default: 0
  attribute :active, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, presence: true
  validates :targetType, presence: true, uniqueness: { scope: :targetId }
  validates :targetId, presence: true
  validates :cron, presence: true
  validates :timezone, presence: true
  validates :nextRunAt, presence: true
end
