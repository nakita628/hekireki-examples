class CreditExpenseLog < ApplicationRecord
  self.table_name = "CreditExpenseLog"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :creditType, { monthly: "MONTHLY", additional: "ADDITIONAL" }, validate: true
  enum :creditFor, { sms: "SMS", cal_ai_phone_call: "CAL_AI_PHONE_CALL" }, validate: { allow_nil: true }

  validates :date, presence: true
  validates :externalRef, uniqueness: { allow_nil: true }

  belongs_to :credit_balance, foreign_key: "creditBalanceId", inverse_of: :expense_logs
  belongs_to :booking, foreign_key: "bookingUid", primary_key: "uid", optional: true, inverse_of: :expense_logs
end
