class ResponseTime < ApplicationRecord
  self.table_name = "ResponseTime"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :threadId, presence: true
  validates :sentMessageId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :receivedMessageId, presence: true
  validates :responseTimeMins, presence: true
  validates :receivedAt, presence: true
  validates :sentAt, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :response_times
end
