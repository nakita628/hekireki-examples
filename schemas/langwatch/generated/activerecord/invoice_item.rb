class InvoiceItem < ApplicationRecord
  self.table_name = "InvoiceItem"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :description, presence: true
  validates :amount, presence: true

  belongs_to :invoice, foreign_key: "invoiceId", inverse_of: :line_items
end
