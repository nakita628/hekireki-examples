class ProcessManagerInbox < ApplicationRecord
  self.table_name = "ProcessManagerInbox"

  attribute :id, default: -> { Nanoid.generate }

  validates :processName, presence: true,
                          uniqueness: { scope: [ :projectId, :sourceEventKey ], conditions: -> { where.not(sourceEventKey: nil) } }
  validates :projectId, presence: true
  validates :processKey, presence: true
  validates :tenantId, presence: true
  validates :sourceEventId, presence: true
  validates :consumedAt, presence: true
end
