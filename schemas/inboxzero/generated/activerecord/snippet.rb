class Snippet < ApplicationRecord
  self.table_name = "Snippet"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :shortcut, presence: true, uniqueness: { scope: :emailAccountId }
  validates :content, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :snippets
end
