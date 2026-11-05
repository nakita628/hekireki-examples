class SentMessageOpen < ApplicationRecord
  self.table_name = "SentMessageOpen"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :openCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :token, presence: true, uniqueness: true
  validates :messageId, uniqueness: { scope: :emailAccountId, allow_nil: true }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :sent_message_opens
end
