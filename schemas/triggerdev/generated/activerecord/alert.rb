class Alert < ApplicationRecord
  self.table_name = "Alert"

  attribute :lifecycle, default: "active"
  attribute :attention, default: "clear"
  attribute :observedCondition, default: "unknown"
  attribute :notifiers, default: -> { JSON.parse("[]") }
  attribute :occurrencesSinceRaise, default: 0
  attribute :occurrencesTotal, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :projectId, presence: true
  validates :environmentId, presence: true
  validates :capabilityKey, presence: true
  validates :params, exclusion: { in: [ nil ] }
  validates :condition, exclusion: { in: [ nil ] }
  validates :createdBy, presence: true
end
