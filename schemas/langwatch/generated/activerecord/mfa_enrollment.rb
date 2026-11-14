class MfaEnrollment < ApplicationRecord
  self.table_name = "MfaEnrollment"
  self.primary_key = "userId"

  attribute :backupCodeCount, default: 0
  attribute :consumedBackupCodeIndexes, default: -> { [] }
  attribute :failedCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :state, presence: true
  validates :occurredAt, presence: true
  validates :lastEventId, presence: true
  validates :acceptedAt, presence: true
  validates :projectionVersion, presence: true
end
