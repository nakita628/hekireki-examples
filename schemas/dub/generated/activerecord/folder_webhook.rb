class FolderWebhook < ApplicationRecord
  self.table_name = "FolderWebhook"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :folderId, uniqueness: { scope: :webhookId }

  belongs_to :folder, foreign_key: "folderId", inverse_of: :webhooks
  belongs_to :webhook, foreign_key: "webhookId", inverse_of: :folders
end
