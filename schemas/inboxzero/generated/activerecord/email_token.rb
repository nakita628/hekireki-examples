class EmailToken < ApplicationRecord
  self.table_name = "EmailToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :email_tokens
end
