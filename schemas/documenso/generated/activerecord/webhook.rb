class Webhook < ApplicationRecord
  self.table_name = "Webhook"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :eventTriggers, default: -> { [] }
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :webhookUrl, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :webhooks
  belongs_to :team, foreign_key: "teamId", inverse_of: :webhooks
  has_many :webhook_calls, foreign_key: "webhookId", inverse_of: :webhook, dependent: :destroy
end
