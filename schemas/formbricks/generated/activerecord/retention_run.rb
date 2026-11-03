class RetentionRun < ApplicationRecord
  self.table_name = "RetentionRun"

  attribute :id, default: -> { Cuid2.call }
  attribute :notifiedCount, default: 0
  attribute :archivedCount, default: 0
  attribute :deletedCount, default: 0
  attribute :skippedCount, default: 0
  attribute :hasChanges, default: false

  enum :entity, { responses: "responses", surveys: "surveys", members: "members" }, validate: true
  enum :kind, { sweep: "sweep", purge: "purge" }, default: :sweep, validate: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :retention_runs
  has_many :items, class_name: "RetentionRunItem", foreign_key: "runId", inverse_of: :run, dependent: :destroy
end
