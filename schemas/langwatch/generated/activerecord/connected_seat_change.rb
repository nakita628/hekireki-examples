class ConnectedSeatChange < ApplicationRecord
  self.table_name = "ConnectedSeatChange"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :currency, { usd: "USD", eur: "EUR" }, validate: { allow_nil: true }
  enum :state, { intent: "intent", invoiced: "invoiced", nothing_to_invoice: "nothing_to_invoice", not_onboarded: "not_onboarded" },
       validate: true

  validates :licenseId, presence: true, uniqueness: true
  validates :changedAt, presence: true
  validates :addedSeats, presence: true
  validates :unitAmountCents, presence: true
  validates :amountCents, presence: true
end
