class Knowledge < ApplicationRecord
  self.table_name = "Knowledge"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true, uniqueness: { scope: :emailAccountId }
  validates :content, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :knowledge
end
