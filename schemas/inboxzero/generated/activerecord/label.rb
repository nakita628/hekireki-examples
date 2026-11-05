class Label < ApplicationRecord
  self.table_name = "Label"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :gmailLabelId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :name, presence: true, uniqueness: { scope: :emailAccountId }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :labels
end
