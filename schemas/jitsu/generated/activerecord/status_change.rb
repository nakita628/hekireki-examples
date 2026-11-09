class StatusChange < ApplicationRecord
  self.table_name = "StatusChange"
  self.inheritance_column = nil

  attribute :counts, default: 0
  attribute :queueSize, default: 0

  validates :workspaceId, presence: true
  validates :actorId, presence: true
  validates :type, presence: true
  validates :tableName, presence: true
  validates :status, presence: true
  validates :startedAt, presence: true
end
