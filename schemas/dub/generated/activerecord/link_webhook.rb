class LinkWebhook < ApplicationRecord
  self.table_name = "LinkWebhook"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :linkId, uniqueness: { scope: :webhookId }

  belongs_to :link, foreign_key: "linkId", inverse_of: :webhooks
  belongs_to :webhook, foreign_key: "webhookId", inverse_of: :links
end
