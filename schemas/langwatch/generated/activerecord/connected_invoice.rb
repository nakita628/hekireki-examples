class ConnectedInvoice < ApplicationRecord
  self.table_name = "ConnectedInvoice"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :kind, { annual: "annual", seat_change: "seat_change", usage: "usage" }, validate: true
  enum :currency, { usd: "USD", eur: "EUR" }, validate: true

  validates :accountId, presence: true
  validates :stripeInvoiceId, presence: true, uniqueness: true
  validates :amountCents, presence: true
  validates :status, presence: true
end
