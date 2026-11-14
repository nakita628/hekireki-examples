class ProcessManagerInstance < ApplicationRecord
  self.table_name = "ProcessManagerInstance"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :updated_at, :updatedAt

  validates :processName, presence: true, uniqueness: { scope: [ :projectId, :processKey ] }
  validates :projectId, presence: true
  validates :processKey, presence: true
  validates :tenantId, presence: true
  validates :state, exclusion: { in: [ nil ] }
  validates :revision, presence: true
end
