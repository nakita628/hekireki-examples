class Invoice < ApplicationRecord
  self.table_name = "Invoice"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :paid, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :totalAmount, presence: true
  validates :currency, presence: true
  validates :dueDate, presence: true

  belongs_to :subscription, foreign_key: "subscriptionId", inverse_of: :invoices
  has_many :line_items, class_name: "InvoiceItem", foreign_key: "invoiceId", inverse_of: :invoice,
                        dependent: :restrict_with_error
end
