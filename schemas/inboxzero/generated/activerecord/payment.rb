class Payment < ApplicationRecord
  self.table_name = "Payment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :refunded, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :processorType, { lemon_squeezy: "LEMON_SQUEEZY", stripe: "STRIPE" }, default: :lemon_squeezy, validate: true

  validates :processorId, uniqueness: { allow_nil: true }
  validates :amount, presence: true
  validates :currency, presence: true
  validates :status, presence: true
  validates :tax, presence: true
  validates :taxInclusive, inclusion: { in: [ true, false ] }

  belongs_to :premium, foreign_key: "premiumId", optional: true, inverse_of: :payments
end
