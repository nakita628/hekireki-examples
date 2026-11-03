class AlertDelivery < ApplicationRecord
  self.table_name = "AlertDelivery"
  self.primary_key = %w[id eventAt]

  attribute :status, default: "pending"
  attribute :attempts, default: 0
  alias_attribute :created_at, :createdAt

  validates :alertEventId, presence: true, uniqueness: { scope: [ :notifierId, :eventAt ] }
  validates :notifierId, presence: true
  validates :notifier, exclusion: { in: [ nil ] }
end
