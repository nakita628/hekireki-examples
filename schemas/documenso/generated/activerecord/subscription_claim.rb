class SubscriptionClaim < ApplicationRecord
  self.table_name = "SubscriptionClaim"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :locked, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :teamCount, presence: true
  validates :memberCount, presence: true
  validates :envelopeItemCount, presence: true
  validates :recipientCount, presence: true
  validates :flags, exclusion: { in: [ nil ] }
  validates :documentRateLimits, exclusion: { in: [ nil ] }
  validates :emailRateLimits, exclusion: { in: [ nil ] }
  validates :apiRateLimits, exclusion: { in: [ nil ] }

  belongs_to :email_transport, foreign_key: "emailTransportId", optional: true, inverse_of: :subscription_claims
end
