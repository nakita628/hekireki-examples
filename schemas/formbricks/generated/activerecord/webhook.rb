class Webhook < ApplicationRecord
  self.table_name = "Webhook"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :triggers, default: -> { [] }
  attribute :surveyIds, default: -> { [] }

  enum :source, { user: "user", zapier: "zapier", make: "make", n8n: "n8n", activepieces: "activepieces" },
       default: :user, validate: true

  validates :url, presence: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :webhooks
end
