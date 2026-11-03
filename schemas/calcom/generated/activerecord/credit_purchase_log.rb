class CreditPurchaseLog < ApplicationRecord
  self.table_name = "CreditPurchaseLog"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  validates :credits, presence: true

  belongs_to :credit_balance, foreign_key: "creditBalanceId", inverse_of: :purchase_logs
end
