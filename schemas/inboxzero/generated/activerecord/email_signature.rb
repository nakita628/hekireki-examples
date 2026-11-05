class EmailSignature < ApplicationRecord
  self.table_name = "EmailSignature"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :emailAccountId }
  validates :html, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :email_signatures
end
