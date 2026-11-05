class EmailSendOperation < ApplicationRecord
  self.table_name = "EmailSendOperation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { processing: "PROCESSING", sent: "SENT", uncertain: "UNCERTAIN" }, default: :processing, validate: true

  validates :clientMutationId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :payloadHash, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :email_send_operations
end
