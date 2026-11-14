class SlackIntegration < ApplicationRecord
  self.table_name = "SlackIntegration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :kind, { bot: "BOT", incoming_webhook: "INCOMING_WEBHOOK" }, validate: true
  enum :scopeType, { organization: "ORGANIZATION", project: "PROJECT" }, validate: true

  validates :name, presence: true
  validates :scopeId, presence: true
  validates :organizationId, presence: true, uniqueness: { scope: [ :scopeType, :scopeId, :secretFingerprint ] }
  validates :secretFingerprint, presence: true
  validates :secretHint, presence: true
  validates :createdById, presence: true
  validates :updatedById, presence: true
end
