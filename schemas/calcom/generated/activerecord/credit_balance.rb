class CreditBalance < ApplicationRecord
  self.table_name = "CreditBalance"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :additionalCredits, default: 0

  validates :teamId, uniqueness: { allow_nil: true }
  validates :userId, uniqueness: { allow_nil: true }

  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :credit_balance
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :credit_balance
  has_many :expense_logs, class_name: "CreditExpenseLog", foreign_key: "creditBalanceId", inverse_of: :credit_balance,
                          dependent: :destroy
  has_many :purchase_logs, class_name: "CreditPurchaseLog", foreign_key: "creditBalanceId", inverse_of: :credit_balance,
                           dependent: :destroy
end
