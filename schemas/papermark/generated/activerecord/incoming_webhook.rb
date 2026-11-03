class IncomingWebhook < ApplicationRecord
  self.table_name = "IncomingWebhook"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :consecutiveFailures, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :externalId, presence: true, uniqueness: true
  validates :name, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :incoming_webhooks
end
